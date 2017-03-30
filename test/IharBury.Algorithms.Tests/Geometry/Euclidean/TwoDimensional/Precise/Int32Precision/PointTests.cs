using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision
{
    public class PointTests
    {
        [Fact]
        public void PointOnTheLineIsNotToTheLeft()
        {
            var point = new Point(3, 3);
            var backPoint = new Point(1, 1);
            var forwardPoint = new Point(2, 2);
            Assert.False(point.IsToTheLeftOf(backPoint, forwardPoint));
        }

        [Fact]
        public void WhenAPointIsToTheLeftItIsDetected()
        {
            var point = new Point(3, 3);
            var backPoint = new Point(1, 1);
            var forwardPoint = new Point(2, 1);
            Assert.True(point.IsToTheLeftOf(backPoint, forwardPoint));
        }

        [Fact]
        public void WhenAPointIsToTheRightItIsNotToTheLeft()
        {
            var point = new Point(3, 3);
            var backPoint = new Point(1, 1);
            var forwardPoint = new Point(1, 2);
            Assert.False(point.IsToTheLeftOf(backPoint, forwardPoint));
        }
    }
}
