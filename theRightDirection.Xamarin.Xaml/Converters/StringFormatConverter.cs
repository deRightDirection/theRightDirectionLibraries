using System;
using System.Globalization;
using Xamarin.Forms;

namespace theRightDirection.Xamarin.Xaml.Converters
{
    /// <summary>
    /// Format the binded value with the given format in de converterparameter, it uses stringformat internally
    /// 1) add in main.xaml
    ///        <TextBlock Text = "{Binding Amount, Mode=OneWay, Converter={StaticResource StringFormatConverter}, ConverterParameter=\{0:C\}}" />
    /// 2) add in MainViewModel
    ///    Amount = 12.345;
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = parameter as string;
            if (format.Contains("{0:"))
            {
                return string.Format(format, value);
            }
            if (format.Contains("{0}"))
            {
                return string.Format(format, value);
            }
            var formatString = "{0:" + format + "}";
            return string.Format(CultureInfo.CurrentUICulture, formatString, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}