using System.Collections.Generic;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class DictionaryExtensionsTests
    {
        public class WhenGettingValueOrDefault
        {
            [Fact]
            public void DictionaryClassCanBeUsed()
            {
                new Dictionary<int, int>().GetValueOrDefault(1);
            }

            [Fact]
            public void DictionaryInterfaceCanBeUsed()
            {
                IDictionary<int, int> dictionary = new Dictionary<int, int>();
                dictionary.GetValueOrDefault(1);
            }

            [Fact]
            public void ReadOnlyDictionaryInterfaceCanBeUsed()
            {
                IReadOnlyDictionary<int, int> dictionary = new Dictionary<int, int>();
                dictionary.GetValueOrDefault(1);
            }

            [Fact]
            public void WhenDictionaryContainsKeyItReturnsValue()
            {
                Assert.Equal(2, new Dictionary<int, int> { { 1, 2 } }.GetValueOrDefault(1));
            }

            [Fact]
            public void WhenDictionaryDoesNotContainKeyItReturnsDefaultValue()
            {
                Assert.Equal(default(int), new Dictionary<int, int>().GetValueOrDefault(1));
            }

            [Fact]
            public void WhenDictionaryDoesNotContainKeyAndDefaultValueGivenItReturnsTheGivenDefaultValue()
            {
                Assert.Equal(5, new Dictionary<int, int>().GetValueOrDefault(1, 5));
            }
        }

        public class WhenGettingNullableValueOrNull
        {
            [Fact]
            public void DictionaryClassCanBeUsed()
            {
                new Dictionary<int, int>().GetNullableValueOrNull(1);
            }

            [Fact]
            public void DictionaryInterfaceCanBeUsed()
            {
                IDictionary<int, int> dictionary = new Dictionary<int, int>();
                dictionary.GetNullableValueOrNull(1);
            }

            [Fact]
            public void ReadOnlyDictionaryInterfaceCanBeUsed()
            {
                IReadOnlyDictionary<int, int> dictionary = new Dictionary<int, int>();
                dictionary.GetNullableValueOrNull(1);
            }

            [Fact]
            public void WhenDictionaryContainsKeyItReturnsValue()
            {
                Assert.Equal(2, new Dictionary<int, int> { { 1, 2 } }.GetNullableValueOrNull(1));
            }

            [Fact]
            public void WhenDictionaryDoesNotContainKeyItReturnsDefaultValue()
            {
                Assert.Null(new Dictionary<int, int>().GetNullableValueOrNull(1));
            }
        }
    }
}
