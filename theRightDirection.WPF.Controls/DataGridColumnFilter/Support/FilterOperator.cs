using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace theRightDirection.WPF.Xaml.Controls.DataGridColumnFilter.Support
{
    public enum FilterOperator
    {
        Undefined,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Equals,
        Like,
        Between
    }
}
