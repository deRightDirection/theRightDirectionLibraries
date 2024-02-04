using HR.KvkConnector.Model.Errors;
using System.Net;

namespace HR.KvkConnector
{
    [Obsolete("03-02-2024 niet meer gebruiken", true)]
    internal class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string message)
            : base(message) => StatusCode = statusCode;

        public ApiException(HttpStatusCode statusCode, ApiError error)
            : this(statusCode, error.ToString()) => Errors = new[] { error };

        public ApiException(HttpStatusCode statusCode, IEnumerable<ApiError> errors)
            : this(statusCode, errors.ToString()) => Errors = errors;

        /// <summary>
        /// The HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The individual errors that were returned by the API.
        /// </summary>
        public IEnumerable<ApiError> Errors { get; }
    }
}
