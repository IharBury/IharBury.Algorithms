using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class CircularSectorTests
    {
        [Fact]
        public void ConstructedCircluarSectorHasTheGivenCircleCenterX()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(4.34, circularSector.CircleCenter.X);
        }

        [Fact]
        public void ConstructedCircluarSectorHasTheGivenCircleCenterY()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(6.12, circularSector.CircleCenter.Y);
        }

        [Fact]
        public void ConstructedCircluarSectorHasTheGivenClockwiseVertexX()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(5.1, circularSector.ClockwiseVertex.X);
        }

        [Fact]
        public void ConstructedCircluarSectorHasTheGivenClockwiseVertexY()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(6.22, circularSector.ClockwiseVertex.Y);
        }

        [Fact]
        public void ConstructedCircluarSectorHasTheGivenCentralAngleInRadians()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.14, circularSector.CentralAngle.InRadians);
        }

        [Fact]
        public void ClockwiseRadiusIsCalculatedCorrectly()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            var expectedClockwiseRadius = new LineSegment(new Point(4.34, 6.12), new Point(5.1, 6.22));
            Assert.True(circularSector.ClockwiseRadius.HasSameCoordinatesAs(expectedClockwiseRadius, 0.00001));
        }

        [Fact]
        public void SquaredRadiusLengthIsCalculatedCorrectly()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.5876, circularSector.SquaredRadiusLength, 5);
        }

        [Fact]
        public void RadiusLengthIsCalculatedCorrectly()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.76655071586947200502188458103729, circularSector.RadiusLength, 5);
        }

        [Fact]
        public void CircleIsCalculatedCorrectly()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            var expectedCircle = new Circle(new Point(4.34, 6.12), 0.76655071586947200502188458103729);
            Assert.True(circularSector.Circle.HasSameCoordinatesAs(expectedCircle, 0.00001, 0.00001));
        }

        [Fact]
        public void AreaIsCalculatedCorrectly()
        {
            var circularSector = new CircularSector(new Point(4.34, 6.12), new Point(5.1, 6.22), new AngleMeasure(0.14));
            Assert.Equal(0.041132, circularSector.Area, 5);
        }
    }
}
