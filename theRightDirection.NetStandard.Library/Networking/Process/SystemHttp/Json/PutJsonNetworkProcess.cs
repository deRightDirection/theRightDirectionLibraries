namespace WtheRightDirection.Networking.Process.SystemHttp.Json
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using theRightDirection.Data.Serialization;
    using theRightDirection.Networking.Process;

    /// <summary>
    /// Defines a network process for a PUT with a JSON response.
    /// </summary>
    public sealed class PutJsonNetworkProcess : NetworkProcess
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="PutJsonNetworkProcess"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/>.
        /// </param>
        public PutJsonNetworkProcess(HttpClient client)
            : this(client, Guid.NewGuid().ToString(), true, 3, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutJsonNetworkProcess"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/>.
        /// </param>
        /// <param name="queueId">
        /// The queue identifier.
        /// </param>
        public PutJsonNetworkProcess(HttpClient client, string queueId)
            : this(client, queueId, true, 3, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutJsonNetworkProcess"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/>.
        /// </param>
        /// <param name="queueId">
        /// The queue identifier.
        /// </param>
        /// <param name="retryOnFail">
        /// A value indicating whether to retry on fail.
        /// </param>
        /// <param name="retryLimit">
        /// The limit to keep retrying.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        public PutJsonNetworkProcess(
            HttpClient client,
            string queueId,
            bool retryOnFail,
            int retryLimit,
            Dictionary<string, string> headers)
            : base(queueId, retryOnFail, retryLimit, headers)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            this.client = client;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Processes the network request.
        /// </summary>
        /// <typeparam name="TResponse">
        /// The type of object returned from the request.
        /// </typeparam>
        /// <returns>
        /// Returns an await-able task wrapping the response.
        /// </returns>
        public override async Task<TResponse> ProcessRequestAsync<TResponse>()
        {
            if (this.client == null) throw new InvalidOperationException("No HTTP client has been supplied for the PutJsonNetworkProcess.");

            var data = await this.GetResponse();

            return SerializationService.Json.Deserialize<TResponse>(data);
        }

        /// <summary>
        /// Processes the network request.
        /// </summary>
        /// <param name="responseType">
        /// The expected response type.
        /// </param>
        /// <returns>
        /// Returns an await-able task wrapping the response.
        /// </returns>
        public override async Task<object> ProcessRequestAsync(Type responseType)
        {
            if (this.client == null) throw new InvalidOperationException("No HTTP client has been supplied for the PutJsonNetworkProcess.");

            var data = await this.GetResponse();

            return SerializationService.Json.Deserialize(data, responseType);
        }

        private async Task<string> GetResponse()
        {
            if (this.client == null) throw new InvalidOperationException("No HTTP client has been supplied for the PutJsonNetworkProcess.");
            if (string.IsNullOrWhiteSpace(this.Url)) throw new InvalidOperationException("No URL has been supplied for the PutJsonNetworkProcess.");

            var uri = new Uri(this.Url);

            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content =
                                      new StringContent(
                                      this.Data,
                                      Encoding.UTF8,
                                      "application/json")
            };

            if (this.Headers != null)
            {
                foreach (var header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = await this.client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}