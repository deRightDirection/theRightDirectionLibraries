using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace theRightDirection.WPF.Xaml.Converters
{
    public class BooleansEvaluationConverter : IMultiValueConverter
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
                return false;
            }
            if (EvaluationMode == BooleansEvaluationMode.And)
            {
                return allBooleans.TrueForAll(x => x == true);
            }
            if (EvaluationMode == BooleansEvaluationMode.Or)
            {
                return allBooleans.Any(x => x == true);
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}