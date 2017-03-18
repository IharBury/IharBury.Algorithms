using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public sealed class EnumerableExtensionsTests
    {
        public sealed class WhenGroupingWhileAConditionIsHeld
        {
            [Fact]
            public void WhenThereAreNoItemsThereAreNoGroups()
            {
                Assert.Empty(Enumerable.Empty<int>().GroupWhile(
                    item => new List<int>() { item },
                    (group, item) =>
                    {
                        group.Add(item);
                        return group;
                    },
                    (group, item) => true));
            }

            [Fact]
            public void WhenThereIsOnlyOneItemItFormsTheOnlyGroup()
            {
                var groups = new[] { 1 }
                    .GroupWhile(
                        item => new List<int>() { item },
                        (group, item) =>
                        {
                            group.Add(item);
                            return group;
                        },
                        (group, item) => true)
                    .ToList();
                Assert.Equal(1, groups.Count);
                Assert.True(groups.Single().SequenceEqual(new[] { 1 }));
            }

            [Fact]
            public void WhenItemsShouldBeInTheSameGroupTheyAre()
            {
                var groups = new[] { 1, 2, 3 }
                    .GroupWhile(
                        item => new List<int>() { item },
                        (group, item) =>
                        {
                            group.Add(item);
                            return group;
                        },
                        (group, item) => true)
                    .ToList();
                Assert.Equal(1, groups.Count);
                Assert.True(groups.Single().SequenceEqual(new[] { 1, 2, 3 }));
            }

            [Fact]
            public void WhenItemsShouldBeInDifferentGroupsTheyAre()
            {
                var groups = new[] { 1, 2, 3 }
                    .GroupWhile(
                        item => new List<int>() { item },
                        (group, item) =>
                        {
                            group.Add(item);
                            return group;
                        },
                        (group, item) => false)
                    .ToList();
                Assert.Equal(3, groups.Count);
                Assert.True(groups[0].SequenceEqual(new[] { 1 }));
                Assert.True(groups[1].SequenceEqual(new[] { 2 }));
                Assert.True(groups[2].SequenceEqual(new[] { 3 }));
            }

            [Fact]
            public void WhenAnItemShouldStartANewGroupItDoes()
            {
                var groups = new[] { 1, 2, 3, 4, 5 }
                    .GroupWhile(
                        item => new List<int>() { item },
                        (group, item) =>
                        {
                            group.Add(item);
                            return group;
                        },
                        (group, item) => item != 3)
                    .ToList();
                Assert.Equal(2, groups.Count);
                Assert.True(groups[0].SequenceEqual(new[] { 1, 2 }));
                Assert.True(groups[1].SequenceEqual(new[] { 3, 4, 5 }));
            }

            [Fact]
            public void WhenAGroupShouldStopGrowingItDoes()
            {
                var groups = new[] { 1, 2, 3, 4, 5 }
                    .GroupWhile(
                        item => new List<int>() { item },
                        (group, item) =>
                        {
                            group.Add(item);
                            return group;
                        },
                        (group, item) => group.Count < 3)
                    .ToList();
                Assert.Equal(2, groups.Count);
                Assert.True(groups[0].SequenceEqual(new[] { 1, 2, 3 }));
                Assert.True(groups[1].SequenceEqual(new[] { 4, 5 }));
            }

            [Fact]
            public void WhenAddingAnItemToAGroupProducesANewObjectItWorks()
            {
                var groups = new[] { 1, 2, 3 }
                    .GroupWhile(
                        item => new List<int>() { item },
                        (group, item) =>
                        {
                            var newGroup = group.ToList();
                            newGroup.Add(item);
                            return newGroup;
                        },
                        (group, item) => true)
                    .ToList();
                Assert.Equal(1, groups.Count);
                Assert.True(groups.Single().SequenceEqual(new[] { 1, 2, 3 }));
            }
        }

        public sealed class WhenGettingRunningSumWithValueSelector
        {
            [Fact]
            public void WhenSumsFitToInt32ItWorksForInt32Unchecked()
            {
                var items = new[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumUnchecked(item => item, 2).ToList();
                Assert.True(runningSum.SequenceEqual(new[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsFitToInt32ItWorksForInt32Checked()
            {
                var items = new[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumChecked(item => item, 2).ToList();
                Assert.True(runningSum.SequenceEqual(new[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt32TheyRoundTripForInt32Unchecked()
            {
                var items = new[] { 1, int.MaxValue, -1 };
                var runningSum = items.GetRunningSumUnchecked(item => item).ToList();
                Assert.True(runningSum.SequenceEqual(new[] { 1, int.MinValue, int.MaxValue }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt32ItThrowsForInt32Checked()
            {
                var items = new[] { 1, int.MaxValue };
                Assert.Throws<OverflowException>(() => items.GetRunningSumChecked(item => item).ToList());
            }

            [Fact]
            public void WhenSumsFitToInt64ItWorksForInt64Unchecked()
            {
                var items = new long[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumUnchecked(item => item, 2).ToList();
                Assert.True(runningSum.SequenceEqual(new long[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsFitToInt64ItWorksForInt64Checked()
            {
                var items = new long[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumChecked(item => item, 2).ToList();
                Assert.True(runningSum.SequenceEqual(new long[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt64TheyRoundTripForInt64Unchecked()
            {
                var items = new[] { 1, long.MaxValue, -1 };
                var runningSum = items.GetRunningSumUnchecked(item => item).ToList();
                Assert.True(runningSum.SequenceEqual(new long[] { 1, long.MinValue, long.MaxValue }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt64ItThrowsForInt64Checked()
            {
                var items = new long[] { 1, long.MaxValue };
                Assert.Throws<OverflowException>(() => items.GetRunningSumChecked(item => item).ToList());
            }
        }

        public sealed class WhenGettingRunningSumWithoutValueSelector
        {
            [Fact]
            public void WhenSumsFitToInt32ItWorksForInt32Unchecked()
            {
                var items = new[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumUnchecked(2).ToList();
                Assert.True(runningSum.SequenceEqual(new[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsFitToInt32ItWorksForInt32Checked()
            {
                var items = new[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumChecked(2).ToList();
                Assert.True(runningSum.SequenceEqual(new[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt32TheyRoundTripForInt32Unchecked()
            {
                var items = new[] { 1, int.MaxValue, -1 };
                var runningSum = items.GetRunningSumUnchecked().ToList();
                Assert.True(runningSum.SequenceEqual(new[] { 1, int.MinValue, int.MaxValue }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt32ItThrowsForInt32Checked()
            {
                var items = new[] { 1, int.MaxValue };
                Assert.Throws<OverflowException>(() => items.GetRunningSumChecked().ToList());
            }

            [Fact]
            public void WhenSumsFitToInt64ItWorksForInt64Unchecked()
            {
                var items = new long[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumUnchecked(2).ToList();
                Assert.True(runningSum.SequenceEqual(new long[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsFitToInt64ItWorksForInt64Checked()
            {
                var items = new long[] { 1, 2, 4, 12, 1, -5, 4 };
                var runningSum = items.GetRunningSumChecked(2).ToList();
                Assert.True(runningSum.SequenceEqual(new long[] { 3, 5, 9, 21, 22, 17, 21 }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt64TheyRoundTripForInt64Unchecked()
            {
                var items = new[] { 1, long.MaxValue, -1 };
                var runningSum = items.GetRunningSumUnchecked().ToList();
                Assert.True(runningSum.SequenceEqual(new long[] { 1, long.MinValue, long.MaxValue }));
            }

            [Fact]
            public void WhenSumsDoNotFitToInt64ItThrowsForInt64Checked()
            {
                var items = new long[] { 1, long.MaxValue };
                Assert.Throws<OverflowException>(() => items.GetRunningSumChecked().ToList());
            }
        }
    }
}
