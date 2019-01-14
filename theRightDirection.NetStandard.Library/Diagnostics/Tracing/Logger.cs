namespace theRightDirection.Diagnostics.Tracing
{
    using System.Diagnostics.Tracing;

    /// <summary>
    /// Defines an event logger for Windows event tracing.
    /// </summary>
    public sealed class Logger : EventSource
    {
        private static Logger current;

        /// <summary>
        /// Gets an instance of the event logger.
        /// </summary>
        public static Logger Current => current ?? (current = new Logger());

        /// <summary>
        /// Writes a debug event to the log.
        /// </summary>
        /// <param name="message">
        /// The debug message to write out.
        /// </param>
        /// <remarks>
        /// This will only write out if the application is running in debug mode.
        /// </remarks>
        [Event(1, Level = EventLevel.Verbose)]
        public void WriteDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Debug: {message}");
            this.WriteEvent(1, message);
        }

        /// <summary>
        /// Writes an informational event to the log.
        /// </summary>
        /// <param name="message">
        /// The informational message to write out.
        /// </param>
        [Event(2, Level = EventLevel.Informational)]
        public void WriteInfo(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Info: {message}");
            this.WriteEvent(2, message);
        }

        /// <summary>
        /// Writes a warning event to the log.
        /// </summary>
        /// <param name="message">
        /// The warning message to write out.
        /// </param>        
        [Event(3, Level = EventLevel.Warning)]
        public void WriteWarning(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Warning: {message}");
            this.WriteEvent(3, message);
        }

        /// <summary>
        /// Writes an error event to the log.
        /// </summary>
        /// <param name="message">
        /// The error message to write out.
        /// </param>
        [Event(4, Level = EventLevel.Error)]
        public void WriteError(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {message}");
            this.WriteEvent(4, message);
        }

        /// <summary>
        /// Writes a critical event to the log.
        /// </summary>
        /// <param name="message">
        /// The critical message to write out.
        /// </param>
        [Event(5, Level = EventLevel.Critical)]
        public void WriteCritical(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Critical: {message}");
            this.WriteEvent(5, message);
        }
    }
}