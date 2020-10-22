using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace theRightDirection.Library
{
    public static partial class Extensions
    {
        public static void InvokeIfRequired(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher == null)
            {
                return;
            }
            if (!dispatcher.CheckAccess())
            {
                dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
                return;
            }
            action();
        }
    }
}