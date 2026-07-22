namespace theRightDirection.EntityFramework.DeprecatedEncryption;

[Obsolete]
public interface IEncryptionProvider
{
    string Encrypt(string dataToEncrypt);
    string Decrypt(string dataToDecrypt);
}