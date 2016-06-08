using System;
using System.Collections;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public class LargeReadOnlyList<T> : ILargeReadOnlyList<T>
    {
        private readonly ILargeReadOnlyList<T> innerList;

        public LargeReadOnlyList(ILargeReadOnlyList<T> innerList)
        {
            if (innerList == null)
                throw new ArgumentNullException(nameof(innerList));

            this.innerList = innerList;
        }

        public T this[long index] => innerList[index];

        public long Count => innerList.Count;

        public IEnumerator<T> GetEnumerator() => innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
