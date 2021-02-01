using System;
using System.Collections.Generic;
using System.Text;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// convert a unix timestamp with milliseconds to a datetime-object
        /// </summary>
        public static DateTime TimeStampAsDateTime(this double unixTimeStamp)
        {
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return converted.AddMilliseconds(unixTimeStamp);
        }

        /// <summary>
        /// convert a unix timestamp with milliseconds to a datetime-object
        /// </summary>
        public static DateTime TimeStampAsDateTime(this long unixTimeStamp)
        {
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return converted.AddMilliseconds(unixTimeStamp);
        }

        public static DateTime TimeStampAsDateTime(this DateTime dateTime, double unixTimeStamp)
        {
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return converted.AddMilliseconds(unixTimeStamp);
        }
    }
}