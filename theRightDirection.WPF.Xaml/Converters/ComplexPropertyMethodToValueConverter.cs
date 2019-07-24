using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Newtonsoft.Json.Linq;

namespace theRightDirection.WPF.Xaml.Converters
{
    public class ComplexPropertyMethodToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string methodName = parameter as string;
            if (value == null || methodName == null)
                return null;
            var methodInfo = value.GetType().GetMethod(methodName, new Type[0]);
            if (methodInfo == null)
                return null;
            var invocationResult = methodInfo.Invoke(value, new object[0]);
            var jTokens = (IEnumerable<JToken>) invocationResult;
            return jTokens.First().Children() ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}