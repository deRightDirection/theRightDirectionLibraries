using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace theRightDirection.Tests.KvKConnector;

public class X509CertificateExtensionsTests
{
    [Fact]
    public void LoadFromEmbeddedResourceFiles_ByDefault_LoadsCertificates()
    {
        // Arrange
        var certificates = new X509CertificateCollection();

        // Act
        certificates.LoadFromEmbeddedResourceFiles();

        // Arrange
        Assert.AreEqual(2, certificates.Count);
    }

    [Fact]
    public void ContainsByCertHash_GivenNull_ThrowsException()
    {
        // Arrange
        var certificates = new X509CertificateCollection();
        certificates.LoadFromEmbeddedResourceFiles();

        // Act
        void action() => certificates.ContainsByCertHash(null);

        // Assert
        Assert.ThrowsException<NullReferenceException>(action);
    }

    [Fact]
    public void ContainsByCertHash_GivenNonExistingCertificate_ReturnsFalse()
    {
        // Arrange
        var certificates = new X509CertificateCollection();
        certificates.LoadFromEmbeddedResourceFiles();

        // Act
        var certificateToFind = LoadCertificateFromResourceFile("Staat_der_Nederlanden_Private_Services_CA_-_G1.crt");
        var certificateFound = certificates.ContainsByCertHash(certificateToFind);

        // Assert
        Assert.IsFalse(certificateFound);
    }

    [Fact]
    public void ContainsByCertHash_GivenExistingCertificate_ReturnsTrue()
    {
        // Arrange
        var certificates = new X509CertificateCollection();
        certificates.LoadFromEmbeddedResourceFiles();

        // Act
        var certificateToFind = LoadCertificateFromResourceFile("Staat_der_Nederlanden_Private_Root_CA_-_G1.crt");
        var certificateFound = certificates.ContainsByCertHash(certificateToFind);

        // Assert
        Assert.IsTrue(certificateFound);
    }

    private static X509Certificate LoadCertificateFromResourceFile(string certificateName)
    {
        using var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"HR.KvkConnector.Tests.Fixture.Certificates.{certificateName}");
        using var memoryStream = new MemoryStream();

        fileStream.CopyTo(memoryStream);
        return new(memoryStream.ToArray());
    }
}