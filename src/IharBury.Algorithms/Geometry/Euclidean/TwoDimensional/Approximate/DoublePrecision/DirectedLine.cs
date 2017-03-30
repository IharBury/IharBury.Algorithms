namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a directed line in a two-dimensional Euclidean space
    /// by two of its points with approximate Cartesian coordinates with <see cref="double"/> precision.
    /// </summary>
    public struct DirectedLine
    {
        public DirectedLine(Point basePoint, Point forwardPoint)
        {
            BasePoint = basePoint;
            ForwardPoint = forwardPoint;
        }

        /// <summary>
        /// One of the points of the directed line.
        /// </summary>
        public Point BasePoint { get; }

        /// <summary>
        /// One of the points of the directed line which is forward from <see cref="BasePoint"/>.
        /// </summary>
        public Point ForwardPoint { get; }

        public override string ToString() => $"({BasePoint}, {ForwardPoint})";
    }
}
