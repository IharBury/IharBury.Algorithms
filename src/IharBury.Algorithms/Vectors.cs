using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class Vectors
    {
        public static bool IsSubvectorAt<T>(
            long vectorLength,
            Func<long, T> getVectorItem,
            long subvectorLength,
            Func<long, T> getSubvectorItem,
            long subvectorStartIndex,
            IEqualityComparer<T> equalityComparer = null)
        {
            if (vectorLength < 0)
                throw new ArgumentOutOfRangeException(nameof(vectorLength));
            if (getVectorItem == null)
                throw new ArgumentNullException(nameof(getVectorItem));
            if (subvectorLength < 0)
                throw new ArgumentOutOfRangeException(nameof(vectorLength));
            if (getSubvectorItem == null)
                throw new ArgumentNullException(nameof(getSubvectorItem));
            if ((subvectorStartIndex < 0) || (subvectorStartIndex >= vectorLength))
                throw new ArgumentOutOfRangeException(nameof(subvectorStartIndex));

            if (checked(vectorLength - subvectorLength) < subvectorStartIndex)
                return false;

            var effectiveEqualityComparer = equalityComparer ?? EqualityComparer<T>.Default;
            var subvectorIndex = 0L;
            var vectorIndex = subvectorStartIndex;
            while (subvectorIndex < subvectorLength)
            {
                if (!effectiveEqualityComparer.Equals(getVectorItem(vectorIndex), getSubvectorItem(subvectorIndex)))
                    return false;

                vectorIndex++;
                subvectorIndex++;
            }

            return true;
        }

        public static long GetSubvectorOccurenceCountIncludingIntersections<T>(
            long vectorLength,
            Func<long, T> getVectorItem,
            long subvectorLength,
            Func<long, T> getSubvectorItem,
            IEqualityComparer<T> equalityComparer = null)
        {
            if (vectorLength < 0)
                throw new ArgumentOutOfRangeException(nameof(vectorLength));
            if (getVectorItem == null)
                throw new ArgumentNullException(nameof(getVectorItem));
            if (subvectorLength < 0)
                throw new ArgumentOutOfRangeException(nameof(vectorLength));
            if (getSubvectorItem == null)
                throw new ArgumentNullException(nameof(getSubvectorItem));

            var endOfPossibleIndexes = checked(vectorLength - subvectorLength).Max(0);
            var count = 0L;
            for (var vectorIndex = 0L; vectorIndex < endOfPossibleIndexes; vectorIndex++)
                if (IsSubvectorAt(
                        vectorLength,
                        getVectorItem,
                        subvectorLength,
                        getSubvectorItem,
                        vectorIndex,
                        equalityComparer))
                    count++;
            return count;
        }
    }
}
