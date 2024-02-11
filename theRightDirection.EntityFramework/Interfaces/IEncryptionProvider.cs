namespace theRightDirection.EntityFramework.Interfaces;

public interface IEncryptionProvider
{
    string Encrypt(string dataToEncrypt);
    string Decrypt(string dataToDecrypt);
}