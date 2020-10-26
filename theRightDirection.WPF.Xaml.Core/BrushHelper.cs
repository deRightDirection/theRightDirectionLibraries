using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace theRightDirection.WPF.Xaml
{
    public static class BrushHelper
    {
        public static SolidColorBrush HexCodeToSolidColorBrush(string hexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexColorString));
        }
    }
}