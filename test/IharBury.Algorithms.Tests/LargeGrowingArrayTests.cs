﻿using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class LargeGrowingArrayTests
    {
        [Fact]
        public void NewArrayIsEmpty()
        {
            var list = new LargeGrowingArray<int>();
            Assert.Empty(list);
        }

        [Fact]
        public void CanAddOneItemToEmptyArray()
        {
            var list = new LargeGrowingArray<int>();
            list.Add(4);
            Assert.Equal(list, new[] { 4 });
        }

        [Fact]
        public void CanAddOneItemToNonEmptyArray()
        {
            var list = new LargeGrowingArray<int>();
            list.Add(4);
            list.Add(7);
            Assert.Equal(list, new[] { 4, 7 });
        }

        [Fact]
        public void CanAddOneItemSoThatPageIsFull()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.Add(4);
            list.Add(7);
            Assert.Equal(list, new[] { 4, 7 });
        }

        [Fact]
        public void CanAddOneItemToNewPage()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.Add(4);
            list.Add(7);
            list.Add(2);
            Assert.Equal(list, new[] { 4, 7, 2 });
        }

        [Fact]
        public void CanRemoveLastItemFromArrayWithMoreThanOneItem()
        {
            var list = new LargeGrowingArray<int>();
            list.Add(2);
            list.Add(7);
            list.RemoveLast();
            Assert.Equal(list, new[] { 2 });
        }

        [Fact]
        public void CanRemoveLastItemFromArrayWithOneItem()
        {
            var list = new LargeGrowingArray<int>();
            list.Add(2);
            list.RemoveLast();
            Assert.Empty(list);
        }

        [Fact]
        public void CanRemoveLastItemFromPageWithOneItem()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.Add(2);
            list.Add(7);
            list.Add(3);
            list.RemoveLast();
            Assert.Equal(list, new[] { 2, 7 });
        }

        [Fact]
        public void CanRemoveLastItemFromFullPage()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.Add(2);
            list.Add(7);
            list.RemoveLast();
            Assert.Equal(list, new[] { 2 });
        }

        [Fact]
        public void CanGrowAddRemoveMultipleTimes()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.EnsureMinCapacity(6);
            list.AddAll(new[] { 1, 2, 3, 4, 5, 6 });
            list.RemoveLast(2);
            list.EnsureMinCapacity(list.Count + 10);
            list.AddAll(new[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            list.RemoveLast(8);
            list.EnsureMinCapacity(list.Count + 6);
            list.AddAll(new[] { 17, 18, 19, 20, 21, 22 });
            list.RemoveLast(3);
            list.EnsureMinCapacity(list.Count + 3);
            list.AddAll(new[] { 23, 24, 25 });
            Assert.Equal(list, new[] { 1, 2, 3, 4, 7, 8, 17, 18, 19, 23, 24, 25 });
        }

        [Fact]
        public void CanEnumerateEmptyArray()
        {
            var list = new LargeGrowingArray<int>();
            Assert.Empty(list);
        }

        [Fact]
        public void CanEnumerateOneNonFullPage()
        {
            var list = new LargeGrowingArray<int>();
            list.Add(7);
            Assert.Equal(list, new[] { 7 });
        }

        [Fact]
        public void CanEnumerateOneFullPage()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.AddAll(new[] { 7, 3 });
            Assert.Equal(list, new int[] { 7, 3 });
        }

        [Fact]
        public void CanEnumerateOneFullAndOneNonFullPage()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.AddAll(new[] { 7, 3, 5 });
            Assert.Equal(list, new int[] { 7, 3, 5 });
        }

        [Fact]
        public void CanEnumerateTwoFullPages()
        {
            var list = new LargeGrowingArray<int>(pageSize: 2);
            list.AddAll(new[] { 7, 3, 5, 1 });
            Assert.Equal(list, new int[] { 7, 3, 5, 1 });
        }
    }
}
