using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IharBury.Algorithms
{
    public sealed class LargeList<T> : ILargeList<T>
    {
        private const int DefaultPageSize = 0x10000000;

        private readonly List<List<T>> pages;
        private readonly int pageSize;
        private readonly IEqualityComparer<T> equalityComparer;

        public LargeList(
            long initialCapacity = 0,
            int pageSize = DefaultPageSize,
            IEqualityComparer<T> equalityComparer = null)
        {
            if (initialCapacity < 0)
                throw new ArgumentException("Initial capacity is negative.", nameof(initialCapacity));
            if (pageSize <= 0)
                throw new ArgumentException("Page size is not positive.", nameof(pageSize));

            this.pageSize = pageSize;
            this.equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;

            var pageCount = checked((int)initialCapacity.GetPageCount(pageSize));
            var lastPageSize = initialCapacity.GetLastPageSize(pageSize);
            pages = new List<List<T>>((int)pageCount);
            for (var chunkIndex = 0; chunkIndex < pageCount - 1; chunkIndex++)
                pages.Add(new List<T>(pageSize));
            if (pageCount > 0)
                pages[(int)pageCount - 1] = new List<T>(lastPageSize);
        }

        public T this[long index]
        {
            get
            {
                return pages[checked((int)index.GetPageIndex(pageSize))][index.GetIndexOnPage(pageSize)];
            }
            set
            {
                pages[checked((int)index.GetPageIndex(pageSize))][index.GetIndexOnPage(pageSize)] = value;
            }
        }

        T ILargeReadOnlyList<T>.this[long index] => this[index];

        public long Count { get; private set; }

        public void Add(T item)
        {
            var newCount = checked(Count + 1);
            EnsureCapacity(newCount);
            pages[checked((int)Count.GetPageIndex(pageSize))].Add(item);
            Count = newCount;
        }

        public void Clear()
        {
            foreach (var page in pages)
                page.Clear();
            Count = 0;
        }

        public bool Contains(T item) => pages.Any(page => page.Contains(item, equalityComparer));

        public IEnumerator<T> GetEnumerator() => pages.SelectMany(page => page).GetEnumerator();

        public void Insert(long index, T value)
        {
            InsertAll(index, new[] { value }.AsLargeReadOnlyCollection());
        }

        public bool Remove(T item)
        {
            var index = FindIndexOf(item);
            if (index == null)
                return false;

            RemoveAt(index.Value);
            return true;
        }

        public void RemoveAt(long index)
        {
            RemoveRange(index, 1);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void InsertAll(long index, ILargeReadOnlyCollection<T> items)
        {
            if (index < 0)
                throw new ArgumentException("Index is negative.", nameof(index));

            var countAfterRange = checked(index + items.Count);
            var newCount = checked(Count + items.Count);
            EnsureCapacity(newCount);
            var pageIndex = checked((int)Count.GetPageIndex(pageSize));
            for (var itemIndex = 0L; itemIndex < items.Count; itemIndex++)
            {
                pages[pageIndex].Add(default(T));
                if ((pages[pageIndex].Count == pageSize))
                    pageIndex++;
            }

            for (var currentMovingIndex = newCount - 1; currentMovingIndex >= countAfterRange; currentMovingIndex--)
                this[currentMovingIndex] = this[checked(currentMovingIndex - items.Count)];

            var currentInsertingIndex = index;
            foreach (var item in items)
            {
                this[currentInsertingIndex] = item;
                currentInsertingIndex++;
            }

            Count = newCount;
        }

        public void RemoveRange(long index, long count)
        {
            if (index < 0)
                throw new ArgumentException("Index is negative.", nameof(index));
            var countAfterRange = checked(index + count);
            if (countAfterRange > Count)
                throw new ArgumentException("Range is not contained in the list.");

            var newCount = checked(Count - count);
            for (var currentIndex = countAfterRange; currentIndex < Count; currentIndex++)
                this[checked(currentIndex - count)] = this[currentIndex];

            var requiredPageCount = checked((int)newCount.GetPageCount(pageSize));
            for (var pageIndex = requiredPageCount; pageIndex < pages.Count; pageIndex++)
                pages[pageIndex].Clear();
            if (requiredPageCount > 0)
            {
                var requiredLastPage = pages[requiredPageCount - 1];
                var requiredLastPageSize = newCount.GetLastPageSize(pageSize);
                if (requiredLastPage.Count > requiredLastPageSize)
                    requiredLastPage.RemoveRange(requiredLastPageSize, requiredLastPage.Count - requiredLastPageSize);
            }
            Count = newCount;
        }

        public void AddAll(ILargeReadOnlyCollection<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items.Count == 0)
                return;

            var newCount = checked(Count + items.Count);
            EnsureCapacity(newCount);
            var pageIndex = checked((int)Count.GetPageIndex(pageSize));
            foreach (var item in items)
            {
                pages[pageIndex].Add(item);
                if ((pages[pageIndex].Count == pageSize))
                    pageIndex++;
            }
            Count = newCount;
        }

        private void EnsureCapacity(long capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Capacity is negative.", nameof(capacity));

            var requiredPageCount = checked((int)capacity.GetPageCount(pageSize));
            if (requiredPageCount > pages.Capacity)
            {
                var standardPageListIncreasedCapacity = 
                    checked(pages.Capacity > int.MaxValue / 2 ? int.MaxValue : pages.Capacity * 2).Max(16);
                pages.Capacity = requiredPageCount.Max(standardPageListIncreasedCapacity);
            }
            if ((requiredPageCount > pages.Count) && (pages.Count > 0))
                EnsurePageCapacity(pages[pages.Count - 1], pageSize);
            while (requiredPageCount - 1 > pages.Count)
                pages.Add(new List<T>(pageSize));
            if (requiredPageCount > pages.Count)
                pages.Add(new List<T>(capacity.GetLastPageSize(pageSize)));
            if (pages.Count > 0)
                EnsurePageCapacity(pages[pages.Count - 1], capacity.GetLastPageSize(pageSize));
        }

        private void EnsurePageCapacity(List<T> page, int capacity)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            if (capacity < 0)
                throw new ArgumentException("Capacity is negative.", nameof(capacity));
            if (capacity > pageSize)
                throw new ArgumentException("Capacity is greater than page size.", nameof(capacity));

            if (capacity > page.Capacity)
            {
                var standardIncreasedCapacity = 
                    (page.Capacity > pageSize / 2 ? pageSize : page.Capacity * 2).Max(16).Min(pageSize);
                page.Capacity = capacity.Max(standardIncreasedCapacity);
            }
        }

        private long? FindIndexOf(T item)
        {
            for (var pageIndex = 0; pageIndex < Count.GetPageCount(pageSize); pageIndex++)
            {
                var page = pages[pageIndex];
                for (var indexOnPage = 0; indexOnPage < page.Count; indexOnPage++)
                    if (equalityComparer.Equals(page[indexOnPage], item))
                        return pageIndex * (long)pageSize + indexOnPage;
            }

            return null;
        }
    }
}
