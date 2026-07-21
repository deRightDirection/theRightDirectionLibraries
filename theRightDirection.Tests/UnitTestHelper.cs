using Meziantou.Framework.Win32;
using theRightDirection.KvKConnector;

namespace theRightDirection.Tests;

public abstract class UnitTestHelper
{
    public KvKApiClient GetKvKClient()
    {
        var kvkApiKey = CredentialManager.ReadCredential("kvkApiKey").Password.ToSecureString();
        return new KvKApiClient(kvkApiKey);
    }
}
