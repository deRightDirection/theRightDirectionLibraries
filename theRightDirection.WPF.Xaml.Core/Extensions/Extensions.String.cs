﻿using System.Windows.Media;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// convert a hex code into a solidcolor brush
        /// </summary>
        public static SolidColorBrush ToSolidColorBrush(this string hex_code)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFromString(hex_code);
        }
    }
}