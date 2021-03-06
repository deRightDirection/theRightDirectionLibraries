﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Diagnostics;

namespace theRightDirection.WPF.Xaml.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public BooleanToVisibilityConverter()
        {
            HiddenState = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typeOfValue = value.GetType();
            bool convertValue = false;
            if (typeOfValue == typeof(string))
            {
                bool.TryParse(value.ToString(), out convertValue);
            }
            if (typeOfValue == typeof(bool))
            {
                convertValue = (bool)value;
            }
            if (convertValue)
            {
                return Visibility.Visible;
            }
            return HiddenState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return visibility == Visibility.Visible;
        }

        public Visibility HiddenState { get; set; }
    }
}