using FluentAssertions;
using System;
using Xunit;

namespace theRightDirection.Library
{
    public class StringExtensionsTest
    {
        [Fact]
        public void HasNoText_With_Null_Is_False()
        {
            string text = null;
            var result = text.HasNoText();
            result.Should().BeTrue();
        }
        [Fact]
        public void HasNoText_Is_Empty_Is_False()
        {
            string text = "";
            var result = text.HasNoText();
            result.Should().BeTrue();
        }
        [Fact]
        public void HasNoText_With_Spaces_Is_False()
        {
            string text = "   ";
            var result = text.HasNoText();
            result.Should().BeTrue();
        }
        [Fact]
        public void HasNoText_With_Text_Is_True()
        {
            string text = "Mannus";
            var result = text.HasNoText();
            result.Should().BeFalse();
        }
        [Fact]
        public void HasText_With_Null_Is_False()
        {
            string text = null;
            var result = text.HasText();
            result.Should().BeFalse();
        }
        [Fact]
        public void HasText_Is_Empty_Is_False()
        {
            string text = "";
            var result = text.HasText();
            result.Should().BeFalse();
        }
        [Fact]
        public void HasText_With_Spaces_Is_False()
        {
            string text = "   ";
            var result = text.HasText();
            result.Should().BeFalse();
        }
        [Fact]
        public void HasText_With_Text_Is_True()
        {
            string text = "Mannus";
            var result = text.HasText();
            result.Should().BeTrue();
        }

        [Fact]
        public void Remove_Special_Characters_With_Spaces_From_String()
        {
            var input = "Mannus   Etten";
            var result = input.RemoveSpecialCharactersFromString("_");
            result.Should().Be("Mannus___Etten");
        }
        [Fact]
        public void Remove_Special_Characters_From_String()
        {
            var input = "Mannus%^&$!Etten";
            var result = input.RemoveSpecialCharactersFromString();
            result.Should().Be("MannusEtten");
        }
        [Fact]
        public void Remove_Special_Characters_From_String_With_Replacement()
        {
            var input = "Mannus%^&$!Etten";
            var result = input.RemoveSpecialCharactersFromString("_");
            result.Should().Be("Mannus_____Etten");
        }
        [Fact]
        public void Remove_Special_Characters_From_String_With_Special_Character()
        {
            var input = "Mannus%^&$!Etten";
            var result = input.RemoveSpecialCharactersFromString("_", new []{'$'});
            result.Should().Be("Mannus___$_Etten");
        }
        [Fact]
        public void Decrypt_With_Invalid_String()
        {
            var decryptText = "aby9qZ0pVNUrjTFIZWFz7Dngn6gJSkNsN8iqzX-A9hP1fThB6RN71u1KKl3-zqGvmt2CWHzft-YkDwHxM8PmHsIhMaRsRVw7rg7dn7ekMrIKgNZOdoIW_FpwfiEoq_uLCPA2h0QeIQMTlLOO5qbZAKscJt2qiCA_x1JjkWf0_MG7fASiHn3q1W8bCd98790Z5XvWq1tq0o7NGe43Z_O8Bb7SqyrFBMd_z12YpWryBab43-jfSz9g2eTvNl10lS9QEIhTVtVk4kiHhkxZBBRHppnEhyIW91Ri007hLa1xPGrnetricBWT-i9bBnWjnXxjd7nPF0DsehUjVbRtpezZz7vLg3UoAqIKaqLHVGFxhTjzs0OpMAIUi1KhGcU_snbvC_43xLnTtdpZNMIZow04jIFO8-sLWQ1Dl27f6bU5yJKx9gMGlObqwWcb7vy4wO2uG_1fBHo4EHPCXDODI8K9m5YHpkBdAacyKSvmUKr4yGQ.";
            var key = "Portal Genius";
            Assert.Throws<ArgumentException>(() => decryptText.Decrypt(key));
        }

        [Fact]
        public void ToSecureString_Gives_Error()
        {
            var code = "skul8NATH.hen1riss";
            var result = code.ToSecureString();
            result.Should().NotBeNull();
            var result2 = result.ToUnsecureString();
            result2.Should().Be(code);
        }

        [Fact]
        public void Decrypt_Does_Not_Cut_String()
        {
            var json = "{\"CustomerName\":\"Defensie\",\"CustomerId\":\"fc584745-1347-4579-87d2-56e93ca1d998\",\"Type\":2,\"ExpirationDate\":\"2025-10-24T00:00:00\",\"AnalyzerLicense\":{\"Status\":3,\"NumberOfAllowedPortals\":10},\"OfflineUsage\":false}";
            var passPhase = "PortalGenius";
            var encrypted = json.Encrypt(passPhase);
            var result = encrypted.Decrypt(passPhase);
            result.Should().BeEquivalentTo(json);
        }

        [Fact]
        public void IsValidJson()
        {
            var json =
                "[{\"name\":\"webhook1\",\"owner\":\"MannusEtten\",\"id\":2,\"globalId\":\"ba2f6e1e-75c1-4889-a00e-ee4d78b8d0c0\",\"tenantId\":27238,\"serviceId\":9,\"active\":false,\"hookUrl\":\"www.mannus.nl\",\"serviceUrl\":\"https://services.arcgis.com/v16XTZeIhHAZEpwh/arcgis/rest/services/punt_lijn_vlak/FeatureServer\",\"signatureKey\":\"\",\"format\":\"json\",\"serverGen\":17004,\"contentType\":\"application/x-www-form-urlencoded\",\"createdTime\":1633183581723,\"lastUpdatedTime\":1633183581723,\"changeTypes\":[\"All\"],\"scheduleInfo\":{\"name\":\"\",\"state\":\"enabled\",\"recurrenceInfo\":{\"frequency\":\"second\",\"interval\":20}}},{\"name\":\"webhook2\",\"owner\":\"MannusEtten\",\"id\":3,\"globalId\":\"b7b47c78-4b4d-463d-89b9-3ac0f632ee89\",\"tenantId\":27238,\"serviceId\":9,\"active\":false,\"hookUrl\":\"www.basketbalnieuws.nl\",\"serviceUrl\":\"https://services.arcgis.com/v16XTZeIhHAZEpwh/ArcGIS/rest/services/punt_lijn_vlak/FeatureServer\",\"signatureKey\":\"\",\"format\":\"json\",\"serverGen\":17004,\"contentType\":\"application/x-www-form-urlencoded\",\"createdTime\":1633183631181,\"lastUpdatedTime\":1633183735028,\"changeTypes\":[\"FeaturesCreated\",\"FeaturesUpdated\",\"FeaturesDeleted\"],\"scheduleInfo\":{\"name\":\"\",\"state\":\"enabled\",\"recurrenceInfo\":{\"frequency\":\"second\",\"interval\":20}}}]";
            json.IsValidJson().Should().BeTrue();
        }
    }
}