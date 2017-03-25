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
        public void ConstructedCircleHasTheGivenRadius()
        {
            Assert.Equal(6.73, new Circle(new Point(4.3, 3.23), 6.73).Radius);
        }

        [Fact]
        public void AreaIsCalculatedCorrectly()
        {
            Assert.Equal(142.29184189977714617036466049454, new Circle(new Point(4.3, 3.23), 6.73).Area, 5);
        }
    }
}
