using Xunit;

namespace IharBury.Algorithms.Tests
{
    public sealed class NumberExtensionsTests
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

        public sealed class WhenLimitingMaxValue
        {
            public sealed class Int32
            {
                public sealed class WithOneLimit
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitItReturnsTheValue()
                    {
                        Assert.Equal(1, 1.ButMax(2));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanLimitItReturnsTheLimit()
                    {
                        Assert.Equal(1, 2.ButMax(1));
                    }
                }

                public sealed class WithTwoLimits
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(1, 1.ButMax(2, 3));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanALimitItReturnsMinOfTheLimits()
                    {
                        Assert.Equal(1, 2.ButMax(1, 3));
                    }
                }

                public sealed class WithThreeLimits
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(1, 1.ButMax(2, 3, 4));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanALimitItReturnsMinOfTheLimits()
                    {
                        Assert.Equal(1, 3.ButMax(1, 2, 4));
                    }
                }

                public sealed class WithArrayOfLimits
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(1, 1.ButMax(2, 3, 4, 5));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanALimitItReturnsMinOfTheLimits()
                    {
                        Assert.Equal(1, 3.ButMax(1, 2, 4, 5));
                    }
                }
            }

            public sealed class Int64
            {
                public sealed class WithOneLimit
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitItReturnsTheValue()
                    {
                        Assert.Equal(1L, 1L.ButMax(2L));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanLimitItReturnsTheLimit()
                    {
                        Assert.Equal(1L, 2L.ButMax(1L));
                    }
                }

                public sealed class WithTwoLimits
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(1L, 1L.ButMax(2L, 3L));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanALimitItReturnsMinOfTheLimits()
                    {
                        Assert.Equal(1L, 2L.ButMax(1L, 3L));
                    }
                }

                public sealed class WithThreeLimits
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(1L, 1L.ButMax(2L, 3L, 4L));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanALimitItReturnsMinOfTheLimits()
                    {
                        Assert.Equal(1L, 3L.ButMax(1L, 2L, 4L));
                    }
                }

                public sealed class WithArrayOfLimits
                {
                    [Fact]
                    public void WhenValueIsLessThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(1L, 1L.ButMax(2L, 3L, 4L, 5L));
                    }

                    [Fact]
                    public void WhenValueIsGreaterThanALimitItReturnsMinOfTheLimits()
                    {
                        Assert.Equal(1L, 3L.ButMax(1L, 2L, 4L, 5L));
                    }
                }
            }
        }

        public sealed class WhenLimitingMinValue
        {
            public sealed class Int32
            {
                public sealed class WithOneLimit
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitItReturnsTheValue()
                    {
                        Assert.Equal(2, 2.ButMin(1));
                    }

                    [Fact]
                    public void WhenValueIsLessThanLimitItReturnsTheLimit()
                    {
                        Assert.Equal(2, 1.ButMin(2));
                    }
                }

                public sealed class WithTwoLimits
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(3, 3.ButMin(1, 2));
                    }

                    [Fact]
                    public void WhenValueIsLessThanALimitItReturnsMaxOfTheLimits()
                    {
                        Assert.Equal(3, 2.ButMin(1, 3));
                    }
                }

                public sealed class WithThreeLimits
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(4, 4.ButMin(2, 3, 1));
                    }

                    [Fact]
                    public void WhenValueIsLessThanALimitItReturnsMaxOfTheLimits()
                    {
                        Assert.Equal(4, 2.ButMin(1, 3, 4));
                    }
                }

                public sealed class WithArrayOfLimits
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(4, 4.ButMin(2, 3, 1, 1));
                    }

                    [Fact]
                    public void WhenValueIsLessThanALimitItReturnsMaxOfTheLimits()
                    {
                        Assert.Equal(4, 2.ButMin(1, 3, 4, 1));
                    }
                }
            }

            public sealed class Int64
            {
                public sealed class WithOneLimit
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitItReturnsTheValue()
                    {
                        Assert.Equal(2L, 2L.ButMin(1L));
                    }

                    [Fact]
                    public void WhenValueIsLessThanLimitItReturnsTheLimit()
                    {
                        Assert.Equal(2L, 1L.ButMin(2L));
                    }
                }

                public sealed class WithTwoLimits
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(3L, 3L.ButMin(1L, 2L));
                    }

                    [Fact]
                    public void WhenValueIsLessThanALimitItReturnsMaxOfTheLimits()
                    {
                        Assert.Equal(3L, 2L.ButMin(1L, 3L));
                    }
                }

                public sealed class WithThreeLimits
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(4L, 4L.ButMin(2L, 3L, 1L));
                    }

                    [Fact]
                    public void WhenValueIsLessThanALimitItReturnsMaxOfTheLimits()
                    {
                        Assert.Equal(4L, 2L.ButMin(1L, 3L, 4L));
                    }
                }

                public sealed class WithArrayOfLimits
                {
                    [Fact]
                    public void WhenValueIsGreaterThanLimitsItReturnsTheValue()
                    {
                        Assert.Equal(4L, 4L.ButMin(2L, 3L, 1L, 1L));
                    }

                    [Fact]
                    public void WhenValueIsLessThanALimitItReturnsMaxOfTheLimits()
                    {
                        Assert.Equal(4L, 2L.ButMin(1L, 3L, 4L, 1L));
                    }
                }
            }
        }
    }
}
