namespace theRightDirection
{
    using System;

    using theRightDirection.Maths;

    /// <summary>
    /// Defines a collection of extensions for handling extended math functions.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Performs a basic calculation of linear interpolation between two values.
        /// </summary>
        /// <param name="start">
        /// The start value.
        /// </param>
        /// <param name="end">
        /// The end value.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// Returns the calculated result.
        /// </returns>
        public static float Lerp(this float start, float end, float amount)
        {
            var difference = end - start;
            var adjusted = difference * amount;
            return start + adjusted;
        }

        /// <summary>
        /// Checks whether a double value is zero.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// Returns true if zero; else false.
        /// </returns>
        public static bool IsZero(this double value)
        {
            return Math.Abs(value) < MathHelper.Epsilon;
        }

        /// <summary>
        /// Checks whether a float value is zero.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// Returns true if zero; else false.
        /// </returns>
        public static bool IsZero(this float value)
        {
            return Math.Abs(value) < MathHelper.Epsilon;
        }

        /// <summary>
        /// Checks whether a double value is not a number.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// Returns true if not a number; else false.
        /// </returns>
        public static bool IsNaN(this double value)
        {
            // Get the double as an unsigned long
            var union = new NanUnion { FloatingValue = value };

            // An IEEE 754 double precision floating point number is NaN if its
            // exponent equals 2047 and it has a non-zero mantissa.
            var exponent = union.IntegerValue & 0xfff0000000000000L;
            if ((exponent != 0x7ff0000000000000L) && (exponent != 0xfff0000000000000L))
            {
                return false;
            }
            var mantissa = union.IntegerValue & 0x000fffffffffffffL;
            return mantissa != 0L;
        }

        /// <summary>
        /// Converts a miles <see cref="double"/> value to meters.
        /// </summary>
        /// <param name="miles">
        /// The miles to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the meters.
        /// </returns>
        public static double ToMeters(this double miles)
        {
            return miles / 0.00062137;
        }

        /// <summary>
        /// Converts a miles <see cref="int"/> value to meters.
        /// </summary>
        /// <param name="miles">
        /// The miles to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the meters.
        /// </returns>
        public static double ToMeters(this int miles)
        {
            return miles / 0.00062137;
        }

        /// <summary>
        /// Converts a meters <see cref="double"/> value to miles.
        /// </summary>
        /// <param name="meters">
        /// The meters to convert.
        /// </param>
        /// <returns>
        /// Returns an <see cref="int"/> value representing the miles.
        /// </returns>
        public static int ToMiles(this double meters)
        {
            return (int)(meters * 0.00062137);
        }

        /// <summary>
        /// Converts a degrees <see cref="double"/> value to radians.
        /// </summary>
        /// <param name="degrees">
        /// The degrees to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the radians.
        /// </returns>
        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}