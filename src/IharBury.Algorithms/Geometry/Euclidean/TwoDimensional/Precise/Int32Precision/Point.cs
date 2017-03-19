using System;
using System.Numerics;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision
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

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool Equals(Point other) => (other.X == X) && (other.Y == Y);

        public override bool Equals(object obj) => (obj is Point point) && Equals(point);

        public override int GetHashCode() => unchecked((X * 37987) + Y);

        public override string ToString() => $"({X}, {Y})";

        public bool IsAtLine(Line line) => line.HasPoint(this);

        public bool IsToTheLeftOf(Point backPoint, Point forwardPoint)
        {
            checked
            {
                var xDifference1 = backPoint.X - (long)X;
                var yDifference1 = backPoint.Y - (long)forwardPoint.Y;
                var xDifference2 = backPoint.X - (long)forwardPoint.X;
                var yDifference2 = backPoint.Y - (long)Y;

                return xDifference1 * (BigInteger)yDifference1 > xDifference2 * (BigInteger)yDifference2;
            }
        }
    }
}
