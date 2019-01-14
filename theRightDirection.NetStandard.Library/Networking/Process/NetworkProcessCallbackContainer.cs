namespace theRightDirection.Networking.Process
{
    using System;

    using theRightDirection.Common;

    /// <summary>
    /// Defines a container for a <see cref="NetworkProcess"/> callback.
    /// </summary>
    public sealed class NetworkProcessCallbackContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcessCallbackContainer"/> class.
        /// </summary>
        /// <param name="networkProcess">
        /// The network process.
        /// </param>
        public NetworkProcessCallbackContainer(NetworkProcess networkProcess)
            : this(networkProcess, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcessCallbackContainer"/> class.
        /// </summary>
        /// <param name="networkProcess">
        /// The network process.
        /// </param>
        /// <param name="callback">
        /// The success callback.
        /// </param>
        public NetworkProcessCallbackContainer(NetworkProcess networkProcess, WeakCallback callback) : this(networkProcess, callback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcessCallbackContainer"/> class.
        /// </summary>
        /// <param name="networkProcess">
        /// The network process.
        /// </param>
        /// <param name="callback">
        /// The success callback.
        /// </param>
        /// <param name="errorCallback">
        /// The error callback.
        /// </param>
        public NetworkProcessCallbackContainer(
            NetworkProcess networkProcess,
            WeakCallback callback,
            WeakCallback errorCallback)
        {
            if (networkProcess == null) throw new ArgumentNullException(nameof(networkProcess));

            this.NetworkProcess = networkProcess;
            this.Callback = callback;
            this.ErrorCallback = errorCallback;
        }

        /// <summary>
        /// Gets the network process.
        /// </summary>
        public NetworkProcess NetworkProcess { get; }

        /// <summary>
        /// Gets the callback.
        /// </summary>
        public WeakCallback Callback { get; }

        /// <summary>
        /// Gets the error callback.
        /// </summary>
        public WeakCallback ErrorCallback { get; }
    }
}