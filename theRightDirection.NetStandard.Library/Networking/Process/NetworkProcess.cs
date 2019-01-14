namespace theRightDirection.Networking.Process
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the model for a network process.
    /// </summary>
    public abstract class NetworkProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcess"/> class.
        /// </summary>
        protected NetworkProcess()
            : this(Guid.NewGuid().ToString(), true, 3, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcess"/> class.
        /// </summary>
        /// <param name="queueId">
        /// The queue identifier.
        /// </param>
        protected NetworkProcess(string queueId)
            : this(queueId, true, 3, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcess"/> class.
        /// </summary>
        /// <param name="queueId">
        /// The queue identifier.
        /// </param>
        /// <param name="retryOnFail">
        /// Whether to retry on fail.
        /// </param>
        /// <param name="retryLimit">
        /// The retry limit.
        /// </param>
        /// <param name="headers">
        /// Additional headers.
        /// </param>
        protected NetworkProcess(string queueId, bool retryOnFail, int retryLimit, Dictionary<string, string> headers)
        {
            this.QueueId = queueId;
            this.RetryOnFail = retryOnFail;
            this.RetryLimit = retryLimit;
            this.Headers = headers;
        }

        /// <summary>
        /// Gets the current retry count.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Gets the time of the last retry.
        /// </summary>
        public DateTime LastRetry { get; set; }

        /// <summary>
        /// Gets or sets the URL of the network process.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the queue ID.
        /// </summary>
        public string QueueId { get; protected set; }

        /// <summary>
        /// Gets or sets the retry limit.
        /// </summary>
        public int RetryLimit { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether to retry when failed.
        /// </summary>
        public bool RetryOnFail { get; protected set; }

        /// <summary>
        /// Gets or sets the headers for the process.
        /// </summary>
        public Dictionary<string, string> Headers { get; protected set; }

        /// <summary>
        /// Gets the retry interval.
        /// </summary>
        public TimeSpan RetryInterval => TimeSpan.FromMinutes(2);

        /// <summary>
        /// Gets a value indicating whether a retry is available.
        /// </summary>
        public bool CanRetry => this.RetryOnFail && this.RetryCount <= this.RetryLimit;

        /// <summary>
        /// Processes the network request.
        /// </summary>
        /// <typeparam name="TResponse">
        /// The type of object returned from the request.
        /// </typeparam>
        /// <returns>
        /// Returns an await-able task wrapping the response.
        /// </returns>
        public abstract Task<TResponse> ProcessRequestAsync<TResponse>();

        /// <summary>
        /// Processes the network request.
        /// </summary>
        /// <param name="responseType">
        /// The expected response type.
        /// </param>
        /// <returns>
        /// Returns an await-able task wrapping the response.
        /// </returns>
        public abstract Task<object> ProcessRequestAsync(Type responseType);
    }
}