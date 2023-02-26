using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace theRightDirection.WPF.Xaml.Converters
{
    public sealed class JValueTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var jValue = value as JValue;
            if (jValue != null)
            {
                switch (jValue.Type)
                {
                    case JTokenType.String:
                        return new BrushConverter().ConvertFrom("#4e9a06");

                    case JTokenType.Float:
                    case JTokenType.Integer:
                        return new BrushConverter().ConvertFrom("#ad7fa8");

                    case JTokenType.Boolean:
                        return new BrushConverter().ConvertFrom("#c4a000");

                    case JTokenType.Null:
                        return new SolidColorBrush(Colors.OrangeRed);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}