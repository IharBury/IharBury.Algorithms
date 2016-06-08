using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IharBury.Algorithms
{
    public sealed class LargeArray<T> : ILargeArray<T>
    {
        private const int DefaultPageSize = 0x10000000;

        private readonly int pageSize;
        private readonly T[][] pages;
        private readonly IEqualityComparer<T> equalityComparer;

        public LargeArray(long count, int pageSize = DefaultPageSize, IEqualityComparer<T> equalityComparer = null)
        {
            if (count < 0)
                throw new ArgumentException("Count is negative.", nameof(count));
            if (pageSize <= 0)
                throw new ArgumentException("Page size is not positive.", nameof(pageSize));

            Count = count;
            this.pageSize = pageSize;
            this.equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;

            var pageCount = count.GetPageCount(pageSize);
            var lastPageSize = count.GetLastPageSize(pageSize);
            pages = new T[pageCount][];
            for (var chunkIndex = 0; chunkIndex < pageCount - 1; chunkIndex++)
                pages[chunkIndex] = new T[pageSize];
            if (pageCount > 0)
                pages[pageCount - 1] = new T[lastPageSize];
        }

        public T this[long index]
        {
            get
            {
                checked
                {
                    return pages[index.GetPageIndex(pageSize)][index.GetIndexOnPage(pageSize)];
                }
            }
            set
            {
                checked
                {
                    pages[index.GetPageIndex(pageSize)][index.GetIndexOnPage(pageSize)] = value;
                }
            }
        }

        T ILargeReadOnlyList<T>.this[long index] => this[index];

        public long Count { get; }

        public bool Contains(T item) => pages.Any(chunk => chunk.Contains(item, equalityComparer));

        public IEnumerator<T> GetEnumerator() => pages.SelectMany(page => page).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
