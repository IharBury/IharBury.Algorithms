using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class LineSegmentSpecs
    {
        [Fact]
        public void ConstructedLineSegmentHasTheGivenBaseEndpointX()
        {
            Assert.Equal(2, new LineSegment(new Point(2, 3), new Point(4, 7)).BaseEndpoint.X);
        }

        [Fact]
        public void ConstructedLineSegmentHasTheGivenBaseEndpointY()
        {
            Assert.Equal(3, new LineSegment(new Point(2, 3), new Point(4, 7)).BaseEndpoint.Y);
        }

        [Fact]
        public void ConstructedLineSegmentHasTheGivenAnotherEndpointX()
        {
            Assert.Equal(4, new LineSegment(new Point(2, 3), new Point(4, 7)).AnotherEndpoint.X);
        }

        [Fact]
        public void ConstructedLineSegmentHasTheGivenAnotherEndpointY()
        {
            Assert.Equal(7, new LineSegment(new Point(2, 3), new Point(4, 7)).AnotherEndpoint.Y);
        }

        [Fact]
        public void SquaredLengthIsCalculatedCorrectly()
        {
            Assert.Equal(20, new LineSegment(new Point(2, 3), new Point(4, 7)).SquaredLength, 5);
        }

        [Fact]
        public void LengthIsCalculatedCorrectly()
        {
            Assert.Equal(4.4721359549995793928183473374626, new LineSegment(new Point(2, 3), new Point(4, 7)).Length, 5);
        }

        [Fact]
        public void MiddleIsCalculatedCorrectly()
        {
            var lineSegment = new LineSegment(new Point(2, 3), new Point(4, 7));
            Assert.True(lineSegment.Middle.EqualsWithMaxSquaredDistanceError(new Point(3, 5), 0.00001));
        }
    }
}
