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
                throw new ArgumentException($"({nameof(value1)} == 0) && ({nameof(value2)} == 0)");

            checked
            {
                var absValue1 = value1.Abs();
                var absValue2 = value2.Abs();

                var lowerValue = absValue1.Min(absValue2);
                var higherValue = absValue1.Max(absValue2);

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
                throw new ArgumentException($"{nameof(value1)} == 0", nameof(value1));
            if (value2 == 0)
                throw new ArgumentException($"{nameof(value2)} == 0", nameof(value2));

            return checked(value1 / value1.Gcd(value2) * value2);
        }

        public static int Min(this int value1, int value2) => Math.Min(value1, value2);

        public static int Min(this int value1, int value2, int value3) => value1.Min(value2.Min(value3));

        public static int Min(this int value1, int value2, int value3, int value4) => value1.Min(value2.Min(value3.Min(value4)));

        public static int Min(this int value1, params int[] otherValues) => new[] { value1 }.Concat(otherValues).Min();

        public static int Max(this int value1, int value2) => Math.Max(value1, value2);

        public static int Max(this int value1, int value2, int value3) => value1.Max(value2.Max(value3));

        public static int Max(this int value1, int value2, int value3, int value4) => value1.Max(value2.Max(value3.Max(value4)));

        public static int Max(this int value1, params int[] otherValues) => new[] { value1 }.Concat(otherValues).Max();

        public static long Min(this long value1, long value2) => Math.Min(value1, value2);

        public static long Min(this long value1, long value2, long value3) => value1.Min(value2.Min(value3));

        public static long Min(this long value1, long value2, long value3, long value4) => 
            value1.Min(value2.Min(value3.Min(value4)));

        public static long Min(this long value1, params long[] otherValues) => new[] { value1 }.Concat(otherValues).Min();

        public static long Max(this long value1, long value2) => Math.Max(value1, value2);

        public static long Max(this long value1, long value2, long value3) => value1.Max(value2.Max(value3));

        public static long Max(this long value1, long value2, long value3, long value4) => 
            value1.Max(value2.Max(value3.Max(value4)));

        public static long Max(this long value1, params long[] otherValues) => new[] { value1 }.Concat(otherValues).Max();
    }
}
