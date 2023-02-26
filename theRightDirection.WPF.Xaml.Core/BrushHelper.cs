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