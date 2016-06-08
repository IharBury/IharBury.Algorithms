using System;
using System.Collections;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class CollectionExtensions
    {
        public static ILargeReadOnlyCollection<T> AsLargeReadOnlyCollection<T>(this IReadOnlyCollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return new LargeReadOnlyCollectionAdapter<T>(collection);
        }

        private sealed class LargeReadOnlyCollectionAdapter<T> : ILargeReadOnlyCollection<T>
        {
            private readonly IReadOnlyCollection<T> innerCollection;

            public LargeReadOnlyCollectionAdapter(IReadOnlyCollection<T> innerCollection)
            {
                if (innerCollection == null)
                    throw new ArgumentNullException(nameof(innerCollection));

                this.innerCollection = innerCollection;
            }

            public long Count => innerCollection.Count;

            public IEnumerator<T> GetEnumerator() => innerCollection.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
