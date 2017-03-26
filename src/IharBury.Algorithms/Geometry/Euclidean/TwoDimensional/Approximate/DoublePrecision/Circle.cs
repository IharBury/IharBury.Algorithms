using System;
using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a circle in a two-dimensional Euclidean space
    /// by its center with approximate Cartesian coordinates
    /// and its approximate radius length
    /// with <see cref="double"/> precision.
    /// </summary>
    public struct Circle
    {
        /// <summary>
        /// Constructs a new circle from its center and approximate radius length with <see cref="double"/> precision.
        /// </summary>
        public Circle(Point center, double radiusLength)
        {
            Center = center;
            RadiusLength = radiusLength;
        }

        /// <summary>
        /// The center of the circle.
        /// </summary>
        public Point Center { get; }

        /// <summary>
        /// The approximate radius length of the circle.
        /// </summary>
        public double RadiusLength { get; }

        /// <summary>
        /// The approximate area of the circle.
        /// </summary>
        public double Area => PI * RadiusLength * RadiusLength;

        public override string ToString() => $"({Center}, {RadiusLength})";

        /// <summary>
        /// Determines whether the circle has approximately the same coordinates as <paramref name="other"/>
        /// with the given max center squared distance error and max radius length error.
        /// </summary>
        /// <param name="maxCenterSquaredDistanceError">
        /// The max center squared distance error. Must be a non-negative finite number.
        /// </param>
        /// <param name="maxRadiusLengthError">
        /// The max radius length error. Must be a non-negative finite number.
        /// </param>
        public bool HasSameCoordinatesAs(Circle other, double maxCenterSquaredDistanceError, double maxRadiusLengthError)
        {
            if (!maxCenterSquaredDistanceError.IsFiniteNumber() || (maxCenterSquaredDistanceError < 0))
                throw new ArgumentOutOfRangeException(nameof(maxCenterSquaredDistanceError));
            if (!maxRadiusLengthError.IsFiniteNumber() || (maxRadiusLengthError < 0))
                throw new ArgumentOutOfRangeException(nameof(maxRadiusLengthError));

            return Center.HasSameCoordinatesAs(other.Center, maxCenterSquaredDistanceError) &&
                (Abs(RadiusLength - other.RadiusLength) <= maxRadiusLengthError);
        }
    }
}
