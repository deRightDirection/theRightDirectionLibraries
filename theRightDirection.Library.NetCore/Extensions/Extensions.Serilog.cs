using System.IO;
using System.Runtime.CompilerServices;
using Serilog;

namespace theRightDirection;
public static partial class Extensions
{
    public static ILogger Here(this ILogger logger, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        var y = Path.GetFileNameWithoutExtension(sourceFilePath);
        return logger.ForContext("MethodInformation", $"{y}.{memberName}:{sourceLineNumber}");
    }
}