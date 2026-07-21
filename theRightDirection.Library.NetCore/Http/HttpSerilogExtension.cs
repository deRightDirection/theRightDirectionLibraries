using Serilog;

namespace theRightDirection.Http;

public static partial class HttpSerilogExtension
{
    public static ILogger Http(this ILogger logger, string id, string status)
    {
        return logger.ForContext("MethodInformation", $"{id} - {status}");
    }
}