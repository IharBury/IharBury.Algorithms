using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class BipartiteGraphs
    {
        public static ICollection<TEdge> GetMaxCardinalityMatching<TNode, TEdge>(
            IEnumerable<TNode> firstNodeSet,
            IEnumerable<TNode> secondNodeSet,
            SequenceVisitor<TNode, TEdge> nodeEdgeVisitor,
            Graphs.GetEdgeOtherNode<TNode, TEdge> getEdgeOtherNode,
            IEqualityComparer<TNode> nodeEqualityComparer = null,
            IEqualityComparer<TEdge> edgeEqualityComparer = null,
            Func<ICollection<TNode>> nodeSetFactory = null,
            Func<ICollection<TEdge>> edgeSetFactory = null)
        {
            if (firstNodeSet == null)
                throw new ArgumentNullException(nameof(firstNodeSet));
            if (secondNodeSet == null)
                throw new ArgumentNullException(nameof(secondNodeSet));
            if (nodeEdgeVisitor == null)
                throw new ArgumentNullException(nameof(nodeEdgeVisitor));
            if (getEdgeOtherNode == null)
                throw new ArgumentNullException(nameof(getEdgeOtherNode));

            if (nodeEqualityComparer == null)
                nodeEqualityComparer = C5.EqualityComparer<TNode>.Default;
            if (edgeEqualityComparer == null)
                edgeEqualityComparer = C5.EqualityComparer<TEdge>.Default;
            if (nodeSetFactory == null)
                nodeSetFactory = () => new C5.HashSet<TNode>(nodeEqualityComparer);
            if (edgeSetFactory == null)
                edgeSetFactory = () => new C5.HashSet<TEdge>(edgeEqualityComparer);

            var matchingEdges = edgeSetFactory();

            var freeFirstSetNodes = nodeSetFactory();
            foreach (var node in firstNodeSet)
                freeFirstSetNodes.Add(node);

            var freeSecondSetNodes = nodeSetFactory();
            foreach (var node in secondNodeSet)
                freeSecondSetNodes.Add(node);

            var layers = new List<ICollection<TNode>>();
            var reachedFreeSecondSetNodes = nodeSetFactory();
            var reverseAugmentingPaths = new List<IReadOnlyList<NodeWithEdgeAndLayerIndex<TNode, TEdge>>>();

            while (true)
            {
                reachedFreeSecondSetNodes.Clear();
                int lastLayerIndex;
                if (!TryBuildLayersForAugmentingPathsOfMinLength(
                        nodeEdgeVisitor,
                        getEdgeOtherNode,
                        nodeSetFactory,
                        freeFirstSetNodes,
                        freeSecondSetNodes,
                        matchingEdges,
                        layers,
                        reachedFreeSecondSetNodes,
                        out lastLayerIndex))
                    break;

                reverseAugmentingPaths.Clear();
                FindMaxSetOfNodeDisjointAugmentingPathsOfMinLength(
                    nodeEdgeVisitor,
                    getEdgeOtherNode,
                    layers,
                    reachedFreeSecondSetNodes,
                    lastLayerIndex,
                    reverseAugmentingPaths);

                if (reverseAugmentingPaths.Count == 0)
                    break;

                EnlargeMatchingEdgesWithAugmentingPaths(
                    matchingEdges,
                    freeFirstSetNodes,
                    freeSecondSetNodes,
                    reverseAugmentingPaths,
                    lastLayerIndex);
            }

            return matchingEdges;
        }

        private static bool TryBuildLayersForAugmentingPathsOfMinLength<TNode, TEdge>(
            SequenceVisitor<TNode, TEdge> nodeEdgeVisitor,
            Graphs.GetEdgeOtherNode<TNode, TEdge> getEdgeOtherNode,
            Func<ICollection<TNode>> nodeSetFactory,
            ICollection<TNode> freeFirstSetNodes,
            ICollection<TNode> freeSecondSetNodes,
            ICollection<TEdge> matchingEdges,
            List<ICollection<TNode>> resultingLayers,
            ICollection<TNode> resultingReachedFreeSecondSetNodes,
            out int lastLayerIndex)
        {
            var layerIndex = 0;
            var firstLayer = EnsureEmptyNodeSetAt(layerIndex, resultingLayers, nodeSetFactory);
            foreach (var node in freeFirstSetNodes)
                firstLayer.Add(node);

            while (true)
            {
                layerIndex++;
                if (!TryAddSecondSetNodeLayerViaUnmatchedEdges(
                    nodeEdgeVisitor,
                    getEdgeOtherNode,
                    nodeSetFactory,
                    freeSecondSetNodes,
                    matchingEdges,
                    resultingLayers,
                    resultingReachedFreeSecondSetNodes,
                    layerIndex))
                {
                    lastLayerIndex = default(int);
                    return false;
                }

                if (resultingReachedFreeSecondSetNodes.Count > 0)
                    break;

                layerIndex++;
                if (!TryAddFirstSetNodeLayerViaMatchedEdges(
                    nodeEdgeVisitor,
                    getEdgeOtherNode,
                    nodeSetFactory,
                    matchingEdges,
                    resultingLayers,
                    layerIndex))
                {
                    lastLayerIndex = default(int);
                    return false;
                }
            }

            lastLayerIndex = layerIndex;
            return true;
        }

        private static bool TryAddFirstSetNodeLayerViaMatchedEdges<TNode, TEdge>(
            SequenceVisitor<TNode, TEdge> nodeEdgeVisitor,
            Graphs.GetEdgeOtherNode<TNode, TEdge> getEdgeOtherNode,
            Func<ICollection<TNode>> nodeSetFactory,
            ICollection<TEdge> matchingEdges,
            List<ICollection<TNode>> layers,
            int layerIndex)
        {
            var currentLayer = EnsureEmptyNodeSetAt(layerIndex, layers, nodeSetFactory);
            foreach (var node in layers[layerIndex - 1])
                nodeEdgeVisitor(
                    node,
                    edge =>
                    {
                        if (edge == null)
                            throw new ArgumentNullException(nameof(edge));

                        if (matchingEdges.Contains(edge))
                        {
                            var adjuncedNode = getEdgeOtherNode(edge, node);
                            currentLayer.Add(adjuncedNode);
                        }
                    },
                    Cancellation.Null);
            return currentLayer.Count > 0;
        }

        private static bool TryAddSecondSetNodeLayerViaUnmatchedEdges<TNode, TEdge>(
            SequenceVisitor<TNode, TEdge> nodeEdgeVisitor,
            Graphs.GetEdgeOtherNode<TNode, TEdge> getEdgeOtherNode,
            Func<ICollection<TNode>> nodeSetFactory,
            ICollection<TNode> freeSecondSetNodes,
            ICollection<TEdge> matchingEdges,
            List<ICollection<TNode>> layers,
            ICollection<TNode> resultingReachedFreeSecondSetNodes,
            int layerIndex)
        {
            var currentLayer = EnsureEmptyNodeSetAt(layerIndex, layers, nodeSetFactory);
            foreach (var node in layers[layerIndex - 1])
                nodeEdgeVisitor(
                    node,
                    edge =>
                    {
                        if (edge == null)
                            throw new ArgumentNullException(nameof(edge));

                        if (!matchingEdges.Contains(edge))
                        {
                            var adjuncedNode = getEdgeOtherNode(edge, node);
                            currentLayer.Add(adjuncedNode);
                            if (freeSecondSetNodes.Contains(adjuncedNode))
                                resultingReachedFreeSecondSetNodes.Add(adjuncedNode);
                        }
                    },
                    Cancellation.Null);
            return currentLayer.Count > 0;
        }

        private static void FindMaxSetOfNodeDisjointAugmentingPathsOfMinLength<TNode, TEdge>(
            SequenceVisitor<TNode, TEdge> nodeEdgeVisitor,
            Graphs.GetEdgeOtherNode<TNode, TEdge> getEdgeOtherNode,
            List<ICollection<TNode>> layers,
            ICollection<TNode> reachedFreeSecondSetNodes,
            int lastLayerIndex,
            List<IReadOnlyList<NodeWithEdgeAndLayerIndex<TNode, TEdge>>> resultingReverseAugmentingPaths)
        {
            foreach (var reachedFreeSecondSetNode in reachedFreeSecondSetNodes)
            {
                var reverseAugmentingPath = OutTrees.FindAnyPathAwayFromRoot(
                    new NodeWithEdgeAndLayerIndex<TNode, TEdge>(reachedFreeSecondSetNode, default(TEdge), lastLayerIndex),
                    nodeWithEdgeAndLayerIndex => nodeWithEdgeAndLayerIndex.LayerIndex == 0,
                    (nodeWithEdgeAndLayerIndex, visit, cancellation) =>
                    {
                        var layerIndex = nodeWithEdgeAndLayerIndex.LayerIndex;
                        if (layerIndex == 0)
                            return;

                        nodeEdgeVisitor(
                            nodeWithEdgeAndLayerIndex.Node,
                            edge =>
                            {
                                if (edge == null)
                                    throw new ArgumentNullException(nameof(edge));

                                var adjuncedNode = getEdgeOtherNode(edge, nodeWithEdgeAndLayerIndex.Node);
                                if (layers[layerIndex - 1].Contains(adjuncedNode))
                                    visit(new NodeWithEdgeAndLayerIndex<TNode, TEdge>(adjuncedNode, edge, layerIndex - 1));
                            },
                            cancellation);
                    });

                if (reverseAugmentingPath != null)
                {
                    foreach (var nodeWithEdgeAndLayerIndex in reverseAugmentingPath)
                    {
                        layers[nodeWithEdgeAndLayerIndex.LayerIndex].Remove(nodeWithEdgeAndLayerIndex.Node);
                    }

                    resultingReverseAugmentingPaths.Add(reverseAugmentingPath);
                }
            }
        }

        private static void EnlargeMatchingEdgesWithAugmentingPaths<TNode, TEdge>(
            ICollection<TEdge> matchingEdges,
            ICollection<TNode> freeFirstSetNodes,
            ICollection<TNode> freeSecondSetNodes,
            List<IReadOnlyList<NodeWithEdgeAndLayerIndex<TNode, TEdge>>> reverseAugmentingPaths,
            int lastLayerIndex)
        {
            foreach (var reverseAugmentingPath in reverseAugmentingPaths)
            {
                foreach (var nodeWithEdgeAndLayerIndex in reverseAugmentingPath)
                {
                    if (nodeWithEdgeAndLayerIndex.LayerIndex != lastLayerIndex)
                    {
                        var edge = nodeWithEdgeAndLayerIndex.Edge;
                        if (matchingEdges.Contains(edge))
                            matchingEdges.Remove(edge);
                        else
                            matchingEdges.Add(edge);
                    }
                }

                freeFirstSetNodes.Remove(reverseAugmentingPath[reverseAugmentingPath.Count - 1].Node);
                freeSecondSetNodes.Remove(reverseAugmentingPath[0].Node);
            }
        }

        private static ICollection<TNode> EnsureEmptyNodeSetAt<TNode>(
            int index, 
            IList<ICollection<TNode>> nodeSets, 
            Func<ICollection<TNode>> nodeSetFactory)
        {
            while (nodeSets.Count <= index)
            {
                nodeSets.Add(nodeSetFactory());
            }

            nodeSets[index].Clear();
            return nodeSets[index];
        }

        private struct NodeWithLayerIndex<TNode>
        {
            public NodeWithLayerIndex(TNode node, int layerIndex)
            {
                Node = node;
                LayerIndex = layerIndex;
            }

            public TNode Node { get; }
            public int LayerIndex { get; }
        }

        private struct NodeWithEdgeAndLayerIndex<TNode, TEdge>
        {
            public NodeWithEdgeAndLayerIndex(TNode node, TEdge edge, int layerIndex)
            {
                Node = node;
                Edge = edge;
                LayerIndex = layerIndex;
            }

            public TNode Node { get; }
            public TEdge Edge { get; }
            public int LayerIndex { get; }
        }
    }
}
