using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IharBury.Algorithms
{
    public sealed class LargeGrowingArray<T> : IEnumerable<T>
    {
        private const int DefaultPageSize = 0x10000000;

        private readonly List<List<T>> pages;
        private readonly int pageSize;

        public LargeGrowingArray(
            long initialCapacity = 0,
            int pageSize = DefaultPageSize)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            this.pageSize = pageSize;

            var pageCount = checked((int)initialCapacity.GetPageCount(pageSize));
            var lastPageSize = initialCapacity.GetLastPageSize(pageSize);
            pages = new List<List<T>>((int)pageCount);
            for (var chunkIndex = 0; chunkIndex < pageCount - 1; chunkIndex++)
                pages.Add(new List<T>(pageSize));
            if (pageCount > 0)
                pages.Add(new List<T>(lastPageSize));
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

        public long Count { get; private set; }

        public void Add(T item)
        {
            var newCount = checked(Count + 1);
            EnsureMinCapacity(newCount);
            pages[checked((int)Count.GetPageIndex(pageSize))].Add(item);
            Count = newCount;
        }

        public void AddAll(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                Add(item);
        }

        public void Clear()
        {
            foreach (var page in pages)
                page.Clear();
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator() => pages.SelectMany(page => page).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void RemoveLast(long count = 1)
        {
            if ((count < 0) || (count > Count))
                throw new ArgumentOutOfRangeException(nameof(count));

            var newCount = checked(Count - count);

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

        public void EnsureMinCapacity(long capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            var requiredPageCount = checked((int)capacity.GetPageCount(pageSize));
            if (requiredPageCount > pages.Capacity)
            {
                var standardPageListIncreasedCapacity = 
                    checked(pages.Capacity > int.MaxValue / 2 ? int.MaxValue : pages.Capacity * 2).ButMin(16);
                pages.Capacity = requiredPageCount.ButMin(standardPageListIncreasedCapacity);
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
            if ((capacity < 0) || (capacity > pageSize))
                throw new ArgumentOutOfRangeException(nameof(capacity));

            if (capacity > page.Capacity)
            {
                var standardIncreasedCapacity = 
                    (page.Capacity > pageSize / 2 ? pageSize : page.Capacity * 2).ButMin(16).ButMax(pageSize);
                page.Capacity = capacity.ButMin(standardIncreasedCapacity);
            }
        }
    }
}
