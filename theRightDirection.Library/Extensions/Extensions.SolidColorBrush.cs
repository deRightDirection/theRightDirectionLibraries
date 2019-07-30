using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace theRightDirection.Library.Extensions
{
    public static partial class Extensions
    {
        public static SolidColorBrush ToBrush(this string HexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(HexColorString));
        }
    }
}