namespace IharBury.Algorithms
{
    public interface ILargeList<T> : ILargeCollection<T>, ILargeReadOnlyList<T>
    {
        new T this[long index] { get; set; }
        void Insert(long index, T item);
        void InsertAll(long index, ILargeReadOnlyCollection<T> items);
        void RemoveAt(long index);
        void RemoveRange(long index, long count);
    }
}
