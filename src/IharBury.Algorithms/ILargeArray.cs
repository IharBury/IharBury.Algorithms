namespace IharBury.Algorithms
{
    public interface ILargeArray<T> : ILargeReadOnlyList<T>
    {
        new T this[long index] { get; set; }
        bool Contains(T item);
    }
}
