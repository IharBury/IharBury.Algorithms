using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class LargeListTests
    {
        [Fact]
        public void NewListIsEmpty()
        {
            var list = new LargeList<int>();
            Assert.Equal(0L, list.Count);
        }

        [Fact]
        public void AddOneItemToEmptyList()
        {
            var list = new LargeList<int>();
            list.Add(4);
            Assert.Equal(1L, list.Count);
            Assert.Equal(4, list[0]);
        }

        [Fact]
        public void AddOneItemToNonEmptyList()
        {
            var list = new LargeList<int>();
            list.Add(4);
            list.Add(7);
            Assert.Equal(2L, list.Count);
            Assert.Equal(7, list[1]);
        }

        [Fact]
        public void AddOneItemSoPageIsFull()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(4);
            list.Add(7);
            Assert.Equal(2L, list.Count);
            Assert.Equal(7, list[1]);
        }

        [Fact]
        public void AddOneItemToNewPage()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(4);
            list.Add(7);
            list.Add(2);
            Assert.Equal(3L, list.Count);
            Assert.Equal(2, list[2]);
        }

        [Fact]
        public void RemoveLastItemFromListWithMoreThanOneItem()
        {
            var list = new LargeList<int>();
            list.Add(2);
            list.Add(7);
            list.RemoveAt(list.Count - 1);
            Assert.Equal(1L, list.Count);
        }

        [Fact]
        public void RemoveLastItemFromListWithOneItem()
        {
            var list = new LargeList<int>();
            list.Add(2);
            list.RemoveAt(list.Count - 1);
            Assert.Equal(0L, list.Count);
        }

        [Fact]
        public void RemoveLastItemFromPageWithOneItem()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(2);
            list.Add(7);
            list.Add(3);
            list.RemoveAt(list.Count - 1);
            Assert.Equal(2L, list.Count);
        }

        [Fact]
        public void RemoveLastItemFromFullPage()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(2);
            list.Add(7);
            list.RemoveAt(list.Count - 1);
            Assert.Equal(1L, list.Count);
        }

        [Fact]
        public void AddEmptyRange()
        {
            var list = new LargeList<int>();
            list.AddAll(new int[] { }.AsLargeReadOnlyCollection());
            Assert.Equal(0L, list.Count);
        }

        [Fact]
        public void AddOneItemAsRangeWithCountToEmptyList()
        {
            var list = new LargeList<int>();
            list.AddAll(new[] { 4 }.AsLargeReadOnlyCollection());
            Assert.Equal(1L, list.Count);
            Assert.Equal(4, list[0]);
        }

        [Fact]
        public void AddOneItemAsRangeToNonEmptyList()
        {
            var list = new LargeList<int>();
            list.Add(4);
            list.AddAll(new[] { 7 }.AsLargeReadOnlyCollection());
            Assert.Equal(2L, list.Count);
            Assert.Equal(7, list[1]);
        }

        [Fact]
        public void AddOneItemAsRangeSoPageIsFull()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(4);
            list.AddAll(new[] { 7 }.AsLargeReadOnlyCollection());
            Assert.Equal(2L, list.Count);
            Assert.Equal(7, list[1]);
        }

        [Fact]
        public void AddOneItemAsRangeToNewPage()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(4);
            list.Add(7);
            list.AddAll(new[] { 2 }.AsLargeReadOnlyCollection());
            Assert.Equal(3L, list.Count);
            Assert.Equal(2, list[2]);
        }

        [Fact]
        public void AddManyItemsAsRangeWithCountToEmptyList()
        {
            var list = new LargeList<int>();
            list.AddAll(new[] { 4, 1, 3 }.AsLargeReadOnlyCollection());
            Assert.Equal(3L, list.Count);
            Assert.Equal(4, list[0]);
            Assert.Equal(1, list[1]);
            Assert.Equal(3, list[2]);
        }

        [Fact]
        public void AddManyItemsAsRangeToNonEmptyList()
        {
            var list = new LargeList<int>();
            list.Add(4);
            list.AddAll(new[] { 4, 1, 3 }.AsLargeReadOnlyCollection());
            Assert.Equal(4L, list.Count);
            Assert.Equal(4, list[1]);
            Assert.Equal(1, list[2]);
            Assert.Equal(3, list[3]);
        }

        [Fact]
        public void AddManyItemsAsRangeSoPageIsFull()
        {
            var list = new LargeList<int>(pageSize: 4);
            list.Add(4);
            list.AddAll(new[] { 4, 1, 3 }.AsLargeReadOnlyCollection());
            Assert.Equal(4L, list.Count);
            Assert.Equal(4, list[1]);
            Assert.Equal(1, list[2]);
            Assert.Equal(3, list[3]);
        }

        [Fact]
        public void AddManyItemAsRangeToNewPage()
        {
            var list = new LargeList<int>(pageSize: 4);
            list.Add(4);
            list.Add(7);
            list.Add(11);
            list.Add(3);
            list.AddAll(new[] { 4, 1, 3 }.AsLargeReadOnlyCollection());
            Assert.Equal(7L, list.Count);
            Assert.Equal(4, list[4]);
            Assert.Equal(1, list[5]);
            Assert.Equal(3, list[6]);
        }

        [Fact]
        public void AddManyItemAsRangeCrossPage()
        {
            var list = new LargeList<int>(pageSize: 4);
            list.Add(4);
            list.Add(7);
            list.Add(11);
            list.AddAll(new[] { 4, 1, 3 }.AsLargeReadOnlyCollection());
            Assert.Equal(6L, list.Count);
            Assert.Equal(4, list[3]);
            Assert.Equal(1, list[4]);
            Assert.Equal(3, list[5]);
        }

        [Fact]
        public void AddManyItemAsRangeCrossManyPages()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(4);
            list.AddAll(new[] { 4, 1, 3, 6, 11 }.AsLargeReadOnlyCollection());
            Assert.Equal(6L, list.Count);
            Assert.Equal(4, list[1]);
            Assert.Equal(1, list[2]);
            Assert.Equal(3, list[3]);
            Assert.Equal(6, list[4]);
            Assert.Equal(11, list[5]);
        }

        [Fact]
        public void RemoveLastItemAsRangeFromListWithMoreThanOneItem()
        {
            var list = new LargeList<int>();
            list.Add(2);
            list.Add(7);
            list.RemoveRange(list.Count - 1, 1);
            Assert.Equal(1L, list.Count);
        }

        [Fact]
        public void RemoveLastItemAsRangeFromListWithOneItem()
        {
            var list = new LargeList<int>();
            list.Add(2);
            list.RemoveRange(list.Count - 1, 1);
            Assert.Equal(0L, list.Count);
        }

        [Fact]
        public void RemoveLastItemAsRangeFromPageWithOneItem()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(2);
            list.Add(7);
            list.Add(3);
            list.RemoveRange(list.Count - 1, 1);
            Assert.Equal(2L, list.Count);
        }

        [Fact]
        public void RemoveLastItemAsRangeFromFullPage()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.Add(2);
            list.Add(7);
            list.RemoveRange(list.Count - 1, 1);
            Assert.Equal(1L, list.Count);
        }

        [Fact]
        public void ManyRangeAddAndRemove()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.AddAll(new[] { 1, 2, 3, 4, 5, 6 }.AsLargeReadOnlyCollection());
            list.RemoveRange(list.Count - 2, 2);
            list.AddAll(new[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }.AsLargeReadOnlyCollection());
            list.RemoveRange(list.Count - 8, 8);
            list.AddAll(new[] { 17, 18, 19, 20, 21, 22 }.AsLargeReadOnlyCollection());
            list.RemoveRange(list.Count - 3, 3);
            list.AddAll(new[] { 23, 24, 25 }.AsLargeReadOnlyCollection());
            Assert.Equal(12L, list.Count);
            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
            Assert.Equal(4, list[3]);
            Assert.Equal(7, list[4]);
            Assert.Equal(8, list[5]);
            Assert.Equal(17, list[6]);
            Assert.Equal(18, list[7]);
            Assert.Equal(19, list[8]);
            Assert.Equal(23, list[9]);
            Assert.Equal(24, list[10]);
            Assert.Equal(25, list[11]);
        }

        [Fact]
        public void EmptyListEnumeration()
        {
            var list = new LargeList<int>();
            Assert.True(list.SequenceEqual(new int[] { }));
        }

        [Fact]
        public void OneNonFullPageListEnumeration()
        {
            var list = new LargeList<int>();
            list.Add(7);
            Assert.True(list.SequenceEqual(new[] { 7 }));
        }

        [Fact]
        public void OneFullPageListEnumeration()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.AddAll(new[] { 7, 3 }.AsLargeReadOnlyCollection());
            Assert.True(list.SequenceEqual(new int[] { 7, 3 }));
        }

        [Fact]
        public void OneFullAndOneNonFullPageListEnumeration()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.AddAll(new[] { 7, 3, 5 }.AsLargeReadOnlyCollection());
            Assert.True(list.SequenceEqual(new int[] { 7, 3, 5 }));
        }

        [Fact]
        public void TwoFullPagesListEnumeration()
        {
            var list = new LargeList<int>(pageSize: 2);
            list.AddAll(new[] { 7, 3, 5, 1 }.AsLargeReadOnlyCollection());
            Assert.True(list.SequenceEqual(new int[] { 7, 3, 5, 1 }));
        }

        [Fact]
        public void InsertMoreThanOnePage()
        {
            var list = new LargeList<int>(pageSize: 3);
            list.AddAll(new[] { 7, 3, 5, 1, 9 }.AsLargeReadOnlyCollection());
            list.InsertAll(2, new[] { 4, 2, 6, 3, 8 }.AsLargeReadOnlyCollection());

            Assert.Equal(10, list.Count);
            Assert.True(list.SequenceEqual(new int[] { 7, 3, 4, 2, 6, 3, 8, 5, 1, 9 }));
        }
    }
}
