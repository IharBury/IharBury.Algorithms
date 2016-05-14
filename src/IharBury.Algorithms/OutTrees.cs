using System;
using System.Collections.Generic;
using System.Linq;

namespace IharBury.Algorithms
{
    public static class OutTrees
    {
        public static IEnumerable<Graphs.NodeTraversal<TNode>> TraverseDepthFirstAwayFromRoot<TNode>(
            TNode initialNode,
            SequenceVisitor<TNode, TNode> adjuncedNodeAwayFromRootVisitor)
        {
            if (initialNode == null)
                throw new ArgumentNullException(nameof(initialNode));
            if (adjuncedNodeAwayFromRootVisitor == null)
                throw new ArgumentNullException(nameof(adjuncedNodeAwayFromRootVisitor));

            var pendingTraversals = new Stack<Graphs.NodeTraversal<TNode>>();
            adjuncedNodeAwayFromRootVisitor(
                initialNode,
                adjuncedNode => 
                {
                    if (adjuncedNode == null)
                        throw new ArgumentNullException(nameof(adjuncedNode));

                    pendingTraversals.Push(new Graphs.NodeTraversal<TNode>(adjuncedNode, initialNode));
                },
                Cancellation.Null);

            while (pendingTraversals.Count > 0)
            {
                var traversal = pendingTraversals.Pop();
                yield return traversal;

                adjuncedNodeAwayFromRootVisitor(
                    traversal.Node,
                    adjuncedNode =>
                    {
                        if (adjuncedNode == null)
                            throw new ArgumentNullException(nameof(adjuncedNode));

                        pendingTraversals.Push(new Graphs.NodeTraversal<TNode>(adjuncedNode, traversal.Node));
                    },
                    Cancellation.Null);
            }
        }

        public static void TraverseDepthFirstAwayFromRootWithState<TNode, TState>(
            TNode initialNode,
            SequenceVisitor<TNode, TNode> adjuncedNodeAwayFromRootVisitor,
            Func<Graphs.NodeTraversalWithState<TNode, TState>, TState> visit,
            ICancellation cancellation,
            TState initialState = default(TState))
        {
            if (initialNode == null)
                throw new ArgumentNullException(nameof(initialNode));
            if (adjuncedNodeAwayFromRootVisitor == null)
                throw new ArgumentNullException(nameof(adjuncedNodeAwayFromRootVisitor));
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));

            if (cancellation.IsRequested)
                return;

            var states = new List<TState> { initialState };
            var pendingNodesWithDistances = new Stack<Graphs.NodeWithDistance<TNode, int>>();
            adjuncedNodeAwayFromRootVisitor(
                initialNode,
                adjuncedNode =>
                {
                    if (adjuncedNode == null)
                        throw new ArgumentNullException(nameof(adjuncedNode));

                    pendingNodesWithDistances.Push(new Graphs.NodeWithDistance<TNode, int>(adjuncedNode, 1));
                },
                Cancellation.Null);

            while (pendingNodesWithDistances.Count > 0)
            {
                var nodeWithDistance = pendingNodesWithDistances.Pop();
                var newState = visit(new Graphs.NodeTraversalWithState<TNode, TState>(
                    nodeWithDistance.Node,
                    states[nodeWithDistance.Distance - 1]));

                if (cancellation.IsRequested)
                    return;

                if (states.Count == nodeWithDistance.Distance)
                    states.Add(newState);
                else
                    states[nodeWithDistance.Distance] = newState;

                adjuncedNodeAwayFromRootVisitor(
                    nodeWithDistance.Node,
                    adjuncedNode =>
                    {
                        if (adjuncedNode == null)
                            throw new ArgumentNullException(nameof(adjuncedNode));

                        pendingNodesWithDistances.Push(
                            new Graphs.NodeWithDistance<TNode, int>(adjuncedNode, nodeWithDistance.Distance + 1));
                    },
                    Cancellation.Null);
            }
        }

        public static IReadOnlyList<TNode> FindAnyPathAwayFromRoot<TNode>(
            TNode sourceNode,
            Func<TNode, bool> isDestinationNode,
            SequenceVisitor<TNode, TNode> adjuncedNodeAwayFromRootVisitor)
        {
            if (sourceNode == null)
                throw new ArgumentNullException(nameof(sourceNode));
            if (isDestinationNode == null)
                throw new ArgumentNullException(nameof(isDestinationNode));
            if (adjuncedNodeAwayFromRootVisitor == null)
                throw new ArgumentNullException(nameof(adjuncedNodeAwayFromRootVisitor));

            List<TNode> result = null;
            var nodesInPath = new List<TNode> { sourceNode };
            var cancellation = new Cancellation();
            TraverseDepthFirstAwayFromRootWithState(
                sourceNode,
                adjuncedNodeAwayFromRootVisitor,
                traversal =>
                {
                    var distance = traversal.PreviousState + 1;
                    if (nodesInPath.Count == distance)
                        nodesInPath.Add(traversal.Node);
                    else
                        nodesInPath[distance] = traversal.Node;

                    if (isDestinationNode(traversal.Node))
                    {
                        result = nodesInPath.Take(distance + 1).ToList();
                        cancellation.Cancel();
                    }

                    return distance;
                },
                cancellation,
                0);

            return result;
        }
    }
}
