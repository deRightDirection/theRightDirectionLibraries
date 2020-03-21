using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace theRightDirection.WPF.Xaml.Converters
{
    public sealed class InverseNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibilityState = Visibility.Collapsed;
            if (parameter != null)
            {
                visibilityState = (Visibility)parameter;
            }
            return value != null ? Visibility.Visible : visibilityState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}