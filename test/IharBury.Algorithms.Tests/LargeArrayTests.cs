using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class LargeArrayTests
    {
        [Fact]
        public void EmptyArrayCanBeCreated()
        {
            var array = new LargeArray<int>(0);
            Assert.Equal(0, array.Count);
            Assert.Empty(array);
        }

        [Fact]
        public void SmallArrayCanBeCreated()
        {
            var array = new LargeArray<int>(2);
            Assert.Equal(2, array.Count);
        }

        [Fact]
        public void CreatedArrayContainsDefaultValues()
        {
            var array = new LargeArray<int>(2);
            Assert.Equal(default(int), array[0]);
            Assert.Equal(default(int), array[1]);
        }

        [Fact]
        public void LargeArrayCanBeCreated()
        {
            var count = int.MaxValue * 2L + 100;
            var array = new LargeArray<int>(count);
            Assert.Equal(count, array.Count);
        }

        [Fact]
        public void ItemCanBeSet()
        {
            var count = int.MaxValue * 2L + 100;
            var array = new LargeArray<int>(count);

            array[count - 1000] = 29348;
            array[count - 20] = 100;
            array[count - int.MaxValue] = 42;

            Assert.Equal(29348, array[count - 1000]);
            Assert.Equal(100, array[count - 20]);
            Assert.Equal(42, array[count - int.MaxValue]);
        }
    }
}
