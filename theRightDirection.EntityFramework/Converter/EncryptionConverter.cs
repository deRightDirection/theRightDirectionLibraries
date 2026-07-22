using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using theRightDirection.EntityFramework.DeprecatedEncryption;

namespace theRightDirection.EntityFramework.Converter;

public class EncryptionConverter : ValueConverter<string, string>
{
    public EncryptionConverter(EncryptionService encryptionService) : base(x => encryptionService.Encrypt(x), x => encryptionService.Decrypt(x))
    {
    }
    [Obsolete]
    public EncryptionConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null) : base(x => encryptionProvider.Encrypt(x), x => encryptionProvider.Decrypt(x), mappingHints)
    {
    }
}