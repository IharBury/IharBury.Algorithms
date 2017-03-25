using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class TriangleTests
    {
        [Fact]
        public void ConstructedTriangleHasTheGivenBaseVertexX()
        {
            Assert.Equal(2, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).BaseVertex.X);
        }

        [Fact]
        public void ConstructedTriangleHasTheGivenBaseVertexY()
        {
            Assert.Equal(3, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).BaseVertex.Y);
        }

        [Fact]
        public void ConstructedTriangleHasTheGivenClockwiseVertexX()
        {
            Assert.Equal(4, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).ClockwiseVertex.X);
        }

        [Fact]
        public void ConstructedTriangleHasTheGivenClockwiseVertexY()
        {
            Assert.Equal(7, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).ClockwiseVertex.Y);
        }

        [Fact]
        public void ConstructedTriangleHasTheGivenAnticlockwiseVertexX()
        {
            Assert.Equal(6, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).AnticlockwiseVertex.X);
        }

        [Fact]
        public void ConstructedTriangleHasTheGivenAnticlockwiseVertexY()
        {
            Assert.Equal(1, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).AnticlockwiseVertex.Y);
        }

        [Fact]
        public void TriangleAreaIsCalculatedCorrectly()
        {
            Assert.Equal(10, new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1)).Area, 5);
        }

        [Fact]
        public void ClockwiseFreeVectorIsCalculatedCorrectly()
        {
            var triangle = new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1));
            Assert.True(triangle.ClockwiseFreeVector.HasSameCoordinatesAs(new FreeVector(2, 4), 0.00001));
        }

        [Fact]
        public void AnticlockwiseFreeVectorIsCalculatedCorrectly()
        {
            var triangle = new Triangle(new Point(2, 3), new Point(4, 7), new Point(6, 1));
            Assert.True(triangle.AnticlockwiseFreeVector.HasSameCoordinatesAs(new FreeVector(4, -2), 0.00001));
        }
    }
}
