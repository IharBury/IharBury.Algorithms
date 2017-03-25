using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class PointTests
    {
        [Fact]
        public void OriginHasZeroX()
        {
            Assert.Equal(0, Point.Origin.X);
        }

        [Fact]
        public void OriginHasZeroY()
        {
            Assert.Equal(0, Point.Origin.Y);
        }

        [Fact]
        public void ConstructedPointHasTheGivenX()
        {
            Assert.Equal(2.34, new Point(2.34, 5.93).X);
        }

        [Fact]
        public void ConstructedPointHasTheGivenY()
        {
            Assert.Equal(5.93, new Point(2.34, 5.93).Y);
        }

        [Fact]
        public void SquaredDistanceToSelfIsZero()
        {
            var point = new Point(2.34, 6.12);
            Assert.Equal(0, point.GetSquaredDistanceTo(point));
        }

        [Fact]
        public void SquaredDistanceIsCalculatedApproximatelyCorrectly()
        {
            Assert.Equal(5, new Point(2, 3).GetSquaredDistanceTo(new Point(0, 4)), 5);
        }

        [Fact]
        public void DistanceToSelfIsZero()
        {
            var point = new Point(2.34, 6.12);
            Assert.Equal(0, point.GetDistanceTo(point));
        }

        [Fact]
        public void DistanceIsCalculatedApproximatelyCorrectly()
        {
            Assert.Equal(2.2360679774997896964091736687313, new Point(2, 3).GetDistanceTo(new Point(0, 4)), 5);
        }

        [Fact]
        public void PointHasSameCoordinatesAsCloseEnoughPointWithTheGivenSquaredDistanceError()
        {
            Assert.True(new Point(2.001, 3.001).HasSameCoordinatesAs(new Point(2, 3), 0.00001));
        }

        [Fact]
        public void PointDoesNotHaveTheSameCoordinatesAsFarEnoughPointWithTheGivenSquaredDistanceError()
        {
            Assert.False(new Point(2.01, 3.01).HasSameCoordinatesAs(new Point(2, 3), 0.00001));
        }
    }
}
