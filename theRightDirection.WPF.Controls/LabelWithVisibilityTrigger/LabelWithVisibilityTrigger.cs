using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace theRightDirection.WPF.Xaml.Controls.LabelWithVisibilityTrigger
{
    public class LabelWithVisibilityTrigger : Label
    {
        public LabelWithVisibilityTrigger()
        {
            base.IsVisibleChanged += LabelWithVisibilityTrigger_IsVisibleChanged;
        }

        private void LabelWithVisibilityTrigger_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(VisibilityChangedEvent, this));
        }

        public static readonly RoutedEvent VisibilityChangedEvent
                = EventManager.RegisterRoutedEvent(
                    "VisibilityChanged",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(LabelWithVisibilityTrigger));

            public event RoutedEventHandler VisibilityChanged
            {
                add { AddHandler(VisibilityChangedEvent, value); }
                remove { RemoveHandler(VisibilityChangedEvent, value); }
            }
    }
}