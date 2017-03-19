using System;
using System.Collections.Generic;
using System.Text;

namespace IharBury.Algorithms
{
    public static class DoubleExtensions
    {
        public static bool IsInfinity(this double value) => double.IsInfinity(value);
        public static bool IsPositiveInfinity(this double value) => double.IsPositiveInfinity(value);
        public static bool IsNegativeInfinity(this double value) => double.IsNegativeInfinity(value);
        public static bool IsNaN(this double value) => double.IsNaN(value);
        public static bool IsFiniteNumber(this double value) => !value.IsNaN() && !value.IsInfinity();
    }
}
