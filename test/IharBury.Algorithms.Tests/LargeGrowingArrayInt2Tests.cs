using System.Linq;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class LargeGrowingArrayInt2Tests
    {
        [Fact]
        public void NewArrayIsEmpty()
        {
            var list = new LargeGrowingArrayUInt2();
            Assert.Empty(list);
        }

        [Fact]
        public void CanAddOneItemToEmptyArray()
        {
            var list = new LargeGrowingArrayUInt2();
            list.Add(2);
            Assert.Equal(list, new byte[] { 2 });
        }

        [Fact]
        public void CanAddOneItemToNonEmptyArray()
        {
            var list = new LargeGrowingArrayUInt2();
            list.Add(2);
            list.Add(3);
            Assert.Equal(list, new byte[] { 2, 3 });
        }

        [Fact]
        public void CanAddOneItemSoThatPageIsFull()
        {
            var list = new LargeGrowingArrayUInt2();
            list.AddAll(Enumerable.Repeat((byte)0, 15));
            list.Add(3);
            Assert.Equal(list, Enumerable.Repeat((byte)0, 15).Concat(new byte[] { 3 }));
        }

        [Fact]
        public void CanAddOneItemToNewPage()
        {
            var list = new LargeGrowingArrayUInt2();
            list.AddAll(Enumerable.Repeat((byte)0, 16));
            list.Add(2);
            Assert.Equal(list, Enumerable.Repeat((byte)0, 16).Concat(new byte[] { 2 }));
        }

        [Fact]
        public void CanRemoveLastItemFromArrayWithMoreThanOneItem()
        {
            var list = new LargeGrowingArrayUInt2();
            list.Add(2);
            list.Add(3);
            list.RemoveLast();
            Assert.Equal(list, new byte[] { 2 });
        }

        [Fact]
        public void CanRemoveLastItemFromArrayWithOneItem()
        {
            var list = new LargeGrowingArrayUInt2();
            list.Add(2);
            list.RemoveLast();
            Assert.Empty(list);
        }

        [Fact]
        public void CanRemoveLastItemFromPageWithOneItem()
        {
            var list = new LargeGrowingArrayUInt2();
            list.AddAll(Enumerable.Repeat((byte)0, 16));
            list.Add(3);
            list.RemoveLast();
            Assert.Equal(list, Enumerable.Repeat((byte)0, 16));
        }

        [Fact]
        public void CanRemoveLastItemFromFullPage()
        {
            var list = new LargeGrowingArrayUInt2();
            list.AddAll(Enumerable.Repeat((byte)0, 16));
            list.RemoveLast();
            Assert.Equal(list, Enumerable.Repeat((byte)0, 15));
        }

        [Fact]
        public void CanGrowAddRemoveMultipleTimes()
        {
            var list = new LargeGrowingArrayUInt2();
            list.EnsureMinCapacity(6);
            list.AddAll(new byte[] { 1, 2, 3, 1, 2, 3 });
            list.RemoveLast(2);
            list.EnsureMinCapacity(list.Count + 10);
            list.AddAll(new byte[] { 1, 2, 3, 1, 2, 3, 1, 2, 0, 3 });
            list.RemoveLast(8);
            list.EnsureMinCapacity(list.Count + 6);
            list.AddAll(new byte[] { 1, 2, 3, 0, 1, 2 });
            list.RemoveLast(3);
            list.EnsureMinCapacity(list.Count + 3);
            list.AddAll(new byte[] { 1, 2, 3 });
            Assert.Equal(list, new byte[] { 1, 2, 3, 1, 1, 2, 1, 2, 3, 1, 2, 3 });
        }

        [Fact]
        public void CanEnumerateEmptyArray()
        {
            var list = new LargeGrowingArrayUInt2();
            Assert.Empty(list);
        }

        [Fact]
        public void CanEnumerateOneNonFullPage()
        {
            var list = new LargeGrowingArrayUInt2();
            list.Add(2);
            Assert.Equal(list, new byte[] { 2 });
        }

        [Fact]
        public void CanEnumerateOneFullPage()
        {
            var list = new LargeGrowingArrayUInt2();
            var items = new byte[] { 1, 2, 3, 0, 2, 3, 1, 0, 0, 1, 1, 2, 2, 3, 3, 3 };
            list.AddAll(items);
            Assert.Equal(list, items);
        }

        [Fact]
        public void CanEnumerateOneFullAndOneNonFullPage()
        {
            var list = new LargeGrowingArrayUInt2();
            var items = new byte[] { 1, 2, 3, 0, 2, 3, 1, 0, 0, 1, 1, 2, 2, 3, 3, 3, 1, 2, 3, 0, 3, 2, 1 };
            list.AddAll(items);
            Assert.Equal(list, items);
        }

        [Fact]
        public void CanEnumerateTwoFullPages()
        {
            var list = new LargeGrowingArrayUInt2();
            var items =
                new byte[] { 1, 2, 3, 0, 2, 3, 1, 0, 0, 1, 1, 2, 2, 3, 3, 3, 1, 2, 3, 0, 3, 2, 1, 0, 1, 1, 1, 1, 2, 2, 2, 2 };
            list.AddAll(items);
            Assert.Equal(list, items);
        }
    }
}
