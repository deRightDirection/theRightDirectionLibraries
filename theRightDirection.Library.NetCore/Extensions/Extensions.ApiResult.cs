using Refit;
using System.Diagnostics;

namespace theRightDirection;

public static partial class Extensions
{
    public static string GetErrorMessage<T>(this ApiResponse<T> apiResponse)
    {
        var apiException = (ApiException)apiResponse.Error;
        return $"{apiException?.Content}, {apiResponse.Error?.ToStringDemystified()}";
    }
}