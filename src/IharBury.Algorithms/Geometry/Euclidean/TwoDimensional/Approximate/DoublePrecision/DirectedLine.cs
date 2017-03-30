﻿namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
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

        /// <summary>
        /// Determines whether the given point is to the left with the given max squared distance error.
        /// </summary>
        public bool HasToTheLeft(Point point, double maxSquaredDistanceError)
        {
            var xDifference1 = BasePoint.X - point.X;
            var yDifference1 = BasePoint.Y - ForwardPoint.Y;
            var xDifference2 = BasePoint.X - ForwardPoint.X;
            var yDifference2 = BasePoint.Y - point.Y;
            var areaFactor = xDifference1 * yDifference1 - xDifference2 * yDifference2;
            if (areaFactor > 0)
                return true;
            var squaredDistance = areaFactor * areaFactor / BasePoint.GetSquaredDistanceTo(ForwardPoint);
            return squaredDistance <= maxSquaredDistanceError;
        }
    }
}
