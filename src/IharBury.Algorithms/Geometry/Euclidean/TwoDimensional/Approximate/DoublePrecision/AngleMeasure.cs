﻿namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    /// <summary>
    /// Represents the measure of an angle in a two-dimensional Euclidean space
    /// by its approximate measure in radians with <see cref="double"/> precision.
    /// </summary>
    public struct AngleMeasure
    {
        public AngleMeasure(double inRadians)
        {
            InRadians = inRadians;
        }

        /// <summary>
        /// The approximate measure of the angle in radians.
        /// </summary>
        public double InRadians { get; }

        public override string ToString() => $"{InRadians}";
    }
}
