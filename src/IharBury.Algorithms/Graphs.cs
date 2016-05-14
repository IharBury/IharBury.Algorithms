using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class Graphs
    {
        public static IEnumerable<NodeTraversalWithDistance<TNode, long>> TraverseInDistanceOrder<TNode>(
            TNode initialNode,
            SequenceVisitor<TNode, NodeWithDistance<TNode, long>> adjuncedNodeVisitor,
            IEqualityComparer<TNode> nodeEqualityComparer = null,
            ICollection<TNode> nodeSet = null)
        {
            return TraverseInDistanceOrder(
                initialNode,
                adjuncedNodeVisitor,
                Comparer<long>.Default,
                (distance1, distance2) => checked(distance1 + distance2),
                nodeEqualityComparer,
                nodeSet);
        }

        public static IEnumerable<NodeTraversalWithDistance<TNode, TDistance>> TraverseInDistanceOrder<TNode, TDistance>(
            TNode initialNode,
            SequenceVisitor<TNode, NodeWithDistance<TNode, TDistance>> adjuncedNodeVisitor,
            IComparer<TDistance> distanceComparer,
            Func<TDistance, TDistance, TDistance> distanceAddition,
            IEqualityComparer<TNode> nodeEqualityComparer = null,
            ICollection<TNode> nodeSet = null)
        {
            if (initialNode == null)
                throw new ArgumentNullException(nameof(initialNode));
            if (adjuncedNodeVisitor == null)
                throw new ArgumentNullException(nameof(adjuncedNodeVisitor));
            if (distanceComparer == null)
                throw new ArgumentNullException(nameof(distanceComparer));
            if (distanceAddition == null)
                throw new ArgumentNullException(nameof(distanceAddition));
            if (nodeEqualityComparer == null)
                nodeEqualityComparer = C5.EqualityComparer<TNode>.Default;

            var visitedNodes = nodeSet ?? new C5.HashSet<TNode>(nodeEqualityComparer);
            visitedNodes.Clear();
            var queuedNodes = new C5.IntervalHeap<NodeTraversalWithDistance<TNode, TDistance>>(
                C5.ComparerFactory<NodeTraversalWithDistance<TNode, TDistance>>.CreateComparer(
                    (nodeTraversal1, nodeTraversal2) => distanceComparer.Compare(
                        nodeTraversal1.DistanceFromInitialNode, 
                        nodeTraversal2.DistanceFromInitialNode)));

            visitedNodes.Add(initialNode);
            adjuncedNodeVisitor(
                initialNode,
                adjuncedNodeWithDistance =>
                {
                    if (!visitedNodes.Contains(adjuncedNodeWithDistance.Node))
                    {
                        queuedNodes.Add(new NodeTraversalWithDistance<TNode, TDistance>(
                            adjuncedNodeWithDistance.Node,
                            adjuncedNodeWithDistance.Distance,
                            initialNode,
                            adjuncedNodeWithDistance.Distance));
                    }
                });

            while (queuedNodes.Count != 0)
            {
                var nodeTraversal = queuedNodes.DeleteMin();
                var currentNode = nodeTraversal.Node;
                if (!visitedNodes.Contains(currentNode))
                {
                    visitedNodes.Add(currentNode);
                    yield return nodeTraversal;

                    adjuncedNodeVisitor(
                        currentNode,
                        adjuncedNodeWithDistance =>
                        {
                            if (!visitedNodes.Contains(adjuncedNodeWithDistance.Node))
                            {
                                var distanceToAdjuncedNodeFromInitialNode = distanceAddition(
                                    nodeTraversal.DistanceFromInitialNode, 
                                    adjuncedNodeWithDistance.Distance);
                                queuedNodes.Add(new NodeTraversalWithDistance<TNode, TDistance>(
                                    adjuncedNodeWithDistance.Node,
                                    distanceToAdjuncedNodeFromInitialNode,
                                    currentNode,
                                    adjuncedNodeWithDistance.Distance));
                            }
                        });
                }
            }
        }

        public delegate TNode GetEdgeOtherNode<TNode, TEdge>(TEdge edge, TNode node);

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
                if (sourceNode == null)
                    throw new ArgumentNullException(nameof(sourceNode));
                if (distanceFromSourceNode == null)
                    throw new ArgumentNullException(nameof(distanceFromSourceNode));

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

        public struct NodeWithDistance<TNode, TDistance>
        {
            public NodeWithDistance(TNode node, TDistance distance)
            {
                Node = node;
                Distance = distance;
            }

            public TNode Node { get; }
            public TDistance Distance { get; }
        }

        public struct NodeTraversal<TNode>
        {
            public NodeTraversal(TNode node, TNode sourceNode)
            {
                if (node == null)
                    throw new ArgumentNullException(nameof(node));
                if (sourceNode == null)
                    throw new ArgumentNullException(nameof(sourceNode));

                Node = node;
                SourceNode = sourceNode;
            }

            public TNode Node { get; }
            public TNode SourceNode { get; }

            public override string ToString() => $"{Node} from {SourceNode}";
        }

        public struct NodeTraversalWithEdge<TNode, TEdge>
        {
            public NodeTraversalWithEdge(TNode node, TEdge edge)
            {
                if (node == null)
                    throw new ArgumentNullException(nameof(node));
                if (edge == null)
                    throw new ArgumentNullException(nameof(edge));

                Node = node;
                Edge = edge;
            }

            public TNode Node { get; }
            public TEdge Edge { get; }

            public override string ToString() => $"{Node} from {Edge}";
        }
    }
}
