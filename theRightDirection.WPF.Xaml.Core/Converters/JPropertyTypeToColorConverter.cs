using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace theRightDirection.WPF.Xaml.Converters
{
    public sealed class JPropertyTypeToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var jprop = values[0] as JProperty;
            if (jprop != null)
            {
                if (values.Length != 5)
                {
                    return Brushes.Black;
                }
                switch (jprop.Value.Type)
                {
                    case JTokenType.String:
                        return BrushHelper.HexCodeToSolidColorBrush(values[1].ToString());

                    case JTokenType.Integer:
                        return BrushHelper.HexCodeToSolidColorBrush(values[2].ToString());

                    case JTokenType.Boolean:
                        return BrushHelper.HexCodeToSolidColorBrush(values[3].ToString());

                    case JTokenType.Null:
                        return BrushHelper.HexCodeToSolidColorBrush(values[4].ToString());
                }
            }
            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}