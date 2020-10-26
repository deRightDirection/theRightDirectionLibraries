using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Newtonsoft.Json.Linq;

namespace theRightDirection.WPF.Xaml.Converters
{
    public sealed class JArrayLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var jToken = value as JToken;
            if(jToken == null)
                throw new Exception("Wrong type for this converter");

            switch (jToken.Type)
            {
                case JTokenType.Array:
                    var arrayLen = jToken.Children().Count();
                    return string.Format("[{0}]", arrayLen);
                case JTokenType.Property:
                    var propertyArrayLen = jToken.Children().FirstOrDefault().Children().Count();
                    return string.Format("[ {0} ]", propertyArrayLen);
                default:
                    throw new Exception("Type should be JProperty or JArray");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
