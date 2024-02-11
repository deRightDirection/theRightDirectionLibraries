using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using theRightDirection.EntityFramework.Interfaces;

namespace theRightDirection.EntityFramework.Converter;

internal sealed class EncryptionConverter : ValueConverter<string, string>
{
    public EncryptionConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null) : base (x => encryptionProvider.Encrypt(x), x => encryptionProvider.Decrypt(x), mappingHints)
    {
    }
}