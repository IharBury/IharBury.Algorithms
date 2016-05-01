using System;

namespace IharBury.Algorithms
{
    public struct Line2DInt16 : IEquatable<Line2DInt16>
    {
        public static bool operator ==(Line2DInt16 line1, Line2DInt16 line2)
        {
            return line1.Equals(line2);
        }

        public static bool operator !=(Line2DInt16 line1, Line2DInt16 line2)
        {
            return !(line1 == line2);
        }

        public Line2DInt16(Point2DInt16 point1, Point2DInt16 point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public Point2DInt16 Point1 { get; }
        public Point2DInt16 Point2 { get; }

        public bool IsValid => Point1 != Point2;

        public override string ToString() => IsValid ? $"({Point1}, {Point2})" : "not a line";

        public bool HasPoint(Point2DInt16 point)
        {
            checked
            {
                var xDifference1 = Point1.X - point.X;
                var yDifference1 = Point1.Y - Point2.Y;
                var xDifference2 = Point1.X - Point2.X;
                var yDifference2 = Point1.Y - point.Y;

                return xDifference1 * (long)yDifference1 == xDifference2 * (long)yDifference2;
            }
        }

        public bool Equals(Line2DInt16 other)
        {
            if ((other.Point1 == Point1) && (other.Point2 == Point2))
                return true;
            if (!other.IsValid || !IsValid)
                return false;
            return HasPoint(other.Point1) && HasPoint(other.Point2);
        }

        public override bool Equals(object obj) => (obj is Line2DInt16) && Equals((Line2DInt16)obj);

        public override int GetHashCode()
        {
            if (!IsValid)
                return Point1.GetHashCode();

            checked
            {
                var xDifference = Point2.X - Point1.X;
                var yDifference = Point2.Y - Point1.Y;
                var xPerYFactor = yDifference == 0 ? (int?)null : xDifference / yDifference;
                var yPerXFactor = xDifference == 0 ? (int?)null : yDifference / xDifference;
                var xWhenYIsZero = yDifference == 0 ?
                    (long?)null :
                    (Point1.X * (long)yDifference - Point1.Y * (long)xDifference) / yDifference;
                var yWhenXIsZero = xDifference == 0 ?
                    (long?)null :
                    (Point1.Y * (long)xDifference - Point1.X * (long)yDifference) / xDifference;

                unchecked
                {
                    var result = xPerYFactor.GetHashCode();
                    result = (result * 37987) ^ yPerXFactor.GetHashCode();
                    result = (result * 37987) ^ xWhenYIsZero.GetHashCode();
                    result = (result * 37987) ^ yWhenXIsZero.GetHashCode();
                    return result;
                }
            }
        }
    }
}
