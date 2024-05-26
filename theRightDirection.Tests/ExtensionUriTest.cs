using FluentAssertions;
using theRightDirection;

namespace Extensions;
public class ExtensionUriTest
{
    [Fact]
    public void IsInvalidAbsoluteUri_With_RelativeUrl_Is_True()
    {
        var baseUri = new Uri("http://localhost/relatief");
        var baseUri2 = new Uri("http://localhost/");
        var testUri = baseUri.MakeRelativeUri(baseUri2);
        testUri.IsInvalidAbsoluteUri().Should().BeTrue();
    }
    [Fact]
    public void IsInvalidAbsoluteUri_With_Null_Is_True()
    {
        Uri uri = null;
        uri.IsInvalidAbsoluteUri().Should().BeTrue();
    }
    [Fact]
    public void IsValidAbsoluteUri_With_AbsoluteUri_Is_True()
    {
        var baseUri = new Uri("http://localhost/relatief");
        baseUri.IsValidAbsoluteUri().Should().BeTrue();
    }
    [Fact]
    public void IsValidAbsoluteUri_With_RelativeUrl_Is_False()
    {
        var baseUri = new Uri("http://localhost/relatief");
        var baseUri2 = new Uri("http://localhost/");
        var testUri = baseUri.MakeRelativeUri(baseUri2);
        testUri.IsValidAbsoluteUri().Should().BeFalse();
    }
    [Fact]
    public void IsValidAbsoluteUri_With_Null_Is_False()
    {
        Uri uri = null;
        uri.IsValidAbsoluteUri().Should().BeFalse();
    }
}
