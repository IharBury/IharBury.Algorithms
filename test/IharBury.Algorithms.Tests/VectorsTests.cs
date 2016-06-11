using System.Linq;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class VectorsTests
    {
        [Fact]
        public void CanFindNonIntersectingSubvectors()
        {
            var vector = "TGCGTTTGCGT";
            var subvector = "GCG";
            var subvectorIndexes = Vectors
                .GetSubvectorOccurencesIncludingIntersections(
                    vector.Length,
                    index => vector[(int)index],
                    subvector.Length,
                    index => subvector[(int)index])
                .ToList();
            Assert.Equal(new long[] { 1, 7 }, subvectorIndexes);
        }

        [Fact]
        public void CanFindIntersectingSubvectors()
        {
            var vector = "TGCGCGT";
            var subvector = "GCG";
            var subvectorIndexes = Vectors
                .GetSubvectorOccurencesIncludingIntersections(
                    vector.Length,
                    index => vector[(int)index],
                    subvector.Length,
                    index => subvector[(int)index])
                .ToList();
            Assert.Equal(new long[] { 1, 3 }, subvectorIndexes);
        }

        [Fact]
        public void CanFindSubvectorAtStart()
        {
            var vector = "GCGTT";
            var subvector = "GCG";
            var subvectorIndexes = Vectors
                .GetSubvectorOccurencesIncludingIntersections(
                    vector.Length,
                    index => vector[(int)index],
                    subvector.Length,
                    index => subvector[(int)index])
                .ToList();
            Assert.Equal(new long[] { 0 }, subvectorIndexes);
        }

        [Fact]
        public void CanFindSubvectorAtEnd()
        {
            var vector = "TTGCG";
            var subvector = "GCG";
            var subvectorIndexes = Vectors
                .GetSubvectorOccurencesIncludingIntersections(
                    vector.Length,
                    index => vector[(int)index],
                    subvector.Length,
                    index => subvector[(int)index])
                .ToList();
            Assert.Equal(new long[] { 2 }, subvectorIndexes);
        }
    }
}
