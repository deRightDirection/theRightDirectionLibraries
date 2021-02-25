using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library
{
    public static partial class Extensions
    {
        public static double TimeStampInMilliseconds(this DateTime dateTime)
        {
            var utcTime = dateTime.ToUniversalTime();
            var t = (utcTime - new DateTime(1970, 1, 1));
            return Math.Truncate(t.TotalMilliseconds);
        }
    }
}