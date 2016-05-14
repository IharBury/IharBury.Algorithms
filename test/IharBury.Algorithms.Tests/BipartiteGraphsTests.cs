using System;
using System.Collections.Generic;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class BipartiteGraphsTests
    {
        public class WhenGettingMaxCardinalityMatching
        {
            [Fact]
            public void OnePassTwoLayerAlgorithmReturnsCorrectResult()
            {
                var edges = new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 3),
                    Tuple.Create(2, 3),
                    Tuple.Create(2, 4)
                };
                var firstNodeSet = new[] { 1, 2 };
                var secondNodeSet = new[] { 3, 4 };

                var matching = BipartiteGraphs.GetMaxCardinalityMatching<int, Tuple<int, int>>(
                    firstNodeSet,
                    secondNodeSet,
                    (node, action, cancellation) =>
                    {
                        foreach (var edge in edges)
                        {
                            if (cancellation.IsRequested)
                                break;
                            if ((edge.Item1 == node) || (edge.Item2 == node))
                                action(edge);
                        }
                    },
                    (edge, node) => edge.Item1 == node ? edge.Item2 : edge.Item1);

                Assert.NotNull(matching);
                Assert.Equal(2, matching.Count);
                Assert.Contains(Tuple.Create(1, 3), matching);
                Assert.Contains(Tuple.Create(2, 4), matching);
            }

            [Fact]
            public void TwoPassFourLayerAlgorithmReturnsCorrectResult()
            {
                var edges = new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 3),
                    Tuple.Create(1, 4),
                    Tuple.Create(2, 3)
                };
                var firstNodeSet = new[] { 1, 2 };
                var secondNodeSet = new[] { 3, 4 };

                var matching = BipartiteGraphs.GetMaxCardinalityMatching<int, Tuple<int, int>>(
                    firstNodeSet,
                    secondNodeSet,
                    (node, action, cancellation) =>
                    {
                        foreach (var edge in edges)
                        {
                            if (cancellation.IsRequested)
                                break;
                            if ((edge.Item1 == node) || (edge.Item2 == node))
                                action(edge);
                        }
                    },
                    (edge, node) => edge.Item1 == node ? edge.Item2 : edge.Item1);

                Assert.NotNull(matching);
                Assert.Equal(2, matching.Count);
                Assert.Contains(Tuple.Create(1, 4), matching);
                Assert.Contains(Tuple.Create(2, 3), matching);
            }

            [Fact]
            public void NodeCannotGetIntoMultipleLayers()
            {
                var edges = new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 10),
                    Tuple.Create(2, 11),
                    Tuple.Create(3, 11),
                    Tuple.Create(4, 12),
                    Tuple.Create(5, 12),
                    Tuple.Create(6, 12),
                    Tuple.Create(1, 13),
                    Tuple.Create(6, 14),
                    Tuple.Create(7, 14),
                    Tuple.Create(3, 14),
                    Tuple.Create(8, 15),
                    Tuple.Create(7, 16),
                    Tuple.Create(3, 16),
                    Tuple.Create(9, 17),
                    Tuple.Create(1, 18)
                };
                var firstNodeSet = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                var secondNodeSet = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                var matching = BipartiteGraphs.GetMaxCardinalityMatching<int, Tuple<int, int>>(
                    firstNodeSet,
                    secondNodeSet,
                    (node, action, cancellation) =>
                    {
                        foreach (var edge in edges)
                        {
                            if (cancellation.IsRequested)
                                break;
                            if ((edge.Item1 == node) || (edge.Item2 == node))
                                action(edge);
                        }
                    },
                    (edge, node) => edge.Item1 == node ? edge.Item2 : edge.Item1);

                Assert.NotNull(matching);
                Assert.Equal(7, matching.Count);
            }
        }
    }
}
