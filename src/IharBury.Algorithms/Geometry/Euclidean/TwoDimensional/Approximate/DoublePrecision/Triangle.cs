using System;

using static System.Math;

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
        public double Area
        {
            get
            {
                var clockwiseVectorX = ClockwiseVertex.X - BaseVertex.X;
                var clockwiseVectorY = ClockwiseVertex.Y - BaseVertex.Y;
                var anticlockwiseVectorX = AnticlockwiseVertex.X - BaseVertex.X;
                var anticlockwiseVectorY = AnticlockwiseVertex.Y - BaseVertex.Y;
                return (clockwiseVectorY * anticlockwiseVectorX - clockwiseVectorX * anticlockwiseVectorY) / 2;
            }
        }
    }
}
