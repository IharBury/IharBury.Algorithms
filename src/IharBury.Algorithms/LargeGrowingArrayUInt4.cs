using System;
using System.Collections;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public sealed class LargeGrowingArrayUInt4 : IEnumerable<byte>
    {
        private const int DefaultInnerPageSize = 0x10000000;
        private const int BitsPerItem = 4;
        private const int ItemBitMask = 0xF;
        private const int BitsPerInt32 = 32;
        private const int ItemsPerInt32 = BitsPerInt32 / BitsPerItem;

        private readonly LargeGrowingArray<int> innerList;

        public LargeGrowingArrayUInt4(
            long initialCapacity = 0,
            int innerPageSize = DefaultInnerPageSize)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            if (innerPageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(innerPageSize));

            innerList = new LargeGrowingArray<int>(initialCapacity.GetPageCount(ItemsPerInt32), innerPageSize);
        }

        public long Count { get; private set; }

        public byte this[long index]
        {
            get
            {
                if ((index < 0) || (index >= Count))
                    throw new ArgumentOutOfRangeException(nameof(index));

                var bitContainer = innerList[index.GetPageIndex(ItemsPerInt32)];
                var bitShift = index.GetIndexOnPage(ItemsPerInt32) * BitsPerItem;
                return (byte)((bitContainer >> bitShift) & ItemBitMask);
            }
            set
            {
                if ((index < 0) || (index >= Count))
                    throw new ArgumentOutOfRangeException(nameof(index));
                if ((value & ~ItemBitMask) != 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                var bitContainer = innerList[index.GetPageIndex(ItemsPerInt32)];
                var bitShift = index.GetIndexOnPage(ItemsPerInt32) * BitsPerItem;
                var itemInContainerMask = ItemBitMask << bitShift;
                var itemInContainer = value << bitShift;
                bitContainer = (bitContainer & ~itemInContainerMask) | itemInContainer;
                innerList[index.GetPageIndex(ItemsPerInt32)] = bitContainer;
            }
        }

        public void RemoveLast(long count = 1)
        {
            if ((count < 0) || (count > Count))
                throw new ArgumentOutOfRangeException(nameof(count));

            var newCount = checked(Count - count);
            var newInnerCount = newCount.GetPageCount(ItemsPerInt32);
            innerList.RemoveLast(innerList.Count - newInnerCount);
            Count = newCount;
        }

        public void Add(byte item)
        {
            if ((item & ~ItemBitMask) != 0)
                throw new ArgumentOutOfRangeException(nameof(item));

            var newCount = checked(Count + 1);
            var newInnerCount = newCount.GetPageCount(ItemsPerInt32);
            EnsureMinInnerCount(newInnerCount);
            Count = newCount;
            this[newCount - 1] = item;
        }

        public void AddAll(IEnumerable<byte> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                Add(item);
        }

        private void EnsureMinInnerCount(long innerCount)
        {
            if (innerCount < 0)
                throw new ArgumentOutOfRangeException(nameof(innerCount));

            innerList.EnsureMinCapacity(innerCount);
            while (innerCount > innerList.Count)
                innerList.Add(0);
        }

        public void Clear()
        {
            innerList.Clear();
            Count = 0;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            var innerIndex = 0L;
            var countLeft = Count;
            while (countLeft >= ItemsPerInt32)
            {
                var itemBuffer = innerList[innerIndex];
                for (var itemIndexInBuffer = 0; itemIndexInBuffer < ItemsPerInt32; itemIndexInBuffer++)
                {
                    yield return (byte)(itemBuffer & ItemBitMask);
                    itemBuffer >>= BitsPerItem;
                }

                innerIndex++;
                countLeft -= ItemsPerInt32;
            }

            if (countLeft > 0)
            {
                var itemBuffer = innerList[innerIndex];
                while (countLeft > 0)
                {
                    yield return (byte)(itemBuffer & ItemBitMask);
                    itemBuffer >>= BitsPerItem;
                    countLeft--;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void EnsureMinCapacity(long capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            innerList.EnsureMinCapacity(capacity.GetPageCount(ItemsPerInt32));
        }
    }
}
