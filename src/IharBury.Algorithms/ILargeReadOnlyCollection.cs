using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public interface ILargeReadOnlyCollection<out T> : IEnumerable<T>
    {
        long Count { get; }
    }
}
