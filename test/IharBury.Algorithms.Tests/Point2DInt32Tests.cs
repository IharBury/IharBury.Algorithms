using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class Point2DInt32Tests
    {
        [Fact]
        public void PointOnTheLineIsNotToTheLeft()
        {
            var point = new Point2DInt32(3, 3);
            var backPoint = new Point2DInt32(1, 1);
            var forwardPoint = new Point2DInt32(2, 2);
            Assert.False(point.IsToTheLeftOf(backPoint, forwardPoint));
        }

        [Fact]
        public void WhenAPointIsToTheLeftItIsDetected()
        {
            var point = new Point2DInt32(3, 3);
            var backPoint = new Point2DInt32(1, 1);
            var forwardPoint = new Point2DInt32(1, 2);
            Assert.True(point.IsToTheLeftOf(backPoint, forwardPoint));
        }

        [Fact]
        public void WhenAPointIsToTheRightItIsNotToTheLeft()
        {
            var point = new Point2DInt32(3, 3);
            var backPoint = new Point2DInt32(1, 1);
            var forwardPoint = new Point2DInt32(2, 1);
            Assert.False(point.IsToTheLeftOf(backPoint, forwardPoint));
        }
    }
}
