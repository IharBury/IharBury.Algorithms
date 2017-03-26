using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a circular sector in a two-dimensional Euclidean space
    /// by the center of its circle, its clockwise vertex with approximate Cartesian coordinates
    /// and its approximate central angle with <see cref="double"/> precision.
    /// </summary>
    public struct CircularSector
    {
        /// <summary>
        /// Constructs a new circular sector from its circle center, clockwise vertex and central angle.
        /// </summary>
        public CircularSector(Point circleCenter, Point clockwiseVertex, AngleMeasure centralAngle)
        {
            CircleCenter = circleCenter;
            ClockwiseVertex = clockwiseVertex;
            CentralAngle = centralAngle;
        }

        /// <summary>
        /// The center of the circle.
        /// </summary>
        public Point CircleCenter { get; }

        /// <summary>
        /// The next vertex clockwise from <see cref="CircleCenter"/>.
        /// </summary>
        public Point ClockwiseVertex { get; }

        /// <summary>
        /// The central angle.
        /// </summary>
        public AngleMeasure CentralAngle { get; }

        public override string ToString() => $"({CircleCenter}, {ClockwiseVertex}, {CentralAngle})";

        /// <summary>
        /// The bounding radius line segment which is clockwise after <see cref="CircleCenter"/>.
        /// </summary>
        public LineSegment ClockwiseRadius => new LineSegment(CircleCenter, ClockwiseVertex);

        /// <summary>
        /// The approximate squared length of the radius of the circle.
        /// </summary>
        public double SquaredRadiusLength => ClockwiseRadius.SquaredLength;

        /// <summary>
        /// The approximate length of the radius of the circle.
        /// Less optimal than <see cref="SquaredRadiusLength"/>.
        /// </summary>
        public double RadiusLength => Sqrt(SquaredRadiusLength);

        /// <summary>
        /// The circle of the circular sector.
        /// </summary>
        public Circle Circle => new Circle(CircleCenter, RadiusLength);

        /// <summary>
        /// The approximate area of the circular sector.
        /// </summary>
        public double Area => CentralAngle.InRadians / 2 * SquaredRadiusLength;
    }
}
