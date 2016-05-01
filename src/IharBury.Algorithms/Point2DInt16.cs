using System;

namespace IharBury.Algorithms
{
    public struct Point2DInt16 : IEquatable<Point2DInt16>
    {
        public static bool operator ==(Point2DInt16 point1, Point2DInt16 point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point2DInt16 point1, Point2DInt16 point2)
        {
            return !(point1 == point2);
        }

        public Point2DInt16(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X { get; }
        public short Y { get; }

        public bool Equals(Point2DInt16 other) => (other.X == X) && (other.Y == Y);

        public override bool Equals(object obj) => (obj is Point2DInt16) && Equals((Point2DInt16)obj);

        public override int GetHashCode() => unchecked((X * 37987) ^ Y);

        public override string ToString() => $"({X}, {Y})";

        public bool IsAtLine(Line2DInt16 line) => line.HasPoint(this);

        public bool IsToTheLeftOf(Point2DInt16 backPoint, Point2DInt16 forwardPoint)
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
