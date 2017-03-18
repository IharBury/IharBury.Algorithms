using System;
using System.Linq;

using static System.Math;

namespace IharBury.Algorithms
{
    public static class NumberExtensions
    {
        public static long Abs(this int value) => Math.Abs((long)value);

        public static long Gcd(this int value1, int value2)
        {
            if ((value1 == 0) && (value2 == 0))
                throw new ArgumentException("Both values are zeros.");

            checked
            {
                var absValue1 = value1.Abs();
                var absValue2 = value2.Abs();

                var lowerValue = absValue1.ButMax(absValue2);
                var higherValue = absValue1.ButMin(absValue2);

                while (lowerValue != 0)
                {
                    var remainder = higherValue % lowerValue;
                    higherValue = lowerValue;
                    lowerValue = remainder;
                }

                return higherValue;
            }
        }

        public static long Lcm(this int value1, int value2)
        {
            if (value1 == 0)
                throw new ArgumentException("Value is zero.", nameof(value1));
            if (value2 == 0)
                throw new ArgumentException("Value is zero.", nameof(value2));

            return checked(value1 / value1.Gcd(value2) * value2);
        }

        /// <summary>
        /// Limits the given value to be not greater than the given max value.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimit">The max limit.</param>
        /// <returns>The value if it does not violate the limit, otherwise the limit.</returns>
        public static int ButMax(this int limitedValue, int maxLimit) => Min(limitedValue, maxLimit);

        /// <summary>
        /// Limits the given value to be not greater than the given max values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimit1">One of the max limits.</param>
        /// <param name="maxLimit2">One of the max limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the min of the limits.</returns>
        public static int ButMax(this int limitedValue, int maxLimit1, int maxLimit2) =>
            limitedValue.ButMax(maxLimit1.ButMax(maxLimit2));

        /// <summary>
        /// Limits the given value to be not greater than the given max values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimit1">One of the max limits.</param>
        /// <param name="maxLimit2">One of the max limits.</param>
        /// <param name="maxLimit3">One of the max limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the min of the limits.</returns>
        public static int ButMax(this int limitedValue, int maxLimit1, int maxLimit2, int maxLimit3) =>
            limitedValue.ButMax(maxLimit1.ButMax(maxLimit2.ButMax(maxLimit3)));

        /// <summary>
        /// Limits the given value to be not greater than the given max values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimits">Zero or more max limits. Cannot be null.</param>
        /// <returns>The value if it does not violate the limits, otherwise the min of the limits.</returns>
        public static int ButMax(this int limitedValue, params int[] maxLimits) =>
            new[] { limitedValue }.Concat(maxLimits).Min();

        /// <summary>
        /// Limits the given value to be not less than the given min value.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimit">The min limit.</param>
        /// <returns>The value if it does not violate the limit, otherwise the limit.</returns>
        public static int ButMin(this int limitedValue, int minLimit) => Max(limitedValue, minLimit);

        /// <summary>
        /// Limits the given value to be not less than the given min values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimit1">One of the min limits.</param>
        /// <param name="minLimit2">One of the min limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the max of the limits.</returns>
        public static int ButMin(this int limitedValue, int minLimit1, int minLimit2) =>
            limitedValue.ButMin(minLimit1.ButMin(minLimit2));

        /// <summary>
        /// Limits the given value to be not less than the given min values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimit1">One of the min limits.</param>
        /// <param name="minLimit2">One of the min limits.</param>
        /// <param name="minLimit3">One of the min limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the max of the limits.</returns>
        public static int ButMin(this int limitedValue, int minLimit1, int minLimit2, int minLimit3) =>
            limitedValue.ButMin(minLimit1.ButMin(minLimit2.ButMin(minLimit3)));

        /// <summary>
        /// Limits the given value to be not less than the given min values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimits">Zero or more min limits. Cannot be null.</param>
        /// <returns>The value if it does not violate the limits, otherwise the max of the limits.</returns>
        public static int ButMin(this int limitedValue, params int[] minLimits) =>
            new[] { limitedValue }.Concat(minLimits).Max();

        /// <summary>
        /// Limits the given value to be not greater than the given max value.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimit">The max limit.</param>
        /// <returns>The value if it does not violate the limit, otherwise the limit.</returns>
        public static long ButMax(this long limitedValue, long maxLimit) => Min(limitedValue, maxLimit);

        /// <summary>
        /// Limits the given value to be not greater than the given max values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimit1">One of the max limits.</param>
        /// <param name="maxLimit2">One of the max limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the min of the limits.</returns>
        public static long ButMax(this long limitedValue, long maxLimit1, long maxLimit2) =>
            limitedValue.ButMax(maxLimit1.ButMax(maxLimit2));

        /// <summary>
        /// Limits the given value to be not greater than the given max values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimit1">One of the max limits.</param>
        /// <param name="maxLimit2">One of the max limits.</param>
        /// <param name="maxLimit3">One of the max limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the min of the limits.</returns>
        public static long ButMax(this long limitedValue, long maxLimit1, long maxLimit2, long maxLimit3) => 
            limitedValue.ButMax(maxLimit1.ButMax(maxLimit2.ButMax(maxLimit3)));

        /// <summary>
        /// Limits the given value to be not greater than the given max values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="maxLimits">Zero or more max limits. Cannot be null.</param>
        /// <returns>The value if it does not violate the limits, otherwise the min of the limits.</returns>
        public static long ButMax(this long limitedValue, params long[] maxLimits) =>
            new[] { limitedValue }.Concat(maxLimits).Min();

        /// <summary>
        /// Limits the given value to be not less than the given min value.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimit">The min limit.</param>
        /// <returns>The value if it does not violate the limit, otherwise the limit.</returns>
        public static long ButMin(this long limitedValue, long minLimit) => Max(limitedValue, minLimit);

        /// <summary>
        /// Limits the given value to be not less than the given min values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimit1">One of the min limits.</param>
        /// <param name="minLimit2">One of the min limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the max of the limits.</returns>
        public static long ButMin(this long limitedValue, long minLimit1, long minLimit2) =>
            limitedValue.ButMin(minLimit1.ButMin(minLimit2));

        /// <summary>
        /// Limits the given value to be not less than the given min values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimit1">One of the min limits.</param>
        /// <param name="minLimit2">One of the min limits.</param>
        /// <param name="minLimit3">One of the min limits.</param>
        /// <returns>The value if it does not violate the limits, otherwise the max of the limits.</returns>
        public static long ButMin(this long limitedValue, long minLimit1, long minLimit2, long minLimit3) => 
            limitedValue.ButMin(minLimit1.ButMin(minLimit2.ButMin(minLimit3)));

        /// <summary>
        /// Limits the given value to be not less than the given min values.
        /// </summary>
        /// <param name="limitedValue">The value to be limited.</param>
        /// <param name="minLimits">Zero or more min limits. Cannot be null.</param>
        /// <returns>The value if it does not violate the limits, otherwise the max of the limits.</returns>
        public static long ButMin(this long limitedValue, params long[] minLimits) =>
            new[] { limitedValue }.Concat(minLimits).Max();

        public static long DivideAndRoundUp(this long value1, long value2)
        {
            if (value2 == 0)
                throw new DivideByZeroException();

            checked
            {
                var quotient = value1 / value2;
                var remainder = value1 % value2;
                return remainder > 0 ? quotient + 1 : quotient;
            }
        }

        public static int DivideAndRoundUp(this int value1, int value2)
        {
            checked
            {
                var quotient = value1 / value2;
                var remainder = value1 % value2;
                return remainder > 0 ? quotient + 1 : quotient;
            }
        }

        public static int GetPageCount(this int value, int pageSize)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return value.DivideAndRoundUp(pageSize);
        }

        public static long GetPageCount(this long value, long pageSize)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return value.DivideAndRoundUp(pageSize);
        }

        public static int GetLastPageSize(this int value, int pageSize)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            if (value == 0)
                return 0;
            var remainder = value % pageSize;
            return remainder == 0 ? pageSize : remainder;
        }

        public static int GetLastPageSize(this long value, int pageSize)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            if (value == 0)
                return 0;
            var remainder = checked((int)(value % pageSize));
            return remainder == 0 ? pageSize : remainder;
        }

        public static long GetLastPageSize(this long value, long pageSize)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            if (value == 0)
                return 0;
            var remainder = value % pageSize;
            return remainder == 0 ? pageSize : remainder;
        }

        public static int GetPageIndex(this int itemIndex, int pageSize)
        {
            if (itemIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(itemIndex));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return itemIndex / pageSize;
        }

        public static long GetPageIndex(this long itemIndex, long pageSize)
        {
            if (itemIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(itemIndex));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return itemIndex / pageSize;
        }

        public static int GetIndexOnPage(this int itemIndex, int pageSize)
        {
            if (itemIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(itemIndex));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return itemIndex % pageSize;
        }

        public static int GetIndexOnPage(this long itemIndex, int pageSize)
        {
            if (itemIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(itemIndex));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return checked((int)(itemIndex % pageSize));
        }

        public static long GetIndexOnPage(this long itemIndex, long pageSize)
        {
            if (itemIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(itemIndex));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return itemIndex % pageSize;
        }
    }
}
