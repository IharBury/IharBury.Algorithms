using System;

namespace IharBury.Algorithms
{
    public static class VisitableSequences
    {
        public static void Visit<TSequence, TValue>(
            TSequence sequence, 
            Action<TValue> visit, 
            ICancellation cancellation = null)
            where TSequence : IVisitableSequence<TValue>
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            sequence.ForEach(visit, cancellation);
        }
    }
}
