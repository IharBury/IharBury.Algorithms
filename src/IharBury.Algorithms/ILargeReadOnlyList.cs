namespace IharBury.Algorithms
{
    public interface ILargeReadOnlyList<out T> : ILargeReadOnlyCollection<T>
    {
        T this[long index] { get; }
    }
}
