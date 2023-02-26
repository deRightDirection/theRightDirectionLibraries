using System;
using System.Globalization;
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

            if (CheckIfStringIsEmpty)
            {
                var valueAsString = value as string;
                if (string.IsNullOrEmpty(valueAsString))
                {
                    return visibilityState;
                }
            }
            return value != null ? Visibility.Visible : visibilityState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// indien deze waarde True is dan wordt de waarde niet alleen gecheckt of het null is, maar ook of het string.empty is
        /// </summary>
        public bool CheckIfStringIsEmpty { get; set; }
    }
}