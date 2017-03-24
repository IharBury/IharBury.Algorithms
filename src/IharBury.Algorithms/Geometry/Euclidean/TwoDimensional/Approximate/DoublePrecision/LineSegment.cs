using System;

using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a line segment in a two-dimensional Euclidean space
    /// by its endpoints with approximate Cartesian coordinates                                                      
    /// with <see cref="double"/> precision.
    /// </summary>
    public struct LineSegment
    {
        /// <summary>
        /// Constructs a new line segment from its endpoints.
        /// </summary>
        public LineSegment(Point baseEndpoint, Point anotherEndpoint)
        {
            BaseEndpoint = baseEndpoint;
            AnotherEndpoint = anotherEndpoint;
        }

        /// <summary>
        /// One of the endpoints of the line segment.
        /// </summary>
        public Point BaseEndpoint { get; }

        /// <summary>
        /// The endpoint opposite to <see cref="BaseEndpoint"/>.
        /// </summary>
        public Point AnotherEndpoint { get; }

        /// <summary>
        /// Calculates approximate squared length.
        /// </summary>
        public double SquaredLength => BaseEndpoint.GetSquaredDistanceTo(AnotherEndpoint);

        /// <summary>
        /// Calculates approximate length.
        /// Less optimal than <see cref="SquaredLength"/>.
        /// </summary>
        public double Length => Sqrt(SquaredLength);

        /// <summary>
        /// The middle point.
        /// </summary>
        public Point Middle => new Point((BaseEndpoint.X + AnotherEndpoint.X) / 2, (BaseEndpoint.Y + AnotherEndpoint.Y) / 2);

        public override string ToString() => $"({BaseEndpoint},{AnotherEndpoint})";

        /// <summary>
        /// Determines whether the line segment is approximately equal to <paramref name="other"/>
        /// with the given max square distance error.
        /// </summary>
        /// <param name="maxSquareDistanceError">The max square distance error. Must be a non-negative finite number.</param>
        public bool EqualsWithMaxSquaredDistanceError(LineSegment other, double maxSquareDistanceError)
        {
            if (!maxSquareDistanceError.IsFiniteNumber() || (maxSquareDistanceError < 0))
                throw new ArgumentOutOfRangeException(nameof(maxSquareDistanceError));

            if (BaseEndpoint.EqualsWithMaxSquaredDistanceError(other.BaseEndpoint, maxSquareDistanceError) &&
                AnotherEndpoint.EqualsWithMaxSquaredDistanceError(other.AnotherEndpoint, maxSquareDistanceError))
                return true;
            if (BaseEndpoint.EqualsWithMaxSquaredDistanceError(other.AnotherEndpoint, maxSquareDistanceError) &&
                AnotherEndpoint.EqualsWithMaxSquaredDistanceError(other.BaseEndpoint, maxSquareDistanceError))
                return true;
            return false;
        }
    }
}
