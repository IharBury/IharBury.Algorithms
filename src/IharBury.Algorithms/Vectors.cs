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
            if (subvectorLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(subvectorLength));
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

        public static IEnumerable<long> GetSubvectorOccurencesIncludingIntersections<T>(
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
            if (subvectorLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(subvectorLength));
            if (getSubvectorItem == null)
                throw new ArgumentNullException(nameof(getSubvectorItem));

            if (vectorLength < subvectorLength)
                yield break;

            var effectiveEqualityComparer = equalityComparer ?? EqualityComparer<T>.Default;
            var backtrackingVector = BuildBacktrackingVectorForSubvectorSearch(
                subvectorLength,
                getSubvectorItem,
                effectiveEqualityComparer);
            var vectorIndex = 0L;
            var subvectorIndex = 0L;
            while (vectorIndex <= vectorLength - subvectorLength + subvectorIndex)
            {
                if (effectiveEqualityComparer.Equals(getVectorItem(vectorIndex), getSubvectorItem(subvectorIndex)))
                {
                    subvectorIndex++;
                    if (subvectorIndex == subvectorLength)
                    {
                        yield return vectorIndex - subvectorLength + 1;
                        subvectorIndex = backtrackingVector.GetValueOrDefault(subvectorLength);
                    }

                    vectorIndex++;
                }
                else if (subvectorIndex == 0)
                {
                    vectorIndex++;
                }
                else
                {
                    subvectorIndex = backtrackingVector.GetValueOrDefault(subvectorIndex);
                }
            }
        }

        public static Dictionary<long, long> BuildBacktrackingVectorForSubvectorSearch<T>(
            long subvectorLength,
            Func<long, T> getSubvectorItem,
            IEqualityComparer<T> equalityComparer = null)
        {
            if (subvectorLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(subvectorLength));
            if (getSubvectorItem == null)
                throw new ArgumentNullException(nameof(getSubvectorItem));

            var backtrackingVector = new Dictionary<long, long>();
            var effectiveEqualityComparer = equalityComparer ?? EqualityComparer<T>.Default;
            var currentIndex = 1L;
            var currentFallbackIndex = 0L;
            while (currentIndex < subvectorLength)
            {
                if (effectiveEqualityComparer.Equals(getSubvectorItem(currentIndex), getSubvectorItem(currentFallbackIndex)))
                {
                    currentIndex++;
                    currentFallbackIndex++;
                }
                else
                {
                    if (currentFallbackIndex == 0)
                    {
                        currentIndex++;
                    }
                    else
                    {
                        backtrackingVector.Add(currentIndex, currentFallbackIndex);
                    }
                    currentFallbackIndex = 0;
                }
            }

            if (currentFallbackIndex != 0)
                backtrackingVector.Add(subvectorLength, currentFallbackIndex);

            return backtrackingVector;
        }
    }
}
