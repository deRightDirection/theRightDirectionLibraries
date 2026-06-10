using Meziantou.Framework.Win32;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace theRightDirection.EntityFramework;

/// <summary>
/// Provides AES-256-GCM authenticated encryption and decryption.
/// The encryption key is read from <c>IConfiguration</c> under the key <c>Encryption:Key</c>
/// as a Base64-encoded 32-byte value.
/// </summary>
/// <example>
/// Register in DI:
/// <code>
/// builder.Services.AddSingleton&lt;EncryptionService&gt;();
/// </code>
/// Set the key via environment variable (double underscore maps to colon):
/// <code>
/// ENCRYPTION__KEY=&lt;base64-encoded-32-bytes&gt;
/// </code>
/// Or via user-secrets for local development:
/// <code>
/// dotnet user-secrets set "Encryption:Key" "&lt;base64-encoded-32-bytes&gt;"
/// </code>
/// </example>
public class EncryptionService
{
    private const int SaltSize = 32;        // 256 bits
    private const int NonceSize = 12;       // 96 bits (AES-GCM standard)
    private const int TagSize = 16;         // 128 bits (maximum for best security)
    private const int KeySize = 32;         // 256 bits for AES-256
    private const int Iterations = 600000;

    private readonly byte[] _key;

    /// <summary>
    /// Create a new encryption service, loading the Encryption:Key from the environment variables.
    /// </summary>
    public EncryptionService(IConfiguration config)
    {
        var windowsCredentialManagerKeyName = config["EF:EncryptionKeyName"];
        if (windowsCredentialManagerKeyName == null || string.IsNullOrEmpty(windowsCredentialManagerKeyName))
        {
            throw new Exception("the configuration element 'EF:EncryptionKeyName' is missing");
        }
        var windowsCredential = CredentialManager.ReadCredential(windowsCredentialManagerKeyName);
        if (windowsCredential == null)
        {
            throw new Exception($"there is no key stored in the Windows Credential Manager with the key '{windowsCredentialManagerKeyName}'");
        }
        _key = Convert.FromBase64String(windowsCredential.Password);
    }

    public EncryptionService(string key)
    {
        _key = Convert.FromBase64String(key);
    }

    public string Encrypt(string plaintext)
    {
        // Convert the plaintext string to a byte array
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        // Generate a cryptographically random salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Derive a key from the password using PBKDF2 with SHA-256
        // This is the modern way - using the static Pbkdf2 method instead of the old constructor
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            password: _key,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: KeySize
        );

        // Generate a random nonce (number used once) for AES-GCM
        byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);

        // Prepare buffers for ciphertext and authentication tag
        byte[] ciphertext = new byte[plaintextBytes.Length];
        byte[] tag = new byte[TagSize];

        // Encrypt using AES-GCM (this is the big upgrade from AES-CBC!)
        using (var aesGcm = new AesGcm(key, TagSize))
        {
            aesGcm.Encrypt(nonce, plaintextBytes, ciphertext, tag);
        }

        // Combine salt + nonce + tag + ciphertext for storage
        // We need all of these to decrypt later
        byte[] result = new byte[salt.Length + nonce.Length + tag.Length + ciphertext.Length];
        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(nonce, 0, result, salt.Length, nonce.Length);
        Buffer.BlockCopy(tag, 0, result, salt.Length + nonce.Length, tag.Length);
        Buffer.BlockCopy(ciphertext, 0, result, salt.Length + nonce.Length + tag.Length, ciphertext.Length);

        // Return as Base64 for easy storage and transmission
        return Convert.ToBase64String(result);
    }

    public string Decrypt(string encryptedBase64)
    {
        // Convert from Base64 back to bytes
        byte[] encryptedData = Convert.FromBase64String(encryptedBase64);

        // Extract the salt, nonce, tag, and ciphertext
        byte[] salt = new byte[SaltSize];
        byte[] nonce = new byte[NonceSize];
        byte[] tag = new byte[TagSize];
        byte[] ciphertext = new byte[encryptedData.Length - SaltSize - NonceSize - TagSize];

        Buffer.BlockCopy(encryptedData, 0, salt, 0, SaltSize);
        Buffer.BlockCopy(encryptedData, SaltSize, nonce, 0, NonceSize);
        Buffer.BlockCopy(encryptedData, SaltSize + NonceSize, tag, 0, TagSize);
        Buffer.BlockCopy(encryptedData, SaltSize + NonceSize + TagSize, ciphertext, 0, ciphertext.Length);

        // Derive the same key from the password using the extracted salt
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            password: _key,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: KeySize
        );

        // Prepare buffer for plaintext
        byte[] plaintext = new byte[ciphertext.Length];

        // Decrypt using AES-GCM
        // If the tag doesn't match, this will throw a CryptographicException
        // This is the tamper-protection feature that AES-CBC didn't have!
        using (var aesGcm = new AesGcm(key, TagSize))
        {
            aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);
        }

        // Convert back to string
        return Encoding.UTF8.GetString(plaintext);
    }
}
