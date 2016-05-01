using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class AngularCoordinateInt32Tests
    {
        [Fact]
        public void GreaterOfFirstQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) > new AngularCoordinateInt32(2, 4));
        }

        [Fact]
        public void EqualFirstQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinateInt32(1, 3).CompareTo(new AngularCoordinateInt32(2, 6)));
        }

        [Fact]
        public void GreaterOfSecondQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinateInt32(-1, 3) < new AngularCoordinateInt32(-2, 4));
        }

        [Fact]
        public void EqualSecondQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinateInt32(-1, 3).CompareTo(new AngularCoordinateInt32(-2, 6)));
        }

        [Fact]
        public void GreaterOfThirdQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinateInt32(-1, -3) > new AngularCoordinateInt32(-2, -4));
        }

        [Fact]
        public void EqualThirdQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinateInt32(-1, -3).CompareTo(new AngularCoordinateInt32(-2, -6)));
        }

        [Fact]
        public void GreaterOfFourthQuarterCoordinatesIsFoundWhenComparing()
        {
            Assert.True(new AngularCoordinateInt32(1, -3) < new AngularCoordinateInt32(2, -4));
        }

        [Fact]
        public void EqualFourthQuarterCoordinatesAreFoundWhenComparing()
        {
            Assert.Equal(0, new AngularCoordinateInt32(1, -3).CompareTo(new AngularCoordinateInt32(2, -6)));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanFirstQuarter()
        {
            Assert.True(AngularCoordinateInt32.Zero < new AngularCoordinateInt32(1, 1));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanQuarter()
        {
            Assert.True(AngularCoordinateInt32.Zero < AngularCoordinateInt32.Quarter);
        }

        [Fact]
        public void ZeroCoordinateIsLessThanSecondQuarter()
        {
            Assert.True(AngularCoordinateInt32.Zero < new AngularCoordinateInt32(-1, 1));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanHalf()
        {
            Assert.True(AngularCoordinateInt32.Zero < AngularCoordinateInt32.Half);
        }

        [Fact]
        public void ZeroCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(AngularCoordinateInt32.Zero < new AngularCoordinateInt32(-1, -1));
        }

        [Fact]
        public void ZeroCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(AngularCoordinateInt32.Zero < AngularCoordinateInt32.ThreeQuarters);
        }

        [Fact]
        public void ZeroCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinateInt32.Zero < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanQuarter()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) < AngularCoordinateInt32.Quarter);
            Assert.True(new AngularCoordinateInt32(3, 1) < AngularCoordinateInt32.Quarter);
            Assert.True(new AngularCoordinateInt32(1, 1) < AngularCoordinateInt32.Quarter);
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanSecondQuarter()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) < new AngularCoordinateInt32(-1, 1));
            Assert.True(new AngularCoordinateInt32(3, 1) < new AngularCoordinateInt32(-1, 1));
            Assert.True(new AngularCoordinateInt32(1, 1) < new AngularCoordinateInt32(-1, 1));
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanHalf()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) < AngularCoordinateInt32.Half);
            Assert.True(new AngularCoordinateInt32(3, 1) < AngularCoordinateInt32.Half);
            Assert.True(new AngularCoordinateInt32(1, 1) < AngularCoordinateInt32.Half);
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) < new AngularCoordinateInt32(-1, -1));
            Assert.True(new AngularCoordinateInt32(3, 1) < new AngularCoordinateInt32(-1, -1));
            Assert.True(new AngularCoordinateInt32(1, 1) < new AngularCoordinateInt32(-1, -1));
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) < AngularCoordinateInt32.ThreeQuarters);
            Assert.True(new AngularCoordinateInt32(3, 1) < AngularCoordinateInt32.ThreeQuarters);
            Assert.True(new AngularCoordinateInt32(1, 1) < AngularCoordinateInt32.ThreeQuarters);
        }

        [Fact]
        public void FirstQuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(new AngularCoordinateInt32(1, 3) < new AngularCoordinateInt32(1, -1));
            Assert.True(new AngularCoordinateInt32(3, 1) < new AngularCoordinateInt32(1, -1));
            Assert.True(new AngularCoordinateInt32(1, 1) < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void QuarterCoordinateIsLessThanSecondQuarter()
        {
            Assert.True(AngularCoordinateInt32.Quarter < new AngularCoordinateInt32(-1, 1));
        }

        [Fact]
        public void QuarterCoordinateIsLessThanHalf()
        {
            Assert.True(AngularCoordinateInt32.Quarter < AngularCoordinateInt32.Half);
        }

        [Fact]
        public void QuarterCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(AngularCoordinateInt32.Quarter < new AngularCoordinateInt32(-1, -1));
        }

        [Fact]
        public void QuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(AngularCoordinateInt32.Quarter < AngularCoordinateInt32.ThreeQuarters);
        }

        [Fact]
        public void QuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinateInt32.Quarter < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanHalf()
        {
            Assert.True(new AngularCoordinateInt32(-1, 3) < AngularCoordinateInt32.Half);
            Assert.True(new AngularCoordinateInt32(-3, 1) < AngularCoordinateInt32.Half);
            Assert.True(new AngularCoordinateInt32(-1, 1) < AngularCoordinateInt32.Half);
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(new AngularCoordinateInt32(-1, 3) < new AngularCoordinateInt32(-1, -1));
            Assert.True(new AngularCoordinateInt32(-3, 1) < new AngularCoordinateInt32(-1, -1));
            Assert.True(new AngularCoordinateInt32(-1, 1) < new AngularCoordinateInt32(-1, -1));
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(new AngularCoordinateInt32(-1, 3) < AngularCoordinateInt32.ThreeQuarters);
            Assert.True(new AngularCoordinateInt32(-3, 1) < AngularCoordinateInt32.ThreeQuarters);
            Assert.True(new AngularCoordinateInt32(-1, 1) < AngularCoordinateInt32.ThreeQuarters);
        }

        [Fact]
        public void SecondQuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(new AngularCoordinateInt32(-1, 3) < new AngularCoordinateInt32(1, -1));
            Assert.True(new AngularCoordinateInt32(-3, 1) < new AngularCoordinateInt32(1, -1));
            Assert.True(new AngularCoordinateInt32(-1, 1) < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void HalfCoordinateIsLessThanThirdQuarter()
        {
            Assert.True(AngularCoordinateInt32.Half < new AngularCoordinateInt32(-1, -1));
        }

        [Fact]
        public void HalfCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(AngularCoordinateInt32.Half < AngularCoordinateInt32.ThreeQuarters);
        }

        [Fact]
        public void HalfCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinateInt32.Half < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void ThirdQuarterCoordinateIsLessThanThreeQuarters()
        {
            Assert.True(new AngularCoordinateInt32(-1, -3) < AngularCoordinateInt32.ThreeQuarters);
            Assert.True(new AngularCoordinateInt32(-3, -1) < AngularCoordinateInt32.ThreeQuarters);
            Assert.True(new AngularCoordinateInt32(-1, -1) < AngularCoordinateInt32.ThreeQuarters);
        }

        [Fact]
        public void ThirdQuarterCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(new AngularCoordinateInt32(-1, -3) < new AngularCoordinateInt32(1, -1));
            Assert.True(new AngularCoordinateInt32(-3, -1) < new AngularCoordinateInt32(1, -1));
            Assert.True(new AngularCoordinateInt32(-1, -1) < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void ThreeQuartersCoordinateIsLessThanFourthQuarter()
        {
            Assert.True(AngularCoordinateInt32.ThreeQuarters < new AngularCoordinateInt32(1, -1));
        }

        [Fact]
        public void CanBeNormalized()
        {
            var normalized = new AngularCoordinateInt32(-12, 15).Normalize();
            Assert.Equal(-4, normalized.X);
            Assert.Equal(5, normalized.Y);
        }
    }
}
