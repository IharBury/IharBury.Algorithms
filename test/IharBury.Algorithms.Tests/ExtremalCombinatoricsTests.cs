using System;
using System.Linq;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class ExtremalCombinatoricsTests
    {
        [Fact]
        public void FindsMaxSetOfUniqueCombinationsWithPairUseLimit()
        {
            var result = ExtremalCombinatorics.GetMaxSetOfUniqueCombinationsWithPairUseLimit(
                    Enumerable.Range(1, 3).ToList(),
                    Enumerable.Range(1, 1).ToList(),
                    Enumerable.Range(1, 3).ToList(),
                    2)
                .ToList();

            Assert.NotNull(result);
            Assert.Equal(6, result.Count);
            Assert.All(
                result,
                combination =>
                {
                    Assert.True(combination.Item1 >= 1);
                    Assert.True(combination.Item1 <= 3);
                    Assert.Equal(1, combination.Item2);
                    Assert.True(combination.Item3 >= 1);
                    Assert.True(combination.Item3 <= 3);
                });
            Assert.All(
                result.GroupBy(combination => combination),
                grouping =>
                {
                    Assert.Equal(1, grouping.Count());
                });
            Assert.All(
                result.GroupBy(combination => Tuple.Create(combination.Item1, combination.Item2)),
                grouping =>
                {
                    Assert.True(grouping.Count() <= 2);
                });
            Assert.All(
                result.GroupBy(combination => Tuple.Create(combination.Item1, combination.Item3)),
                grouping =>
                {
                    Assert.True(grouping.Count() <= 2);
                });
            Assert.All(
                result.GroupBy(combination => Tuple.Create(combination.Item2, combination.Item3)),
                grouping =>
                {
                    Assert.True(grouping.Count() <= 2);
                });
        }
    }
}
