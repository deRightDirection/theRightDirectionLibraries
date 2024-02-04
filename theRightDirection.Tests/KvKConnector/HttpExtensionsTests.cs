using System.Net;

namespace theRightDirection.Tests.KvKConnector;

public class HttpExtensionsTests
{
    [Fact]
    public void AddTrustedRootCertificates_ByDefault_SetsServerCertificateValidationCallback()
    {
        // Arrange
        var httpRequest = WebRequest.CreateHttp("https://ssltest.kvk.nl");

        // Act
        var callbackBefore = httpRequest.ServerCertificateValidationCallback;
        httpRequest.AddTrustedRootCertificates();
        var callbackAfter = httpRequest.ServerCertificateValidationCallback;

        // Assert
        Assert.IsNull(callbackBefore);
        Assert.IsNotNull(callbackAfter);
    }

    #region Integration tests
    [Fact]
    public void ExecutingWebRequest_WithoutCallingAddTrustedRootCertificates_ThrowsException()
    {
        // Arrange
        var httpRequest = WebRequest.CreateHttp("https://ssltest.kvk.nl");

        // Act
        void action() => httpRequest.GetResponse();

        // Assert
        Assert.ThrowsException<WebException>(action);
    }

    [Fact]
    public void ExecutingWebRequest_AfterCallingAddTrustedRootCertificates_ReturnsResponse()
    {
        // Arrange
        var httpRequest = WebRequest.CreateHttp("https://ssltest.kvk.nl");
        httpRequest.AddTrustedRootCertificates();

        // Act
        using var httpResponse = httpRequest.GetResponse();

        // Assert
        Assert.IsNotNull(httpResponse);
    }
    #endregion
}