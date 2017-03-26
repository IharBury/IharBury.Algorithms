using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class CircleTests
    {
        [Fact]
        public void ConstructedCircleHasTheGivenCenterX()
        {
            Assert.Equal(4.3, new Circle(new Point(4.3, 3.23), 6.73).Center.X);
        }

        [Fact]
        public void ConstructedCircleHasTheGivenCenterY()
        {
            Assert.Equal(3.23, new Circle(new Point(4.3, 3.23), 6.73).Center.Y);
        }

        [Fact]
        public void ConstructedCircleHasTheGivenRadiusLength()
        {
            Assert.Equal(6.73, new Circle(new Point(4.3, 3.23), 6.73).RadiusLength);
        }

        [Fact]
        public void AreaIsCalculatedCorrectly()
        {
            Assert.Equal(142.29184189977714617036466049454, new Circle(new Point(4.3, 3.23), 6.73).Area, 5);
        }

        [Fact]
        public void CircleHasSameCoordinatesAsCircleWithCloseEnoughCenterAndSimilarEnoughRadiusWithTheGivenErrors()
        {
            var circle1 = new Circle(new Point(2.001, 3.001), 5.0001);
            var circle2 = new Circle(new Point(2, 3), 5);
            Assert.True(circle1.HasSameCoordinatesAs(circle2, 0.00001, 0.001));
        }

        [Fact]
        public void CircleDoesNotHaveSameCoordinatesAsCircleWithFarEnoughCenterWithTheGivenErrors()
        {
            var circle1 = new Circle(new Point(2.01, 3.01), 5.0001);
            var circle2 = new Circle(new Point(2, 3), 5);
            Assert.False(circle1.HasSameCoordinatesAs(circle2, 0.00001, 0.001));
        }

        [Fact]
        public void CircleDoesNotHaveSameCoordinatesAsCircleWithDifferentEnoughRadiusWithTheGivenErrors()
        {
            var circle1 = new Circle(new Point(2.001, 3.001), 5.01);
            var circle2 = new Circle(new Point(2, 3), 5);
            Assert.False(circle1.HasSameCoordinatesAs(circle2, 0.00001, 0.001));
        }
    }
}
