using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public interface ILargeCollection<T> : ILargeReadOnlyCollection<T>
    {
        void Add(T item);
        void AddAll(ILargeReadOnlyCollection<T> items);
        void Clear();
        bool Contains(T item);
        bool Remove(T item);
    }
}
