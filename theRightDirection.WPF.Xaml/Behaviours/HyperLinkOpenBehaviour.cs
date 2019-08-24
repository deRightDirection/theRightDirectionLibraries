using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using Microsoft.Xaml.Behaviors;

namespace theRightDirection.WPF.Xaml.Behaviour
{
    public class HyperlinkOpenBehaviour : Behavior<Hyperlink>
    {
        public static readonly DependencyProperty ConfirmNavigationProperty = DependencyProperty.Register(
            nameof(ConfirmNavigation), typeof(bool), typeof(HyperlinkOpenBehaviour), new PropertyMetadata(default(bool)));

        public bool ConfirmNavigation
        {
            get { return (bool)GetValue(ConfirmNavigationProperty); }
            set { SetValue(ConfirmNavigationProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.RequestNavigate += NavigationRequested;
            AssociatedObject.Unloaded += AssociatedObjectOnUnloaded;
            base.OnAttached();
        }

        private void AssociatedObjectOnUnloaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Unloaded -= AssociatedObjectOnUnloaded;
            AssociatedObject.RequestNavigate -= NavigationRequested;
        }

        private void NavigationRequested(object sender, RequestNavigateEventArgs e)
        {
            if (!ConfirmNavigation || MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OpenUrl();
            }
            e.Handled = true;
        }

        private void OpenUrl()
        {
            Process.Start(new ProcessStartInfo(AssociatedObject.NavigateUri.AbsoluteUri));
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RequestNavigate -= NavigationRequested;
            base.OnDetaching();
        }
    }
}