using System;
using System.Collections;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public sealed class LargeTwoBitList<T> : ILargeList<T>
    {
        private const int DefaultPageSize = 0x10000000;
        private const int BitsPerItem = 2;
        private const int ItemBitMask = 0x3;
        private const int BitsPerInt32 = 32;
        private const int ItemsPerInt32 = BitsPerInt32 / BitsPerItem;

        private readonly Func<T, int> convertItemToBits;
        private readonly Func<int, T> convertBitsToItem;
        private readonly LargeList<int> innerList;

        public LargeTwoBitList(
            Func<T, int> convertItemToBits,
            Func<int, T> convertBitsToItem,
            long initialCapacity = 0,
            int pageSize = DefaultPageSize)
        {
            if (convertItemToBits == null)
                throw new ArgumentNullException(nameof(convertItemToBits));
            if (convertBitsToItem == null)
                throw new ArgumentNullException(nameof(convertBitsToItem));
            if (initialCapacity < 0)
                throw new ArgumentException("Initial capacity is negative.", nameof(initialCapacity));
            if (pageSize <= 0)
                throw new ArgumentException("Page size is not positive.", nameof(pageSize));

            this.convertItemToBits = convertItemToBits;
            this.convertBitsToItem = convertBitsToItem;
            innerList = new LargeList<int>(initialCapacity.GetPageCount(ItemsPerInt32), pageSize);
        }

        public long Count { get; private set; }

        T ILargeReadOnlyList<T>.this[long index] => this[index];

        public T this[long index]
        {
            get
            {
                return convertBitsToItem(GetBitsAt(index));
            }
            set
            {
                SetBitsAt(index, convertItemToBits(value));
            }
        }

        private int GetBitsAt(long index)
        {
            if (index < 0)
                throw new ArgumentException("Index is negative.", nameof(index));
            if (index >= Count)
                throw new ArgumentException("Index is too large.", nameof(index));

            var bitContainer = innerList[index.GetPageIndex(ItemsPerInt32)];
            var bitShift = index.GetIndexOnPage(ItemsPerInt32) * BitsPerItem;
            return (bitContainer >> bitShift) & ItemBitMask;
        }

        private void SetBitsAt(long index, int itemBits)
        {
            if (index < 0)
                throw new ArgumentException("Index is negative.", nameof(index));
            if (index >= Count)
                throw new ArgumentException("Index is too large.", nameof(index));

            var bitContainer = innerList[index.GetPageIndex(ItemsPerInt32)];
            var bitShift = index.GetIndexOnPage(ItemsPerInt32) * BitsPerItem;
            var itemInContainerMask = ItemBitMask << bitShift;
            var itemInContainer = (itemBits & ItemBitMask) << bitShift;
            bitContainer = (bitContainer & ~itemInContainerMask) | itemInContainer;
            innerList[index.GetPageIndex(ItemsPerInt32)] = bitContainer;
        }

        public void Insert(long index, T item)
        {
            InsertAll(index, new[] { item }.AsLargeReadOnlyCollection());
        }

        public void InsertAll(long index, ILargeReadOnlyCollection<T> items)
        {
            if (index < 0)
                throw new ArgumentException("Index is negative.", nameof(index));
            if (index > Count)
                throw new ArgumentException("Index is too large.", nameof(index));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var countAfterRange = checked(index + items.Count);
            var newCount = checked(Count + items.Count);
            var newInnerCount = newCount.GetPageCount(ItemsPerInt32);
            EnsureInnerCount(newInnerCount);
            Count = newCount;

            for (var currentMovingIndex = newCount - 1; currentMovingIndex >= countAfterRange; currentMovingIndex--)
                SetBitsAt(currentMovingIndex, GetBitsAt(checked(currentMovingIndex - items.Count)));

            var currentInsertingIndex = index;
            foreach (var item in items)
            {
                this[currentInsertingIndex] = item;
                currentInsertingIndex++;
            }
        }

        public void RemoveAt(long index)
        {
            RemoveRange(index, 1);
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
                SetBitsAt(checked(currentIndex - count), GetBitsAt(currentIndex));

            var newInnerCount = newCount.GetPageCount(ItemsPerInt32);
            innerList.RemoveRange(newInnerCount, innerList.Count - newInnerCount);
            Count = newCount;
        }

        public void Add(T item)
        {
            var newCount = checked(Count + 1);
            var newInnerCount = newCount.GetPageCount(ItemsPerInt32);
            EnsureInnerCount(newInnerCount);
            Count = newCount;
            this[newCount - 1] = item;
        }

        public void AddAll(ILargeReadOnlyCollection<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var oldCount = Count;
            var newCount = checked(Count + items.Count);
            var newInnerCount = newCount.GetPageCount(ItemsPerInt32);
            EnsureInnerCount(newInnerCount);
            Count = newCount;
            var currentIndex = oldCount;
            foreach (var item in items)
            {
                this[currentIndex] = item;
                currentIndex++;
            }
        }

        private void EnsureInnerCount(long innerCount)
        {
            if (innerCount < 0)
                throw new ArgumentException("Inner count is negative.", nameof(innerCount));

            innerList.EnsureCapacity(innerCount);
            while (innerCount > innerList.Count)
                innerList.Add(0);
        }

        public void Clear()
        {
            innerList.Clear();
            Count = 0;
        }

        public bool Contains(T item)
        {
            return FindIndexOf(item).HasValue;
        }

        public bool Remove(T item)
        {
            var index = FindIndexOf(item);
            if (index == null)
                return false;

            RemoveAt(index.Value);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0L; index < Count; index++)
                yield return this[index];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private long? FindIndexOf(T item)
        {
            var itemBits = convertItemToBits(item) & ItemBitMask;

            for (var index = 0L; index < Count; index++)
                if (GetBitsAt(index) == itemBits)
                    return index;

            return null;
        }

        public void EnsureCapacity(long capacity)
        {
            innerList.EnsureCapacity(capacity.GetPageCount(ItemsPerInt32));
        }
    }
}
