namespace theRightDirection.Networking
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using theRightDirection.Common;
    using theRightDirection.Networking.Process;

    /// <summary>
    /// Defines a helper for managing network requests.
    /// </summary>
    public sealed class NetworkManager
    {
        private readonly
            SortedDictionary<NetworkProcessPriority, ConcurrentDictionary<string, NetworkProcessCallbackContainer>>
            networkQueues =
                new SortedDictionary
                    <NetworkProcessPriority, ConcurrentDictionary<string, NetworkProcessCallbackContainer>>();

        private Timer timer;

        private bool isProcessing;

        /// <summary>
        /// Starts the processing of the network manager queues.
        /// </summary>
        public void Start()
        {
            if (this.timer == null)
            {
                this.timer = new Timer(this.Timer_OnTick, null, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(1));
            }
            else
            {
                this.timer.Change(TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(1));
            }
        }

        /// <summary>
        /// Stops the processing of the network manager queues.
        /// </summary>
        public void Stop()
        {
            this.timer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        private void Timer_OnTick(object state)
        {
            this.ProcessQueues();
        }

        private void ProcessQueues()
        {
            if (this.isProcessing)
            {
                return;
            }

            if (this.QueueSize == 0)
            {
                return;
            }

            this.isProcessing = true;

            try
            {
                this.ProcessQueueByPriority(NetworkProcessPriority.High);
                this.ProcessQueueByPriority(NetworkProcessPriority.Medium);
                this.ProcessQueueByPriority(NetworkProcessPriority.Low);
            }
            finally
            {
                this.isProcessing = false;
            }
        }

        /// <summary>
        /// Gets the current queue size.
        /// </summary>
        public int QueueSize => this.networkQueues.Values.Sum(queue => queue.Count);

        /// <summary>
        /// Adds a network process to the specified priority queue.
        /// </summary>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <typeparam name="TProcess">
        /// The network process type.
        /// </typeparam>
        /// <param name="callback">
        /// The action to execute when a response is acquired.
        /// </param>
        /// <param name="priority">
        /// The priority of the process.
        /// </param>
        /// <param name="process">
        /// The network process to execute.
        /// </param>
        public void AddProcess<TResponse, TProcess>(
            Action<TResponse> callback,
            NetworkProcessPriority priority,
            TProcess process) where TProcess : NetworkProcess
        {
            this.AddProcess<TProcess, TResponse, Exception>(process, callback, null, priority);
        }

        /// <summary>
        /// Adds a network process to the specified priority queue.
        /// </summary>
        /// <typeparam name="TProcess">
        /// The network process type.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <typeparam name="TErrorResponse">
        /// The expected errored response type.
        /// </typeparam>
        /// <param name="process">
        /// The network process to execute.
        /// </param>
        /// <param name="callback">
        /// The action to execute when a response is required.
        /// </param>
        /// <param name="errorCallback">
        /// The action to execute if the response errors.
        /// </param>
        /// <param name="priority">
        /// The priority of the process
        /// </param>
        public void AddProcess<TProcess, TResponse, TErrorResponse>(
            TProcess process,
            Action<TResponse> callback,
            Action<TErrorResponse> errorCallback,
            NetworkProcessPriority priority) where TProcess : NetworkProcess
        {
            var weakCallback = new WeakCallback();
            weakCallback.SetCallback<TResponse>(callback);

            var weakErrorCallback = new WeakCallback();
            weakErrorCallback.SetCallback<TErrorResponse>(errorCallback);

            var callbackContainer = new NetworkProcessCallbackContainer(process, weakCallback, weakErrorCallback);

            var queue = this.GetQueueByPriority(priority);

            queue.AddOrUpdate(
                callbackContainer.NetworkProcess.QueueId,
                callbackContainer,
                (key, val) => callbackContainer);
        }

        /// <summary>
        /// Processes the <see cref="NetworkManager"/> queue by the specified priority.
        /// </summary>
        /// <param name="priority">
        /// The priority of the queue to process.
        /// </param>
        public void ProcessQueueByPriority(NetworkProcessPriority priority)
        {
            var cancellationSource = new CancellationTokenSource();
            var queue = this.GetQueueByPriority(priority);

            var queueTasks = new List<Task>();
            var containers = new List<NetworkProcessCallbackContainer>();

            while (queue.Count > 0)
            {
                NetworkProcessCallbackContainer container;
                if (queue.TryRemove(queue.FirstOrDefault().Key, out container))
                {
                    containers.Add(container);
                }
            }

            foreach (var container in containers)
            {
                queueTasks.Add(ExecuteProcessAsync(queue, container, cancellationSource));
            }
        }

        private static async Task ExecuteProcessAsync(
            ConcurrentDictionary<string, NetworkProcessCallbackContainer> queue,
            NetworkProcessCallbackContainer container,
            CancellationTokenSource cancellationSource)
        {
            if (cancellationSource.IsCancellationRequested)
            {
                queue.AddOrUpdate(container.NetworkProcess.QueueId, container, (key, existingVal) => container);
                return;
            }

            var process = container.NetworkProcess;
            var callback = container.Callback;
            var errorCallback = container.ErrorCallback;

            if (DateTime.Now.Subtract(process.RetryInterval) <= process.LastRetry)
            {
                queue.AddOrUpdate(container.NetworkProcess.QueueId, container, (key, existingVal) => container);
                return;
            }

            try
            {
                process.LastRetry = DateTime.Now;

                var processResponse = await process.ProcessRequestAsync(callback.CallbackType);
                ExecuteCallback(callback, processResponse);
            }
            catch (Exception ex)
            {
                process.RetryCount++;

                if (process.CanRetry)
                {
                    queue.AddOrUpdate(container.NetworkProcess.QueueId, container, (key, existingVal) => container);
                }
                else
                {
                    ExecuteCallback(callback, Activator.CreateInstance(callback.CallbackType));
                    ExecuteCallback(errorCallback, ex);
                }
            }
        }

        /// <summary>
        /// Executes a network request bypassing the queue.
        /// </summary>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <param name="process">
        /// The network process.
        /// </param>
        /// <param name="cancellationSource">
        /// The task cancellation source.
        /// </param>
        /// <returns>
        /// Returns the result of the network request.
        /// </returns>
        public async Task<TResponse> ExecuteRequestAsync<TResponse>(
            NetworkProcess process,
            CancellationTokenSource cancellationSource)
        {
            return await process.ProcessRequestAsync<TResponse>();
        }

        private static void ExecuteCallback(WeakCallback callback, object processResponse)
        {
            if (callback == null) return;

            if (callback.IsAlive)
            {
                callback.FireCallback(processResponse);
            }
        }

        private ConcurrentDictionary<string, NetworkProcessCallbackContainer> GetQueueByPriority(
            NetworkProcessPriority priority)
        {
            if (!this.networkQueues.ContainsKey(priority))
            {
                this.networkQueues.Add(priority, new ConcurrentDictionary<string, NetworkProcessCallbackContainer>());
            }

            return this.networkQueues[priority];
        }
    }
}