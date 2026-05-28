using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace theRightDirection.EntityFramework.Converter;

public class EncryptionConverter(EncryptionService encryptionService) : ValueConverter<string, string>(
    v => encryptionService.Encrypt(v),
    v => encryptionService.Decrypt(v));