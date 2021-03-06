﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xaml;

namespace theRightDirection.WPF.Xaml.Converters
{
    /// <summary>
    /// convert a string value to a resource
    /// </summary>
    public class StaticResourceConverter : MarkupExtension, IValueConverter
    {
        private Control _target;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resourceKey = (string)value;
            return _target?.FindResource(resourceKey) ?? Application.Current.FindResource(resourceKey);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var rootObjectProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (rootObjectProvider == null)
                return this;

            _target = rootObjectProvider.RootObject as Control;
            return this;
        }
    }
}