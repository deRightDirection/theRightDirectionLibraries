using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace theRightDirection;

public static partial class Extensions

{
    /// <summary>
    /// Using the hash values of the certificates to determine equality, returns a boolean value indicating whether the <see cref="X509CertificateCollection"/> contains the specified <see cref="X509Certificate"/>.
    /// </summary>
    /// <param name="certificates">The certificate collection to locate the certificate in.</param>
    /// <param name="certificate">The certificate to locate.</param>
    /// <returns></returns>
    public static bool ContainsByCertHash(this X509CertificateCollection certificates, X509Certificate certificate)
    {
        var certHash = certificate.GetCertHash();
        for (var i = 0; i < certificates.Count; i++)
        {
            if (certificates[i].GetCertHash().SequenceEqual(certHash))
            {
                return true;
            }
        }
        return false;
    }
}