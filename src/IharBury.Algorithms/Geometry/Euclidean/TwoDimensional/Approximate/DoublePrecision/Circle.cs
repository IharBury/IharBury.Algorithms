using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a circle in a two-dimensional Euclidean space
    /// by its center with approximate Cartesian coordinates
    /// and its approximate radius
    /// with <see cref="double"/> precision.
    /// </summary>
    public struct Circle
    {
        /// <summary>
        /// Constructs a new circle from its center and approximate radius with <see cref="double"/> precision.
        /// </summary>
        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// The center of the circle.
        /// </summary>
        public Point Center { get; }

        /// <summary>
        /// The approximate radius of the circle.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// The approximate area of the circle.
        /// </summary>
        public double Area => PI * Radius * Radius;

        public override string ToString() => $"({Center}, {Radius})";
    }
}
