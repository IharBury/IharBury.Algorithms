using System;
using System.Linq;

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

        public static int ButMax(this int value1, int value2) => Math.Min(value1, value2);

        public static int ButMax(this int value1, int value2, int value3) => value1.ButMax(value2.ButMax(value3));

        public static int ButMax(this int value1, int value2, int value3, int value4) =>
            value1.ButMax(value2.ButMax(value3.ButMax(value4)));

        public static int ButMax(this int value1, params int[] otherValues) => new[] { value1 }.Concat(otherValues).Min();

        public static int ButMin(this int value1, int value2) => Math.Max(value1, value2);

        public static int ButMin(this int value1, int value2, int value3) => value1.ButMin(value2.ButMin(value3));

        public static int ButMin(this int value1, int value2, int value3, int value4) =>
            value1.ButMin(value2.ButMin(value3.ButMin(value4)));

        public static int ButMin(this int value1, params int[] otherValues) => new[] { value1 }.Concat(otherValues).Max();

        public static long ButMax(this long value1, long value2) => Math.Min(value1, value2);

        public static long ButMax(this long value1, long value2, long value3) => value1.ButMax(value2.ButMax(value3));

        public static long ButMax(this long value1, long value2, long value3, long value4) => 
            value1.ButMax(value2.ButMax(value3.ButMax(value4)));

        public static long ButMax(this long value1, params long[] otherValues) => new[] { value1 }.Concat(otherValues).Min();

        public static long ButMin(this long value1, long value2) => Math.Max(value1, value2);

        public static long ButMin(this long value1, long value2, long value3) => value1.ButMin(value2.ButMin(value3));

        public static long ButMin(this long value1, long value2, long value3, long value4) => 
            value1.ButMin(value2.ButMin(value3.ButMin(value4)));

        public static long ButMin(this long value1, params long[] otherValues) => new[] { value1 }.Concat(otherValues).Max();

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
