namespace theRightDirection.Xamarin.Xaml.Converters
{
    using global::Xamarin.Forms;
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines a value converter for checking whether a value is not null.
    /// </summary>
    public sealed class InverseNullToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Checks whether the <see cref="value"/> is not null.
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
        /// Returns true if not null; else false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="InverseNullToBooleanConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}