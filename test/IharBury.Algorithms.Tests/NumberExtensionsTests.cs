using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class NumberExtensionsTests
    {
        [Fact]
        public void AbsOfMinInt32IsCorrect()
        {
            Assert.Equal(-(long)int.MinValue, int.MinValue.Abs());
        }

        [Fact]
        public void GcdOfInt32IsCorrect()
        {
            Assert.Equal(5L, 25.Gcd(15));
        }

        [Fact]
        public void GcdOfMinInt32IsCorrect()
        {
            Assert.Equal(-(long)int.MinValue, int.MinValue.Gcd(int.MinValue));
        }

        [Fact]
        public void LcmOfInt32IsCorrect()
        {
            Assert.Equal(75L, 25.Lcm(15));
        }

        [Fact]
        public void LcmOfMinInt32IsCorrect()
        {
            Assert.Equal(-(long)int.MinValue, int.MinValue.Lcm(int.MinValue));
        }

        [Fact]
        public void VeryLargeLcmOfInt32IsCorrect()
        {
            Assert.Equal((-(long)int.MinValue) * -(int.MinValue + 1), int.MinValue.Lcm(int.MinValue + 1));
        }
    }
}
