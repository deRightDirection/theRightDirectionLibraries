using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace theRightDirection.WPF.Xaml.Converters
{
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibilityState = Visibility.Collapsed;
            if (parameter != null)
            {
                visibilityState = (Visibility)parameter;
            }
            return !(bool)value ? Visibility.Visible : visibilityState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return visibility == Visibility.Visible ? true : false;
        }
    }
}