using System;
using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a point in a two-dimensional Euclidean space
    /// by its approximate Cartesian coordinates with <see cref="double"/> precision.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Origin of the space. Has zero coordinates.
        /// </summary>
        public static Point Origin => default(Point);

        /// <summary>
        /// Creates a point by its coordinates.
        /// </summary>
        /// <param name="x">The approximate x coordinate.</param>
        /// <param name="y">The approximate y coordinate.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The approximate x coordinate.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The approximate y coordinate.
        /// </summary>
        public double Y { get; }

        public override string ToString() => $"({X}, {Y})";

        /// <summary>
        /// Calculates approximate squared distance to another point.
        /// </summary>
        public double GetSquaredDistanceTo(Point other)
        {
            var xDifference = other.X - X;
            var yDifference = other.Y - Y;
            return xDifference * xDifference + yDifference * yDifference;
        }

        /// <summary>
        /// Calculates approximate distance to another point.
        /// Less optimal than <see cref="GetSquaredDistanceTo(Point)"/>.
        /// </summary>
        public double GetDistanceTo(Point other) => Sqrt(GetSquaredDistanceTo(other));

        /// <summary>
        /// Determines whether the point has approximately the same coordinates as <paramref name="other"/>
        /// with the given max squared distance error.
        /// </summary>
        /// <param name="maxSquaredDistanceError">The max squared distance error. Must be a non-negative finite number.</param>
        public bool HasSameCoordinatesAs(Point other, double maxSquaredDistanceError)
        {
            if (!maxSquaredDistanceError.IsFiniteNumber() || (maxSquaredDistanceError < 0))
                throw new ArgumentOutOfRangeException(nameof(maxSquaredDistanceError));
            return GetSquaredDistanceTo(other) <= maxSquaredDistanceError;
        }
    }
}
