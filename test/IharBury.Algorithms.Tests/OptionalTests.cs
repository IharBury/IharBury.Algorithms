using FakeItEasy;
using System;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public sealed class OptionalTests
    {
        [Fact]
        public void DefaultHasNoValue()
        {
            Assert.False(default(Optional<int>).HasValue);
        }

        [Fact]
        public void ConstructedWithValueHasTheValue()
        {
            var optional = new Optional<int>(1);
            Assert.True(optional.HasValue);
            Assert.Equal(1, optional.Value);
        }

        [Fact]
        public void ConstructedWithNullValueHasNullValue()
        {
            var optional = new Optional<string>(null);
            Assert.True(optional.HasValue);
            Assert.Null(optional.Value);
        }

        [Fact]
        public void ConstructedWithEmptyNullableValueHasTheValue()
        {
            var optional = new Optional<int?>(default(int?));
            Assert.True(optional.HasValue);
            Assert.Equal(default(int?), optional.Value);
        }

        [Fact]
        public void ImplicitlyConvertedFromValueHasTheValue()
        {
            Optional<int> optional = 1;
            Assert.True(optional.HasValue);
            Assert.Equal(1, optional.Value);
        }

        [Fact]
        public void ConstructedWithValueViaStaticMethodHasTheValue()
        {
            var optional = Optional.From(1);
            Assert.True(optional.HasValue);
            Assert.Equal(1, optional.Value);
        }

        [Fact]
        public void AttemptToGetValueThrowsWhenThereIsNoValue()
        {
            var optional = default(Optional<int>);
            Assert.Throws<InvalidOperationException>(() => optional.Value);
        }

        [Fact]
        public void NoneHasNoValue()
        {
            Assert.False(Optional<int>.None.HasValue);
        }

        [Fact]
        public void WithValueItIsConvertedToStringAsTheValue()
        {
            var optional = new Optional<int>(1);
            Assert.Equal(1.ToString(), optional.ToString());
        }

        [Fact]
        public void WithoutValueItIsConvertedToStringAsSpecialTextWithTheValueTypeName()
        {
            var optional = new Optional<int>();
            Assert.Equal($"Empty {typeof(Optional<int>)}", optional.ToString());
        }

        public sealed class WhenGettingValueOrDefaultValueFromAConstant
        {
            [Fact]
            public void WithValueItReturnsTheValue()
            {
                var optional = new Optional<int>(1);
                Assert.Equal(1, optional.OrDefault(2));
            }

            [Fact]
            public void WithoutValueItReturnsTheDefaultValue()
            {
                var optional = new Optional<int>();
                Assert.Equal(2, optional.OrDefault(2));
            }
        }

        public sealed class WhenGettingValueOrDefaultValueFromADelegate
        {
            [Fact]
            public void WithValueItReturnsTheValue()
            {
                var optional = new Optional<int>(1);
                Assert.Equal(1, optional.OrDefault(() => 2));
            }

            [Fact]
            public void WithValueItDoesNotInvokeTheDelegate()
            {
                var theDelegate = A.Fake<Func<int>>();
                var optional = new Optional<int>(1);
                optional.OrDefault(theDelegate);
                A.CallTo(theDelegate).MustNotHaveHappened();
            }

            [Fact]
            public void WithoutValueItInvokesTheDelegateAndReturnsTheResult()
            {
                var optional = new Optional<int>();
                Assert.Equal(2, optional.OrDefault(2));
            }
        }

        public sealed class WhenEqualityIsDeterminedViaStronglyTypedOverload
        {
            [Fact]
            public void WhenBothValuesAreMissingTheOptionalValuesAreEqual()
            {
                Assert.True(Optional<int>.None.Equals(Optional<int>.None));
            }

            [Fact]
            public void WhenOnlyTheFirstValueIsMissingTheOptionalValuesAreNotEqual()
            {
                Assert.False(Optional<int>.None.Equals(new Optional<int>(1)));
            }

            [Fact]
            public void WhenOnlyTheSecondValueIsMissingTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals(Optional<int>.None));
            }

            [Fact]
            public void WhenRepresentedValuesAreEqualTheOptionalValuesAreEqual()
            {
                Assert.True(new Optional<int>(1).Equals(new Optional<int>(1)));
            }

            [Fact]
            public void WhenRepresentedValuesAreNotEqualTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals(new Optional<int>(2)));
            }

            [Fact]
            public void WhenBothValuesAreNullTheOptionalValuesAreEqual()
            {
                Assert.True(new Optional<string>(null).Equals(new Optional<string>(null)));
            }

            [Fact]
            public void WhenOnlyTheFirstValueIsNullTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<string>(null).Equals(new Optional<string>("")));
            }

            [Fact]
            public void WhenOnlyTheSecondValueIsNullTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<string>("").Equals(new Optional<string>(null)));
            }

            [Fact]
            public void WhenBothValuesAreEmptyNullablesTheOptionalValuesAreEqual()
            {
                Assert.True(new Optional<int?>(default(int?)).Equals(new Optional<int?>(default(int?))));
            }

            [Fact]
            public void WhenOnlyTheFirstValueIsEmptyNullableTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int?>(default(int?)).Equals(new Optional<int?>(0)));
            }

            [Fact]
            public void WhenOnlyTheSecondValueIsEmptyNullableTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int?>(0).Equals(new Optional<int?>(default(int?))));
            }
        }

        public sealed class WhenEqualityIsDeterminedViaNonTypedOverload
        {
            [Fact]
            public void WhenBothValuesAreMissingTheOptionalValuesAreEqual()
            {
                Assert.True(Optional<int>.None.Equals((object)Optional<int>.None));
            }

            [Fact]
            public void WhenOnlyTheFirstValueIsMissingTheOptionalValuesAreNotEqual()
            {
                Assert.False(Optional<int>.None.Equals((object)new Optional<int>(1)));
            }

            [Fact]
            public void WhenOnlyTheSecondValueIsMissingTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals((object)Optional<int>.None));
            }

            [Fact]
            public void WhenRepresentedValuesAreEqualTheOptionalValuesAreEqual()
            {
                Assert.True(new Optional<int>(1).Equals((object)new Optional<int>(1)));
            }

            [Fact]
            public void WhenRepresentedValuesAreNotEqualTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals((object)new Optional<int>(2)));
            }

            [Fact]
            public void WhenBothValuesAreNullTheOptionalValuesAreEqual()
            {
                Assert.True(new Optional<string>(null).Equals((object)new Optional<string>(null)));
            }

            [Fact]
            public void WhenOnlyTheFirstValueIsNullTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<string>(null).Equals((object)new Optional<string>("")));
            }

            [Fact]
            public void WhenOnlyTheSecondValueIsNullTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<string>("").Equals((object)new Optional<string>(null)));
            }

            [Fact]
            public void WhenBothValuesAreEmptyNullablesTheOptionalValuesAreEqual()
            {
                Assert.True(new Optional<int?>(default(int?)).Equals((object)new Optional<int?>(default(int?))));
            }

            [Fact]
            public void WhenOnlyTheFirstValueIsEmptyNullableTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int?>(default(int?)).Equals((object)new Optional<int?>(0)));
            }

            [Fact]
            public void WhenOnlyTheSecondValueIsEmptyNullableTheOptionalValuesAreNotEqual()
            {
                Assert.False(new Optional<int?>(0).Equals((object)new Optional<int?>(default(int?))));
            }

            [Fact]
            public void WhenTheArgumentIsNotAnOptionalValueTheObjectsAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals((object)1));
            }

            [Fact]
            public void WhenTheArgumentIsNullTheObjectsAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals((object)null));
            }

            [Fact]
            public void WhenTheArgumentIsAnOptionalValueOfADifferentTypeTheObjectsAreNotEqual()
            {
                Assert.False(new Optional<int>(1).Equals((object)new Optional<uint>(1)));
            }
        }

        [Fact]
        public void WithoutValueHashCodeIsStable()
        {
            var hashCode1 = Optional<int>.None.GetHashCode();
            var hashCode2 = Optional<int>.None.GetHashCode();
            Assert.True(hashCode1 == hashCode2);
        }

        [Fact]
        public void WithValueHashCodeIsStable()
        {
            var hashCode1 = new Optional<int>(1).GetHashCode();
            var hashCode2 = new Optional<int>(1).GetHashCode();
            Assert.True(hashCode1 == hashCode2);
        }

        [Fact]
        public void WithNullValueHashCodeIsStable()
        {
            var hashCode1 = new Optional<string>(null).GetHashCode();
            var hashCode2 = new Optional<string>(null).GetHashCode();
            Assert.True(hashCode1 == hashCode2);
        }

        [Fact]
        public void WithEmptyNullableValueHashCodeIsStable()
        {
            var hashCode1 = new Optional<int?>(default(int?)).GetHashCode();
            var hashCode2 = new Optional<int?>(default(int?)).GetHashCode();
            Assert.True(hashCode1 == hashCode2);
        }
    }
}
