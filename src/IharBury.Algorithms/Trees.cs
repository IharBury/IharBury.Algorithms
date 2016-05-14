using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class Trees
    {
        public static IEnumerable<Graphs.NodeTraversal<TNode>> TraverseDepthFirst<TNode>(
            TNode initialNode,
            SequenceVisitor<TNode, TNode> adjuncedNodeVisitor,
            IEqualityComparer<TNode> nodeEqualityComparer = null)
        {
            if (initialNode == null)
                throw new ArgumentNullException(nameof(initialNode));
            if (adjuncedNodeVisitor == null)
                throw new ArgumentNullException(nameof(adjuncedNodeVisitor));

            if (nodeEqualityComparer == null)
                nodeEqualityComparer = C5.EqualityComparer<TNode>.Default;

            var pendingTraversals = new Stack<Graphs.NodeTraversal<TNode>>();
            adjuncedNodeVisitor(
                initialNode, 
                adjuncedNode => pendingTraversals.Push(new Graphs.NodeTraversal<TNode>(adjuncedNode, initialNode)));

            while (pendingTraversals.Count > 0)
            {
                var traversal = pendingTraversals.Pop();
                yield return traversal;

                adjuncedNodeVisitor(
                    traversal.Node,
                    adjuncedNode =>
                    {
                        if (!nodeEqualityComparer.Equals(adjuncedNode, traversal.SourceNode))
                            pendingTraversals.Push(new Graphs.NodeTraversal<TNode>(adjuncedNode, traversal.Node));
                    });
            }
        }

        public static IEnumerable<Graphs.NodeTraversalWithEdge<TNode, TEdge>> TraverseDepthFirst<TNode, TEdge>(
            TNode initialNode,
            SequenceVisitor<TNode, TEdge> nodeEdgeVisitor,
            Graphs.GetEdgeOtherNode<TNode, TEdge> getEdgeOtherNode,
            IEqualityComparer<TEdge> edgeEqualityComparer = null)
        {
            if (initialNode == null)
                throw new ArgumentNullException(nameof(initialNode));
            if (nodeEdgeVisitor == null)
                throw new ArgumentNullException(nameof(nodeEdgeVisitor));
            if (getEdgeOtherNode == null)
                throw new ArgumentNullException(nameof(getEdgeOtherNode));

            if (edgeEqualityComparer == null)
                edgeEqualityComparer = C5.EqualityComparer<TEdge>.Default;

            var pendingTraversals = new Stack<Graphs.NodeTraversalWithEdge<TNode, TEdge>>();
            nodeEdgeVisitor(
                initialNode,
                edge => pendingTraversals.Push(
                    new Graphs.NodeTraversalWithEdge<TNode, TEdge>(getEdgeOtherNode(edge, initialNode), edge)));

            while (pendingTraversals.Count > 0)
            {
                var traversal = pendingTraversals.Pop();
                yield return traversal;

                nodeEdgeVisitor(
                    traversal.Node,
                    edge =>
                    {
                        if (!edgeEqualityComparer.Equals(edge, traversal.Edge))
                            pendingTraversals.Push(
                                new Graphs.NodeTraversalWithEdge<TNode, TEdge>(getEdgeOtherNode(edge, traversal.Node), edge));
                    });
            }
        }
    }
}
