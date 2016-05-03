using C5;
using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class Graphs
    {
        public static void TraverseInDistanceOrder<TNode>(
            TNode initialNode,
            AdjuncedNodeVisitorWithDistance<TNode, long> adjuncedNodeVisitor,
            Action<NodeTraversalWithDistance<TNode, long>, Cancellation> nodeTraversalAction,
            IEqualityComparer<TNode> nodeEqualityComparer = null,
            System.Collections.Generic.ICollection<TNode> visitedNodes = null)
        {
            TraverseInDistanceOrder(
                initialNode,
                adjuncedNodeVisitor,
                nodeTraversalAction,
                Comparer<long>.Default,
                (distance1, distance2) => checked(distance1 + distance2),
                nodeEqualityComparer,
                visitedNodes);
        }

        public static void TraverseInDistanceOrder<TNode, TDistance>(
            TNode initialNode,
            AdjuncedNodeVisitorWithDistance<TNode, TDistance> adjuncedNodeVisitor,
            Action<NodeTraversalWithDistance<TNode, TDistance>, Cancellation> nodeTraversalAction,
            IComparer<TDistance> distanceComparer,
            Func<TDistance, TDistance, TDistance> distanceAddition,
            IEqualityComparer<TNode> nodeEqualityComparer = null,
            System.Collections.Generic.ICollection<TNode> visitedNodes = null)
        {
            if (initialNode == null)
                throw new ArgumentNullException(nameof(initialNode));
            if (adjuncedNodeVisitor == null)
                throw new ArgumentNullException(nameof(adjuncedNodeVisitor));
            if (nodeTraversalAction == null)
                throw new ArgumentNullException(nameof(nodeTraversalAction));
            if (distanceComparer == null)
                throw new ArgumentNullException(nameof(distanceComparer));
            if (distanceAddition == null)
                throw new ArgumentNullException(nameof(distanceAddition));
            if (nodeEqualityComparer == null)
                nodeEqualityComparer = C5.EqualityComparer<TNode>.Default;
            if (visitedNodes == null)
                visitedNodes = new C5.HashSet<TNode>(nodeEqualityComparer);
            if (visitedNodes.Count != 0)
                throw new ArgumentException($"{nameof(visitedNodes)}.{nameof(visitedNodes.Count)} != 0", nameof(visitedNodes));

            var queuedNodes = new IntervalHeap<NodeTraversalWithDistance<TNode, TDistance>>(
                ComparerFactory<NodeTraversalWithDistance<TNode, TDistance>>.CreateComparer(
                    (nodeTraversal1, nodeTraversal2) => distanceComparer.Compare(
                        nodeTraversal1.DistanceFromInitialNode, 
                        nodeTraversal2.DistanceFromInitialNode)));
            var cancellation = new Cancellation();

            visitedNodes.Add(initialNode);
            adjuncedNodeVisitor(
                initialNode,
                (adjuncedNode, distanceToAdjuncedNode) =>
                {
                    if (!visitedNodes.Contains(adjuncedNode))
                    {
                        queuedNodes.Add(new NodeTraversalWithDistance<TNode, TDistance>(
                            adjuncedNode,
                            distanceToAdjuncedNode,
                            initialNode,
                            distanceToAdjuncedNode));
                    }
                 });

            while (queuedNodes.Count != 0)
            {
                var nodeTraversal = queuedNodes.DeleteMin();
                var currentNode = nodeTraversal.Node;
                if (!visitedNodes.Contains(currentNode))
                {
                    visitedNodes.Add(currentNode);
                    nodeTraversalAction(nodeTraversal, cancellation);
                    if (cancellation.IsCancellationRequested)
                        return;

                    adjuncedNodeVisitor(
                        currentNode,
                        (adjuncedNode, distanceToAdjuncedNodeFromCurrentNode) =>
                        {
                            if (!visitedNodes.Contains(adjuncedNode))
                            {
                                var distanceToAdjuncedNodeFromInitialNode = distanceAddition(
                                    nodeTraversal.DistanceFromInitialNode, 
                                    distanceToAdjuncedNodeFromCurrentNode);
                                queuedNodes.Add(new NodeTraversalWithDistance<TNode, TDistance>(
                                    adjuncedNode,
                                    distanceToAdjuncedNodeFromInitialNode,
                                    currentNode,
                                    distanceToAdjuncedNodeFromCurrentNode));
                            }
                        });
                }
            }
        }

        public delegate void AdjuncedNodeVisitWithDistance<TNode, TDistance>(TNode adjuncedNode, TDistance distance);

        public delegate void AdjuncedNodeVisitorWithDistance<TNode, TDistance>(
            TNode node,
            AdjuncedNodeVisitWithDistance<TNode, TDistance> visit);

        public struct NodeTraversalWithDistance<TNode, TDistance>
        {
            public NodeTraversalWithDistance(
                TNode node,
                TDistance distanceFromInitialNode, 
                TNode sourceNode, 
                TDistance distanceFromSourceNode)
            {
                if (node == null)
                    throw new ArgumentNullException(nameof(node));
                if (distanceFromInitialNode == null)
                    throw new ArgumentNullException(nameof(distanceFromInitialNode));

                Node = node;
                DistanceFromInitialNode = distanceFromInitialNode;
                SourceNode = sourceNode;
                DistanceFromSourceNode = distanceFromSourceNode;
            }

            public TNode Node { get; }
            public TDistance DistanceFromInitialNode { get; }
            public TNode SourceNode { get; }
            public TDistance DistanceFromSourceNode { get; }

            public override string ToString() => 
                $"{DistanceFromInitialNode} from source to {Node}, {DistanceFromSourceNode} from {SourceNode}";
        }
    }
}
