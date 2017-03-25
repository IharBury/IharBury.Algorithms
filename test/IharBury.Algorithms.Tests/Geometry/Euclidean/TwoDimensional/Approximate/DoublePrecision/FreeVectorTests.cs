using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class FreeVectorTests
    {
        [Fact]
        public void ZeroFreeVectorHasZeroX()
        {
            Assert.Equal(0, FreeVector.Zero.X);
        }

        [Fact]
        public void ZeroFreeVectorHasZeroY()
        {
            Assert.Equal(0, FreeVector.Zero.Y);
        }

        [Fact]
        public void ConstructedFreeVectorHasTheGivenX()
        {
            Assert.Equal(2.34, new FreeVector(2.34, 5.93).X);
        }

        [Fact]
        public void ConstructedFreeVectorHasTheGivenY()
        {
            Assert.Equal(5.93, new FreeVector(2.34, 5.93).Y);
        }

        [Fact]
        public void FreeVectorHasSameCoordinatesAsSimilarEnoughFreeVectorWithTheGivenErrorSquaredLength()
        {
            Assert.True(new FreeVector(2.001, 3.001).HasSameCoordinatesAs(new FreeVector(2, 3), 0.00001));
        }

        [Fact]
        public void FreeVectorDoesNotHaveTheSameCoordinatesAsDifferentEnoughFreeVectorWithTheGivenErrorSquaredLength()
        {
            Assert.False(new FreeVector(2.01, 3.01).HasSameCoordinatesAs(new FreeVector(2, 3), 0.00001));
        }

        [Fact]
        public void SumIsCalculatedCorrectly()
        {
            var sum = new FreeVector(2, 3) + new FreeVector(1, 2);
            Assert.True(sum.HasSameCoordinatesAs(new FreeVector(3, 5), 0.00001));
        }

        [Fact]
        public void DifferenceIsCalculatedCorrectly()
        {
            var difference = new FreeVector(2, 3) - new FreeVector(1, -1);
            Assert.True(difference.HasSameCoordinatesAs(new FreeVector(1, 4), 0.00001));
        }

        [Fact]
        public void SquaredLengthIsCalculatedCorrectly()
        {
            Assert.Equal(13, new FreeVector(2, 3).SquaredLength);
        }

        [Fact]
        public void LengthIsCalculatedCorrectly()
        {
            Assert.Equal(3.6055512754639892931192212674705, new FreeVector(2, 3).Length);
        }
    }
}
