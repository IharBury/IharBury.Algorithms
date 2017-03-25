using System;
using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a free vector in a two-dimensional Euclidean space
    /// by its approximate Cartesian coordinates with <see cref="double"/> precision.
    /// </summary>
    public struct FreeVector
    {
        public static FreeVector operator +(FreeVector vector1, FreeVector vector2) =>
            new FreeVector(vector1.X + vector2.X, vector1.Y + vector2.Y);

        public static FreeVector operator -(FreeVector vector1, FreeVector vector2) =>
            new FreeVector(vector1.X - vector2.X, vector1.Y - vector2.Y);

        /// <summary>
        /// Zero free vector.
        /// </summary>
        public static FreeVector Zero => default(FreeVector);

        /// <summary>
        /// Creates a free vector by its coordinates.
        /// </summary>
        /// <param name="x">The approximate x coordinate.</param>
        /// <param name="y">The approximate y coordinate.</param>
        public FreeVector(double x, double y)
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
        /// Determines whether the free vector has approximately the same coordinates as <paramref name="other"/>
        /// with the given max error squared length.
        /// </summary>
        /// <param name="maxErrorSquaredLength">The max error squared length. Must be a non-negative finite number.</param>
        public bool HasSameCoordinatesAs(FreeVector other, double maxErrorSquaredLength)
        {
            if (!maxErrorSquaredLength.IsFiniteNumber() || (maxErrorSquaredLength < 0))
                throw new ArgumentOutOfRangeException(nameof(maxErrorSquaredLength));

            return (other - this).SquaredLength <= maxErrorSquaredLength;
        }

        /// <summary>
        /// Approximate squared length of the free vector.
        /// </summary>
        public double SquaredLength => X * X + Y * Y;

        /// <summary>
        /// Approximate length of the free vector.
        /// Less optimal than <see cref="SquaredLength"/>.
        /// </summary>
        public double Length => Sqrt(SquaredLength);
    }
}
