using System;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Precise.Int16Precision
{
    public struct Point : IEquatable<Point>
    {
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }

        public Point(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X { get; }
        public short Y { get; }

        public bool Equals(Point other) => (other.X == X) && (other.Y == Y);

        public override bool Equals(object obj) => (obj is Point) && Equals((Point)obj);

        public override int GetHashCode() => unchecked((X * 37987) ^ Y);

        public override string ToString() => $"({X}, {Y})";

        public bool IsAtLine(Line line) => line.HasPoint(this);

        public bool IsToTheLeftOf(Point backPoint, Point forwardPoint)
        {
            checked
            {
                var xDifference1 = backPoint.X - X;
                var yDifference1 = backPoint.Y - forwardPoint.Y;
                var xDifference2 = backPoint.X - forwardPoint.X;
                var yDifference2 = backPoint.Y - Y;

                return xDifference1 * (long)yDifference1 > xDifference2 * (long)yDifference2;
            }
        }
    }
}
