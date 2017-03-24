using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class SquareSpecs
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
        public void BaseDiagonalHasBaseVertexXAsBaseEndpointX()
        {
            Assert.Equal(2, new Square(new Point(2, 3), new Point(4, 7)).BaseDiagonal.BaseEndpoint.X);
        }

        [Fact]
        public void BaseDiagonalHasBaseVertexYAsBaseEndpointY()
        {
            Assert.Equal(3, new Square(new Point(2, 3), new Point(4, 7)).BaseDiagonal.BaseEndpoint.Y);
        }

        [Fact]
        public void BaseDiagonalHasOppositeVertexXAsAnotherEndpointX()
        {
            Assert.Equal(4, new Square(new Point(2, 3), new Point(4, 7)).BaseDiagonal.AnotherEndpoint.X);
        }

        [Fact]
        public void BaseDiagonalHasOppositeVertexYAsAnotherEndpointY()
        {
            Assert.Equal(7, new Square(new Point(2, 3), new Point(4, 7)).BaseDiagonal.AnotherEndpoint.Y);
        }

        [Fact]
        public void CenterIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            Assert.True(square.Center.EqualsWithMaxSquaredDistanceError(new Point(3, 5), 0.00001));
        }

        [Fact]
        public void ClockwiseVertexIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            Assert.True(square.ClockwiseVertex.EqualsWithMaxSquaredDistanceError(new Point(1, 6), 0.00001));
        }

        [Fact]
        public void AnticlockwiseVertexIsCalculatedCorrectly()
        {
            var square = new Square(new Point(2, 3), new Point(4, 7));
            Assert.True(square.AnticlockwiseVertex.EqualsWithMaxSquaredDistanceError(new Point(5, 4), 0.00001));
        }
    }
}
