using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class Line2DInt32Tests
    {
        [Fact]
        public void Point1IsOnTheLine()
        {
            var line = new Line2DInt32(new Point2DInt32(1, 2), new Point2DInt32(5, 9));
            Assert.True(line.HasPoint(line.Point1));
        }

        [Fact]
        public void Point2IsOnTheLine()
        {
            var line = new Line2DInt32(new Point2DInt32(1, 2), new Point2DInt32(5, 9));
            Assert.True(line.HasPoint(line.Point2));
        }

        [Fact]
        public void WhenAPointBetweenTheLinePointsIsOnTheLineItIsDetected()
        {
            var line = new Line2DInt32(new Point2DInt32(1, 2), new Point2DInt32(4, 8));
            Assert.True(line.HasPoint(new Point2DInt32(3, 6)));
        }

        [Fact]
        public void WhenAPointNotBetweenTheLinePointsIsOnTheLineItIsDetected()
        {
            var line = new Line2DInt32(new Point2DInt32(1, 2), new Point2DInt32(4, 8));
            Assert.True(line.HasPoint(new Point2DInt32(5, 10)));
        }

        [Fact]
        public void WhenAPointIsNotOnTheLineItIsDetected()
        {
            var line = new Line2DInt32(new Point2DInt32(1, 2), new Point2DInt32(4, 8));
            Assert.False(line.HasPoint(new Point2DInt32(5, 11)));
        }

        [Fact]
        public void WhenDetectingIfAPointIsOnTheLineItSupportsWholeInt32Range()
        {
            var coordinateValues = new[] { int.MinValue, int.MaxValue, 1, 0 };
            foreach (var coordinate1 in coordinateValues)
                foreach (var coordinate2 in coordinateValues)
                    foreach (var coordinate3 in coordinateValues)
                        foreach (var coordinate4 in coordinateValues)
                            foreach (var coordinate5 in coordinateValues)
                                foreach (var coordinate6 in coordinateValues)
                                    new Line2DInt32(
                                            new Point2DInt32(coordinate1, coordinate2),
                                            new Point2DInt32(coordinate3, coordinate4))
                                        .HasPoint(new Point2DInt32(coordinate5, coordinate6));

            Assert.True(
                new Line2DInt32(new Point2DInt32(int.MinValue, int.MinValue), new Point2DInt32(int.MaxValue, int.MaxValue))
                    .HasPoint(new Point2DInt32(int.MinValue + 1, int.MinValue + 1)));
            Assert.True(
                new Line2DInt32(new Point2DInt32(int.MinValue, int.MaxValue), new Point2DInt32(int.MinValue, int.MaxValue))
                    .HasPoint(new Point2DInt32(int.MinValue + 1, int.MaxValue - 1)));
        }

        [Fact]
        public void WhenDetectingIfAPointIsOnTheLineItDoesNotLosePrecision()
        {
            var line = new Line2DInt32(new Point2DInt32(1, 7), new Point2DInt32(7, 1));
            Assert.True(line.HasPoint(new Point2DInt32(13, -5)));
        }

        [Fact]
        public void EqualInclinedLinesHaveSameHashCode()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(-2, 5));
            var line2 = new Line2DInt32(new Point2DInt32(-7, -1), new Point2DInt32(-17, -13));
            Assert.True(line1.GetHashCode() == line2.GetHashCode());
        }

        [Fact]
        public void EqualVerticalLinesHaveSameHashCode()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(3, 5));
            var line2 = new Line2DInt32(new Point2DInt32(3, -1), new Point2DInt32(3, 12));
            Assert.True(line1.GetHashCode() == line2.GetHashCode());
        }

        [Fact]
        public void EqualHorizontalLinesHaveSameHashCode()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(-2, 11));
            var line2 = new Line2DInt32(new Point2DInt32(-7, 11), new Point2DInt32(10, 11));
            Assert.True(line1.GetHashCode() == line2.GetHashCode());
        }

        [Fact]
        public void WhenInclinedLinesAreEqualItIsDetected()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(-2, 5));
            var line2 = new Line2DInt32(new Point2DInt32(-7, -1), new Point2DInt32(-17, -13));
            Assert.True(line1 == line2);
        }

        [Fact]
        public void WhenVerticalLinesAreEqualItIsDetected()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(3, 5));
            var line2 = new Line2DInt32(new Point2DInt32(3, -1), new Point2DInt32(3, 12));
            Assert.True(line1 == line2);
        }

        [Fact]
        public void WhenHorizontalLinesAreEqualItIsDetected()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(-2, 11));
            var line2 = new Line2DInt32(new Point2DInt32(-7, 11), new Point2DInt32(10, 11));
            Assert.True(line1 == line2);
        }

        [Fact]
        public void LinesWithSwappedPointsAreEqual()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(3, 5));
            var line2 = new Line2DInt32(new Point2DInt32(3, 5), new Point2DInt32(3, 11));
            Assert.True(line1 == line2);
        }

        [Fact]
        public void WhenInclinedLinesAreNotEqualItIsDetected()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(-2, 5));
            var line2 = new Line2DInt32(new Point2DInt32(-6, -1), new Point2DInt32(-16, -13));
            Assert.False(line1 == line2);
        }

        [Fact]
        public void WhenVerticalLinesAreNotEqualItIsDetected()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(3, 5));
            var line2 = new Line2DInt32(new Point2DInt32(4, -1), new Point2DInt32(4, 12));
            Assert.False(line1 == line2);
        }

        [Fact]
        public void WhenHorizontalLinesAreNotEqualItIsDetected()
        {
            var line1 = new Line2DInt32(new Point2DInt32(3, 11), new Point2DInt32(-2, 11));
            var line2 = new Line2DInt32(new Point2DInt32(-7, 10), new Point2DInt32(10, 10));
            Assert.False(line1 == line2);
        }

        [Fact]
        public void WhenDetectingIfTwoLinesAreEqualItSupportsWholeInt32Range()
        {
            var coordinateValues = new[] { int.MinValue, int.MaxValue, 1, 0 };
            foreach (var coordinate1 in coordinateValues)
                foreach (var coordinate2 in coordinateValues)
                    foreach (var coordinate3 in coordinateValues)
                        foreach (var coordinate4 in coordinateValues)
                            foreach (var coordinate5 in coordinateValues)
                                foreach (var coordinate6 in coordinateValues)
                                    foreach (var coordinate7 in coordinateValues)
                                        foreach (var coordinate8 in coordinateValues)
                                            new Line2DInt32(
                                                    new Point2DInt32(coordinate1, coordinate2),
                                                    new Point2DInt32(coordinate3, coordinate4))
                                                .Equals(new Line2DInt32(
                                                    new Point2DInt32(coordinate5, coordinate6),
                                                    new Point2DInt32(coordinate7, coordinate8)));
        }

        [Fact]
        public void WhenCalculatingHashCodeItSupportsWholeInt32Range()
        {
            var coordinateValues = new[] { int.MinValue, int.MaxValue, 1, 0 };
            foreach (var coordinate1 in coordinateValues)
                foreach (var coordinate2 in coordinateValues)
                    foreach (var coordinate3 in coordinateValues)
                        foreach (var coordinate4 in coordinateValues)
                            new Line2DInt32(
                                    new Point2DInt32(coordinate1, coordinate2),
                                    new Point2DInt32(coordinate3, coordinate4))
                                .GetHashCode();
        }
    }
}
