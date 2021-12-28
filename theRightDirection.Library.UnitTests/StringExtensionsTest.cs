using FluentAssertions;
using Xunit;

namespace theRightDirection.Library
{
    public class StringExtensionsTest
    {
        [Fact]
        public void Decrypt_Does_Not_Cut_String()
        {
            var json = "{\"CustomerName\":\"Defensie\",\"CustomerId\":\"fc584745-1347-4579-87d2-56e93ca1d998\",\"Type\":2,\"ExpirationDate\":\"2025-10-24T00:00:00\",\"AnalyzerLicense\":{\"Status\":3,\"NumberOfAllowedPortals\":10},\"OfflineUsage\":false}";
            var passPhase = "PortalGenius";
            var encrypted = json.Encrypt(passPhase);
            var result = encrypted.Decrypt(passPhase);
            result.Should().BeEquivalentTo(json);
        }
    }
}