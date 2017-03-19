using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision
{
    public class AngularCoordinateTests
    {
        [Fact]
        public void GreaterOfFirstQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinate(1, 3) > new AngularCoordinate(2, 4));
        }

        [Fact]
        public void EqualFirstQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinate(1, 3).CompareTo(new AngularCoordinate(2, 6)));
        }

        [Fact]
        public void GreaterOfSecondQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinate(-1, 3) < new AngularCoordinate(-2, 4));
        }

        [Fact]
        public void EqualSecondQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinate(-1, 3).CompareTo(new AngularCoordinate(-2, 6)));
        }

        [Fact]
        public void GreaterOfThirdQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinate(-1, -3) > new AngularCoordinate(-2, -4));
        }

        [Fact]
        public void EqualThirdQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinate(-1, -3).CompareTo(new AngularCoordinate(-2, -6)));
        }

        [Fact]
        public void GreaterOfFourthQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinate(1, -3) < new AngularCoordinate(2, -4));
        }

        [Fact]
        public void EqualFourthQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinate(1, -3).CompareTo(new AngularCoordinate(2, -6)));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanFirstQuarter()
        {
            Assert.True(AngularCoordinate.Zero < new AngularCoordinate(1, 1));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanQuarter()
        {
            Assert.True(AngularCoordinate.Zero < AngularCoordinate.Quarter);
        }

        [Fact]
        public void ZeroCoordinateIsLessThanSecondQuarter()
        {
            Assert.True(AngularCoordinate.Zero < new AngularCoordinate(-1, 1));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanHalf()
        {
            Assert.True(AngularCoordinate.Zero < AngularCoordinate.Half);
        }

        [Fact]
        public void ZeroCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(AngularCoordinate.Zero < new AngularCoordinate(-1, -1));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(AngularCoordinate.Zero < AngularCoordinate.ThreeQuarters);
        }

        [Fact]
        public void ZeroCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinate.Zero < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanQuarter()
        {
            Assert.True(new AngularCoordinate(1, 3) < AngularCoordinate.Quarter);
            Assert.True(new AngularCoordinate(3, 1) < AngularCoordinate.Quarter);
            Assert.True(new AngularCoordinate(1, 1) < AngularCoordinate.Quarter);
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanSecondQuarter()
        {
            Assert.True(new AngularCoordinate(1, 3) < new AngularCoordinate(-1, 1));
            Assert.True(new AngularCoordinate(3, 1) < new AngularCoordinate(-1, 1));
            Assert.True(new AngularCoordinate(1, 1) < new AngularCoordinate(-1, 1));
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanHalf()
        {
            Assert.True(new AngularCoordinate(1, 3) < AngularCoordinate.Half);
            Assert.True(new AngularCoordinate(3, 1) < AngularCoordinate.Half);
            Assert.True(new AngularCoordinate(1, 1) < AngularCoordinate.Half);
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(new AngularCoordinate(1, 3) < new AngularCoordinate(-1, -1));
            Assert.True(new AngularCoordinate(3, 1) < new AngularCoordinate(-1, -1));
            Assert.True(new AngularCoordinate(1, 1) < new AngularCoordinate(-1, -1));
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(new AngularCoordinate(1, 3) < AngularCoordinate.ThreeQuarters);
            Assert.True(new AngularCoordinate(3, 1) < AngularCoordinate.ThreeQuarters);
            Assert.True(new AngularCoordinate(1, 1) < AngularCoordinate.ThreeQuarters);
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(new AngularCoordinate(1, 3) < new AngularCoordinate(1, -1));
            Assert.True(new AngularCoordinate(3, 1) < new AngularCoordinate(1, -1));
            Assert.True(new AngularCoordinate(1, 1) < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void QuarterCoordinateIsLessThanSecondQuarter()
        {
            Assert.True(AngularCoordinate.Quarter < new AngularCoordinate(-1, 1));
        }

        [Fact]
        public void QuarterCoordinateIsLessThanHalf()
        {
            Assert.True(AngularCoordinate.Quarter < AngularCoordinate.Half);
        }

        [Fact]
        public void QuarterCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(AngularCoordinate.Quarter < new AngularCoordinate(-1, -1));
        }

        [Fact]
        public void QuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(AngularCoordinate.Quarter < AngularCoordinate.ThreeQuarters);
        }

        [Fact]
        public void QuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinate.Quarter < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanHalf()
        {
            Assert.True(new AngularCoordinate(-1, 3) < AngularCoordinate.Half);
            Assert.True(new AngularCoordinate(-3, 1) < AngularCoordinate.Half);
            Assert.True(new AngularCoordinate(-1, 1) < AngularCoordinate.Half);
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(new AngularCoordinate(-1, 3) < new AngularCoordinate(-1, -1));
            Assert.True(new AngularCoordinate(-3, 1) < new AngularCoordinate(-1, -1));
            Assert.True(new AngularCoordinate(-1, 1) < new AngularCoordinate(-1, -1));
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(new AngularCoordinate(-1, 3) < AngularCoordinate.ThreeQuarters);
            Assert.True(new AngularCoordinate(-3, 1) < AngularCoordinate.ThreeQuarters);
            Assert.True(new AngularCoordinate(-1, 1) < AngularCoordinate.ThreeQuarters);
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(new AngularCoordinate(-1, 3) < new AngularCoordinate(1, -1));
            Assert.True(new AngularCoordinate(-3, 1) < new AngularCoordinate(1, -1));
            Assert.True(new AngularCoordinate(-1, 1) < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void HalfCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(AngularCoordinate.Half < new AngularCoordinate(-1, -1));
        }

        [Fact]
        public void HalfCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(AngularCoordinate.Half < AngularCoordinate.ThreeQuarters);
        }

        [Fact]
        public void HalfCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinate.Half < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void ThirdQuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(new AngularCoordinate(-1, -3) < AngularCoordinate.ThreeQuarters);
            Assert.True(new AngularCoordinate(-3, -1) < AngularCoordinate.ThreeQuarters);
            Assert.True(new AngularCoordinate(-1, -1) < AngularCoordinate.ThreeQuarters);
        }

        [Fact]
        public void ThirdQuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(new AngularCoordinate(-1, -3) < new AngularCoordinate(1, -1));
            Assert.True(new AngularCoordinate(-3, -1) < new AngularCoordinate(1, -1));
            Assert.True(new AngularCoordinate(-1, -1) < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void ThreeQuartersCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinate.ThreeQuarters < new AngularCoordinate(1, -1));
        }

        [Fact]
        public void CanBeNormalized()
        {
            var normalized = new AngularCoordinate(-12, 15).Normalize();
            Assert.Equal(-4, normalized.X);
            Assert.Equal(5, normalized.Y);
        }
    }
}
