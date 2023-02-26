using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace theRightDirection.WPF.Xaml.Converters
{
    public sealed class InverseNullToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Checks whether the specified <see cref="value"/> is null.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target Type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns true if null; else false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="NullToBooleanConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}