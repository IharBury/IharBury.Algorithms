namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a triangle in a two-dimensional Euclidean space
    /// by its vertices with approximate Cartesian coordinates                                                      
    /// with <see cref="double"/> precision.
    /// </summary>
    public struct Triangle
    {
        /// <summary>
        /// Constructs a new triangle from its vertices.
        /// </summary>
        public Triangle(Point baseVertex, Point clockwiseVertex, Point anticlockwiseVertex)
        {
            BaseVertex = baseVertex;
            ClockwiseVertex = clockwiseVertex;
            AnticlockwiseVertex = anticlockwiseVertex;
        }

        /// <summary>
        /// One of the vertices of the triangle.
        /// </summary>
        public Point BaseVertex { get; }

        /// <summary>
        /// The next vertex clockwise after <see cref="BaseVertex"/>.
        /// </summary>
        public Point ClockwiseVertex { get; }

        /// <summary>
        /// The next vertex anticlockwise after <see cref="BaseVertex"/>.
        /// </summary>
        public Point AnticlockwiseVertex { get; }

        public override string ToString() => $"({BaseVertex}, {ClockwiseVertex}, {AnticlockwiseVertex})";

        /// <summary>
        /// Approximate area of the triangle.
        /// </summary>
        public double Area =>
            (ClockwiseFreeVector.Y * AnticlockwiseFreeVector.X - ClockwiseFreeVector.X * AnticlockwiseFreeVector.Y) / 2;

        /// <summary>
        /// Free vector from <see cref="BaseVertex"/> to <see cref="ClockwiseVertex"/>.
        /// </summary>
        public FreeVector ClockwiseFreeVector => BaseVertex.GetFreeVectorTo(ClockwiseVertex);

        /// <summary>
        /// Free vector from <see cref="BaseVertex"/> to <see cref="AnticlockwiseVertex"/>.
        /// </summary>
        public FreeVector AnticlockwiseFreeVector => BaseVertex.GetFreeVectorTo(AnticlockwiseVertex);
    }
}
