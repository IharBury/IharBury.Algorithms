using System;
using System.Linq;

using static System.Math;

namespace IharBury.Algorithms
{
    public static class Int64Extensions
    {
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

        public static long GetPageCount(this long value, long pageSize)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return value.DivideAndRoundUp(pageSize);
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

        public static long GetPageIndex(this long itemIndex, long pageSize)
        {
            if (itemIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(itemIndex));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return itemIndex / pageSize;
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
