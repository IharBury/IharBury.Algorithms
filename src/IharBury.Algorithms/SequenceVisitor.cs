using System;

namespace IharBury.Algorithms
{
    /// <remarks>
    /// About 40% faster than <see cref="System.Collections.Generic.IEnumerable{T}" />
    /// for collections with just several items.
    /// </remarks>
    public delegate void SequenceVisitor<TSequence, TValue>(
        TSequence sequence,
        Action<TValue> visit,
        ICancellation cancellation = null);
}
