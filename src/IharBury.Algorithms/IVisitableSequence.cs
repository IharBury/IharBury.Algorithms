using System;

namespace IharBury.Algorithms
{
    public interface IVisitableSequence<T>
    {
        /// <remarks>
        /// About 40% faster than <see cref="System.Collections.Generic.IEnumerable{T}" />
        /// for collections with just several items.
        /// </remarks>
        void ForEach(Action<T> action, ICancellation cancellation = null);
    }
}
