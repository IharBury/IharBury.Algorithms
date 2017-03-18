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
    }
}
