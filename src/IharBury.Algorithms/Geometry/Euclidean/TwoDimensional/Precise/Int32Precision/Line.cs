using System;
using System.Numerics;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision
{
    public struct Line : IEquatable<Line>
    {
        public static bool operator ==(Line line1, Line line2)
        {
            return line1.Equals(line2);
        }

        public static bool operator !=(Line line1, Line line2)
        {
            return !(line1 == line2);
        }

        public Line(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public Point Point1 { get; }
        public Point Point2 { get; }

        public bool IsValid => Point1 != Point2;

        public override string ToString() => IsValid ? $"({Point1}, {Point2})" : "not a line";

        public bool HasPoint(Point point)
        {
            checked
            {
                var xDifference1 = Point1.X - (long)point.X;
                var yDifference1 = Point1.Y - (long)Point2.Y;
                var xDifference2 = Point1.X - (long)Point2.X;
                var yDifference2 = Point1.Y - (long)point.Y;

                return xDifference1 * (BigInteger)yDifference1 == xDifference2 * (BigInteger)yDifference2;
            }
        }

        public bool Equals(Line other)
        {
            if ((other.Point1 == Point1) && (other.Point2 == Point2))
                return true;
            if (!other.IsValid || !IsValid)
                return false;
            return HasPoint(other.Point1) && HasPoint(other.Point2);
        }

        public override bool Equals(object obj) => (obj is Line line) && Equals(line);

        public override int GetHashCode()
        {
            if (!IsValid)
                return Point1.GetHashCode();

            checked
            {
                var xDifference = Point2.X - (long)Point1.X;
                var yDifference = Point2.Y - (long)Point1.Y;
                var xPerYFactor = yDifference == 0 ? (long?)null : xDifference / yDifference;
                var yPerXFactor = xDifference == 0 ? (long?)null : yDifference / xDifference;
                var xWhenYIsZero = yDifference == 0 ?
                    (BigInteger?)null :
                    (Point1.X * (BigInteger)yDifference - Point1.Y * (BigInteger)xDifference) / yDifference;
                var yWhenXIsZero = xDifference == 0 ?
                    (BigInteger?)null :
                    (Point1.Y * (BigInteger)xDifference - Point1.X * (BigInteger)yDifference) / xDifference;

                unchecked
                {
                    var result = xPerYFactor.GetHashCode();
                    result = (result * 37987) + yPerXFactor.GetHashCode();
                    result = (result * 37987) + xWhenYIsZero.GetHashCode();
                    result = (result * 37987) + yWhenXIsZero.GetHashCode();
                    return result;
                }
            }
        }
    }
}
