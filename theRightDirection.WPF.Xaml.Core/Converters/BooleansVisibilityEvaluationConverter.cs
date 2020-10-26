using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace theRightDirection.WPF.Xaml.Converters
{
    /// <summary>
    /// afhankelijk van de evaluationmode wordt de visibility op visible gezet
    /// lijst met booleans en mode = and --> alle booleans moeten de waarde true hebben
    /// lijst met booleans en mode = or --> minimaal 1 boolean moet de waarde true hebben
    /// </summary>
    public class BooleansVisibilityEvaluationConverter : IMultiValueConverter
    {
        public BooleansEvaluationMode EvaluationMode { get; set; }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var allBooleans = new List<bool>();
            foreach (var val in values)
            {
                if (val is bool)
                {
                    allBooleans.Add((bool)val);
                }
            }
            if (allBooleans.Count != values.Length)
            {
                return Visibility.Hidden;
            }
            if (EvaluationMode == BooleansEvaluationMode.And)
            {
                if (allBooleans.TrueForAll(x => x == true))
                {
                    return Visibility.Visible;
                }
            }
            if (EvaluationMode == BooleansEvaluationMode.Or)
            {
                if (allBooleans.Any(x => x == true))
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}