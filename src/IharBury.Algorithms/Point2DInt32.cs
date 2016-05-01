using System;
using System.Numerics;

namespace IharBury.Algorithms
{
    public struct Point2DInt32 : IEquatable<Point2DInt32>
    {
        public static bool operator ==(Point2DInt32 point1, Point2DInt32 point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point2DInt32 point1, Point2DInt32 point2)
        {
            return !(point1 == point2);
        }

        public Point2DInt32(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool Equals(Point2DInt32 other) => (other.X == X) && (other.Y == Y);

        public override bool Equals(object obj) => (obj is Point2DInt32) && Equals((Point2DInt32)obj);

        public override int GetHashCode() => unchecked((X * 37987) ^ Y);

        public override string ToString() => $"({X}, {Y})";

        public bool IsAtLine(Line2DInt32 line) => line.HasPoint(this);

        public bool IsToTheLeftOf(Point2DInt32 backPoint, Point2DInt32 forwardPoint)
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
