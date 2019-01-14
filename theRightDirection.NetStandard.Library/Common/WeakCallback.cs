namespace theRightDirection.Common
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines a model for providing a weak callback.
    /// </summary>
    public sealed class WeakCallback
    {
        private MethodInfo callbackMethod;

        private Type callbackType;

        private WeakReference callbackTarget;

        /// <summary>
        /// Sets the callback.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="TEventArgs">
        /// The expected type for the callback.
        /// </typeparam>
        public void SetCallback<TEventArgs>(Delegate action)
        {
            this.callbackMethod = action.GetMethodInfo();
            this.callbackTarget = new WeakReference(action.Target);
            this.callbackType = typeof(TEventArgs);
        }

        /// <summary>
        /// Gets the callback target.
        /// </summary>
        public object CallbackTarget => this.callbackTarget.Target;

        /// <summary>
        /// Gets the callback type.
        /// </summary>
        public Type CallbackType => this.callbackType;

        /// <summary>
        /// Gets a value indicating whether the callback is alive.
        /// </summary>
        public bool IsAlive => this.callbackTarget.IsAlive;

        /// <summary>
        /// Fires the callback with the specified argument.
        /// </summary>
        /// <param name="args">
        /// The callback argument.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// InvalidOperationException thrown if the argument type is not the expected or the callback could not be found.
        /// </exception>
        public void FireCallback(object args)
        {
            if (args != null && args.GetType() != this.callbackType)
            {
                throw new InvalidOperationException(
                          $"Expected parameter type of {this.callbackType}. Parameter type provided is {args.GetType()}");
            }

            if (this.callbackTarget.IsAlive)
            {
                var methodArgs = new[] { args };

                this.callbackMethod.Invoke(this.callbackTarget.Target, methodArgs);
            }
            else
            {
                throw new InvalidOperationException("The callback target could not be found.");
            }
        }
    }
}