using System.Collections.Generic;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class OutTreeTests
    {
        [Fact]
        public void AnyPathAwayFromRootCanBeFound()
        {
            var adjuncedNodesAwayFromRoot = new List<int[]>
            {
                new int[] { 1, 2 },
                new int[] { },
                new int[] { 3, 4 },
                new int[] { },
                new int[] { 5 },
                new int[] { }
            };

            var path = OutTrees.FindAnyPathAwayFromRoot(
                0,
                node => node == 3,
                (node, visit, cancellation) =>
                {
                    foreach (var adjuncedNode in adjuncedNodesAwayFromRoot[node])
                    {
                        if (cancellation.IsRequested)
                            break;

                        visit(adjuncedNode);
                    }
                });

            Assert.Equal<int>(new[] { 0, 2, 3 }, path);
        }
    }
}
