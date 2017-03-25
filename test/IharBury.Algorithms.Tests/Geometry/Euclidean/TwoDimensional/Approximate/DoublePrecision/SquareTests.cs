﻿using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class SquareTests
    {
        [Fact]
        public void ConstructedSquareHasTheGivenBaseVertexX()
        {
            Assert.Equal(2, new Square(new Point(2, 3), new Point(4, 7)).BaseVertex.X);
        }

        [Fact]
        public void ConstructedSquareHasTheGivenBaseVertexY()
        {
            Assert.Equal(3, new Square(new Point(2, 3), new Point(4, 7)).BaseVertex.Y);
        }

        [Fact]
        public void ConstructedSquareHasTheGivenOppositeVertexX()
        {
            Assert.Equal(4, new Square(new Point(2, 3), new Point(4, 7)).OppositeVertex.X);
        }

        [Fact]
        public void ConstructedSquareHasTheGivenOppositeVertexY()
        {
            Assert.Equal(7, new Square(new Point(2, 3), new Point(4, 7)).OppositeVertex.Y);
        }

        [Fact]
        public void DiagonalSquaredLengthIsCalculatedCorrectly()
        {
            Assert.Equal(20, new Square(new Point(2, 3), new Point(4, 7)).DiagonalSquaredLength, 5);
        }

        [Fact]
        public void DiagonalLengthIsCalculatedCorrectly()
        {
            Assert.Equal(4.4721359549995793928183473374626, new Square(new Point(2, 3), new Point(4, 7)).DiagonalLength, 5);
        }

        [Fact]
        public void SideSquaredLengthIsCalculatedCorrectly()
        {
            Assert.Equal(10, new Square(new Point(2, 3), new Point(4, 7)).SideSquaredLength, 5);
        }

        [Fact]
        public void SideLengthIsCalculatedCorrectly()
        {
            Assert.Equal(3.1622776601683793319988935444327, new Square(new Point(2, 3), new Point(4, 7)).SideLength, 5);
        }

        [Fact]
        public void AreaIsCalculatedCorrectly()
        {
            Assert.Equal(10, new Square(new Point(2, 3), new Point(4, 7)).Area, 5);
        }

        [Fact]
        public void BaseDiagonalIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            var expectedBaseDiagonal = new LineSegment(new Point(2, 3), new Point(4, 7));
            Assert.True(square.BaseDiagonal.HasSameCoordinatesAs(expectedBaseDiagonal, 0.00001));
        }

        [Fact]
        public void CenterIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            Assert.True(square.Center.HasSameCoordinatesAs(new Point(3, 5), 0.00001));
        }

        [Fact]
        public void ClockwiseVertexIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            Assert.True(square.ClockwiseVertex.HasSameCoordinatesAs(new Point(1, 6), 0.00001));
        }

        [Fact]
        public void AnticlockwiseVertexIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            Assert.True(square.AnticlockwiseVertex.HasSameCoordinatesAs(new Point(5, 4), 0.00001));
        }

        [Fact]
        public void AnotherDiagonalIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            var expectedAnotherDiagonal = new LineSegment(new Point(1, 6), new Point(5, 4));
            Assert.True(square.AnotherDiagonal.HasSameCoordinatesAs(expectedAnotherDiagonal, 0.00001));
        }

        [Fact]
        public void SquareHasSameCoordinatesAsCloseEnoughSquareWithSameVertexOrderWithTheGivenSquaredDistanceError()
        {
            var square1 = new Square(new Point(2.001, 3.001), new Point(6.001, -2.001));
            var square2 = new Square(new Point(2, 3), new Point(6, -2));
            Assert.True(square1.HasSameCoordinatesAs(square2, 0.00001));
        }

        [Fact]
        public void SquareHasSameCoordinatesAsCloseEnoughSquareWithAnotherVertexOrderWithTheGivenSquaredDistanceError()
        {
            var square1 = new Square(new Point(2.001, 3.001), new Point(6.001, -2.001));
            var square2 = new Square(new Point(6.5, 2.5), new Point(1.5, -1.5));
            Assert.True(square1.HasSameCoordinatesAs(square2, 0.00001));
        }

        [Fact]
        public void SquareDoesNotHaveSameCoordinatesAsFarEnoughSquareWithTheGivenSquaredDistanceError()
        {
            var square1 = new Square(new Point(2.01, 3.01), new Point(6.01, -2.01));
            var square2 = new Square(new Point(2, 3), new Point(6, -2));
            Assert.False(square1.HasSameCoordinatesAs(square2, 0.00001));
        }
    }
}
