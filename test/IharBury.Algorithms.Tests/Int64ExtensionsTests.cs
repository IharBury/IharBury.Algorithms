using Xunit;

namespace IharBury.Algorithms.Tests
{
    public sealed class Int64ExtensionsTests
    {
        public sealed class WhenLimitingMaxValue
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

        public sealed class WhenLimitingMinValue
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
