using System;
using static System.Math;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents a square in a two-dimensional Euclidean space
    /// by two of its opposite vertices with approximate Cartesian coordinates                                                      
    /// with <see cref="double"/> precision.
    /// </summary>
    public struct Square
    {
        /// <summary>
        /// Constructs a new square from its base vertex and the opposite vertex.
        /// </summary>
        public Square(Point baseVertex, Point oppositeVertex)
        {
            BaseVertex = baseVertex;
            OppositeVertex = oppositeVertex;
        }

        /// <summary>
        /// One of the vertices of the square.
        /// </summary>
        public Point BaseVertex { get; }

        /// <summary>
        /// The vertex opposite to <see cref="BaseVertex"/>.
        /// </summary>
        public Point OppositeVertex { get; }

        /// <summary>
        /// Calculates approximate squared length of the diagonals.
        /// </summary>
        public double DiagonalSquaredLength => BaseVertex.GetSquaredDistanceTo(OppositeVertex);

        /// <summary>
        /// Calculates approximate length of the diagonals.
        /// Less optimal than <see cref="DiagonalSquaredLength"/>.
        /// </summary>
        public double DiagonalLength => Sqrt(DiagonalSquaredLength);

        /// <summary>
        /// Calculates approximate squared length of the sides.
        /// </summary>
        public double SideSquaredLength => DiagonalSquaredLength / 2;

        /// <summary>
        /// Calculates approximate length of the sides.
        /// Less optimal than <see cref="SideSquaredLength"/>.
        /// </summary>
        public double SideLength => Sqrt(SideSquaredLength);

        /// <summary>
        /// Calculates approximate area.
        /// </summary>
        public double Area => SideSquaredLength;

        /// <summary>
        /// Diagonal that contains <see cref="BaseVertex"/>.
        /// </summary>
        public LineSegment BaseDiagonal => new LineSegment(BaseVertex, OppositeVertex);

        /// <summary>
        /// The center point.
        /// </summary>
        public Point Center => BaseDiagonal.Middle;

        /// <summary>
        /// The next vertex clockwise after <see cref="BaseVertex"/>.
        /// Less optimal than <see cref="BaseVertex"/> and <see cref="OppositeVertex"/>.
        /// </summary>
        public Point ClockwiseVertex => new Point(Center.X - Center.Y + BaseVertex.Y, Center.Y + Center.X - BaseVertex.X);

        /// <summary>
        /// The next vertex anticlockwise after <see cref="BaseVertex"/>.
        /// Less optimal than <see cref="BaseVertex"/> and <see cref="OppositeVertex"/>.
        /// </summary>
        public Point AnticlockwiseVertex => new Point(Center.X + Center.Y - BaseVertex.Y, Center.Y - Center.X + BaseVertex.X);

        /// <summary>
        /// Diagonal that does not contain <see cref="BaseVertex"/>.
        /// Less optimal then <see cref="BaseDiagonal"/>.
        /// </summary>
        public LineSegment AnotherDiagonal => new LineSegment(ClockwiseVertex, AnticlockwiseVertex);

        public override string ToString() => $"({BaseVertex}, {ClockwiseVertex}, {OppositeVertex}, {AnticlockwiseVertex})";

        /// <summary>
        /// Determines whether the square has approximately the same coordinates as <paramref name="other"/>
        /// with the given max squared distance error.
        /// </summary>
        /// <param name="maxSquaredDistanceError">The max squared distance error. Must be a non-negative finite number.</param>
        public bool HasSameCoordinatesAs(Square other, double maxSquaredDistanceError)
        {
            if (!maxSquaredDistanceError.IsFiniteNumber() || (maxSquaredDistanceError < 0))
                throw new ArgumentOutOfRangeException(nameof(maxSquaredDistanceError));

            return BaseDiagonal.HasSameCoordinatesAs(other.BaseDiagonal, maxSquaredDistanceError) ||
                BaseDiagonal.HasSameCoordinatesAs(other.AnotherDiagonal, maxSquaredDistanceError);
        }
    }
}
