using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class GraphsTests
    {
        [Fact]
        public void GridGraphIsCorrectlyTraversedInDistanceOrderFromCorner()
        {
            var grid = new int[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            var traversals = Graphs
                .TraverseInDistanceOrder(
                    new GridGraph.Node(0, 0),
                    GridGraph.GetAdjucentNodeVisitorWithDistance(grid.GetLength(0), grid.GetLength(1)))
                .ToList();

            Assert.Equal(8, traversals.Count);

            var distance1Traversals = traversals.Take(2).ToList();
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(0, 1),
                1,
                new GridGraph.Node(0, 0),
                1)));
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(1, 0),
                1,
                new GridGraph.Node(0, 0),
                1)));

            var distance2Traversals = traversals.Skip(2).Take(3).ToList();
            Assert.True(distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(0, 2),
                2,
                new GridGraph.Node(0, 1),
                1)));
            Assert.True(
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 1),
                    2,
                    new GridGraph.Node(0, 1),
                    1)) ||
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 1),
                    2,
                    new GridGraph.Node(1, 0),
                    1)));
            Assert.True(distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(2, 0),
                2,
                new GridGraph.Node(1, 0),
                1)));

            var distance3Traversals = traversals.Skip(5).Take(2).ToList();
            Assert.True(
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 2),
                    3,
                    new GridGraph.Node(0, 2),
                    1)) ||
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 2),
                    3,
                    new GridGraph.Node(1, 1),
                    1)));
            Assert.True(
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 1),
                    3,
                    new GridGraph.Node(1, 1),
                    1)) ||
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 1),
                    3,
                    new GridGraph.Node(2, 0),
                    1)));

            var distance4Traversals = traversals.Skip(7).ToList();
            Assert.True(
                distance4Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 2),
                    4,
                    new GridGraph.Node(2, 1),
                    1)) ||
                distance4Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 2),
                    4,
                    new GridGraph.Node(1, 2),
                    1)));
        }

        [Fact]
        public void GridGraphIsCorrectlyTraversedInDistanceOrderFromCenter()
        {
            var grid = new int[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            var traversals = Graphs
                .TraverseInDistanceOrder(
                    new GridGraph.Node(1, 1),
                    GridGraph.GetAdjucentNodeVisitorWithDistance(grid.GetLength(0), grid.GetLength(1)))
                .ToList();

            Assert.Equal(8, traversals.Count);

            var distance1Traversals = traversals.Take(4).ToList();
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(0, 1),
                1,
                new GridGraph.Node(1, 1),
                1)));
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(1, 0),
                1,
                new GridGraph.Node(1, 1),
                1)));
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(2, 1),
                1,
                new GridGraph.Node(1, 1),
                1)));
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(1, 2),
                1,
                new GridGraph.Node(1, 1),
                1)));

            var distance2Traversals = traversals.Skip(4).ToList();
            Assert.True(
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(0, 0),
                    2,
                    new GridGraph.Node(0, 1),
                    1)) ||
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(0, 0),
                    2,
                    new GridGraph.Node(1, 0),
                    1)));
            Assert.True(
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(0, 2),
                    2,
                    new GridGraph.Node(0, 1),
                    1)) ||
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(0, 2),
                    2,
                    new GridGraph.Node(1, 2),
                    1)));
            Assert.True(
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 0),
                    2,
                    new GridGraph.Node(1, 0),
                    1)) ||
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 0),
                    2,
                    new GridGraph.Node(2, 1),
                    1)));
            Assert.True(
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 2),
                    2,
                    new GridGraph.Node(1, 2),
                    1)) ||
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 2),
                    2,
                    new GridGraph.Node(2, 1),
                    1)));
        }

        [Fact]
        public void GridGraphIsCorrectlyTraversedWithOptimizedNodeSet()
        {
            var grid = new int[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            var traversals = Graphs
                .TraverseInDistanceOrder(
                    new GridGraph.Node(0, 0),
                    GridGraph.GetAdjucentNodeVisitorWithDistance(grid.GetLength(0), grid.GetLength(1)),
                    nodeSet: new GridGraph.NodeSet(grid.GetLength(0), grid.GetLength(1)))
                .ToList();

            Assert.Equal(8, traversals.Count);

            var distance1Traversals = traversals.Take(2).ToList();
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(0, 1),
                1,
                new GridGraph.Node(0, 0),
                1)));
            Assert.True(distance1Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(1, 0),
                1,
                new GridGraph.Node(0, 0),
                1)));

            var distance2Traversals = traversals.Skip(2).Take(3).ToList();
            Assert.True(distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(0, 2),
                2,
                new GridGraph.Node(0, 1),
                1)));
            Assert.True(
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 1),
                    2,
                    new GridGraph.Node(0, 1),
                    1)) ||
                distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 1),
                    2,
                    new GridGraph.Node(1, 0),
                    1)));
            Assert.True(distance2Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                new GridGraph.Node(2, 0),
                2,
                new GridGraph.Node(1, 0),
                1)));

            var distance3Traversals = traversals.Skip(5).Take(2).ToList();
            Assert.True(
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 2),
                    3,
                    new GridGraph.Node(0, 2),
                    1)) ||
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(1, 2),
                    3,
                    new GridGraph.Node(1, 1),
                    1)));
            Assert.True(
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 1),
                    3,
                    new GridGraph.Node(1, 1),
                    1)) ||
                distance3Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 1),
                    3,
                    new GridGraph.Node(2, 0),
                    1)));

            var distance4Traversals = traversals.Skip(7).ToList();
            Assert.True(
                distance4Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 2),
                    4,
                    new GridGraph.Node(2, 1),
                    1)) ||
                distance4Traversals.Contains(new Graphs.NodeTraversalWithDistance<GridGraph.Node, long>(
                    new GridGraph.Node(2, 2),
                    4,
                    new GridGraph.Node(1, 2),
                    1)));
        }

    }
}
