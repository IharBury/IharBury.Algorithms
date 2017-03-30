using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class CircularSegmentTests
    {
        [Fact]
        public void ConstructedCircluarSegmentHasTheGivenCircleCenterX()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(4.34, circularSegment.CircleCenter.X);
        }

        [Fact]
        public void ConstructedCircluarSegmentHasTheGivenCircleCenterY()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(6.12, circularSegment.CircleCenter.Y);
        }

        [Fact]
        public void ConstructedCircluarSegmentHasTheGivenClockwiseVertexX()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(5.1, circularSegment.ClockwiseVertex.X);
        }

        [Fact]
        public void ConstructedCircluarSegmentHasTheGivenClockwiseVertexY()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(6.22, circularSegment.ClockwiseVertex.Y);
        }

        [Fact]
        public void ConstructedCircluarSegmentHasTheGivenCentralAngleInRadians()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.14, circularSegment.CentralAngle.InRadians);
        }

        [Fact]
        public void ClockwiseRadiusIsCalculatedCorrectly()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            var expectedClockwiseRadius = new LineSegment(new Point(4.34, 6.12), new Point(5.1, 6.22));
            Assert.True(circularSegment.ClockwiseRadius.HasSameCoordinatesAs(expectedClockwiseRadius, 0.00001));
        }

        [Fact]
        public void SquaredRadiusLengthIsCalculatedCorrectly()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.5876, circularSegment.SquaredRadiusLength, 5);
        }

        [Fact]
        public void RadiusLengthIsCalculatedCorrectly()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.76655071586947200502188458103729, circularSegment.RadiusLength, 5);
        }

        [Fact]
        public void CircleIsCalculatedCorrectly()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            var expectedCircle = new Circle(new Point(4.34, 6.12), 0.76655071586947200502188458103729);
            Assert.True(circularSegment.Circle.HasSameCoordinatesAs(expectedCircle, 0.00001, 0.00001));
        }

        [Fact]
        public void AreaIsCalculatedCorrectly()
        {
            var circularSegment = new CircularSegment(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(1.3423291752332167125502226896182e-4, circularSegment.Area, 5);
        }
    }
}
