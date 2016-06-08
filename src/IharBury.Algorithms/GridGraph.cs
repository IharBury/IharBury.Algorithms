using System;
using System.Collections;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class GridGraph
    {
        public static SequenceVisitor<Node, Graphs.NodeWithDistance<Node, long>> GetAdjucentNodeVisitorWithDistance(
            int rowCount,
            int columnCount)
        {
            if (rowCount <= 0)
                throw new ArgumentException("Row count is not positive.", nameof(rowCount));
            if (columnCount <= 0)
                throw new ArgumentException("Column count is not positive.", nameof(columnCount));

            return (node, visit, cancellation) =>
            {
                if (visit == null)
                    throw new ArgumentNullException(nameof(visit));

                if (cancellation == null)
                    cancellation = Cancellation.Null;

                if ((node.Row > 0) && !cancellation.IsRequested)
                    visit(new Graphs.NodeWithDistance<Node, long>(new Node(node.Row - 1, node.Column), 1));
                if ((node.Row < rowCount - 1) && !cancellation.IsRequested)
                    visit(new Graphs.NodeWithDistance<Node, long>(new Node(node.Row + 1, node.Column), 1));
                if ((node.Column > 0) && !cancellation.IsRequested)
                    visit(new Graphs.NodeWithDistance<Node, long>(new Node(node.Row, node.Column - 1), 1));
                if ((node.Column < columnCount - 1) && !cancellation.IsRequested)
                    visit(new Graphs.NodeWithDistance<Node, long>(new Node(node.Row, node.Column + 1), 1));
            };
        }

        public static SequenceVisitor<Node, Node> GetAdjucentNodeVisitor(int rowCount, int columnCount)
        {
            if (rowCount <= 0)
                throw new ArgumentException("Row count is not positive.", nameof(rowCount));
            if (columnCount <= 0)
                throw new ArgumentException("Column count is not positive.", nameof(columnCount));

            return (node, visit, cancellation) =>
            {
                if (visit == null)
                    throw new ArgumentNullException(nameof(visit));

                if (cancellation == null)
                    cancellation = Cancellation.Null;

                if ((node.Row > 0) && !cancellation.IsRequested)
                    visit(new Node(node.Row - 1, node.Column));
                if ((node.Row < rowCount - 1) && !cancellation.IsRequested)
                    visit(new Node(node.Row + 1, node.Column));
                if ((node.Column > 0) && !cancellation.IsRequested)
                    visit(new Node(node.Row, node.Column - 1));
                if ((node.Column < columnCount - 1) && !cancellation.IsRequested)
                    visit(new Node(node.Row, node.Column + 1));
            };
        }

        public struct Node : IEquatable<Node>
        {
            public static bool operator ==(Node node1, Node node2)
            {
                return node1.Equals(node2);
            }

            public static bool operator !=(Node node1, Node node2)
            {
                return !(node1 == node2);
            }

            public Node(int row, int column)
            {
                if (row < 0)
                    throw new ArgumentException("Row is negative.", nameof(row));
                if (column < 0)
                    throw new ArgumentException("Column is negative.", nameof(column));

                Row = row;
                Column = column;
            }

            public int Row { get; }
            public int Column { get; }

            public bool Equals(Node other) => (Row == other.Row) && (Column == other.Column);

            public override bool Equals(object obj) => (obj is Node) && Equals((Node)obj);

            public override int GetHashCode() => (Row * 37987) ^ Column;

            public override string ToString() => $"({Row}, {Column})";
        }

        public sealed class NodeSet : ICollection<Node>
        {
            private readonly BitArray nodes;

            public NodeSet(int rowCount, int columnCount)
            {
                if (rowCount <= 0)
                    throw new ArgumentException("Row count is not positive.", nameof(rowCount));
                if (columnCount <= 0)
                    throw new ArgumentException("Column count is not positive.", nameof(columnCount));

                RowCount = rowCount;
                ColumnCount = columnCount;
                nodes = new BitArray(rowCount * columnCount);
            }

            public int RowCount { get; }
            public int ColumnCount { get; }
            public int Count { get; private set; }
            public bool IsReadOnly => false;

            public void Add(Node item)
            {
                var nodeIndex = GetNodeIndex(item, nameof(item));
                if (!nodes[nodeIndex])
                {
                    nodes[nodeIndex] = true;
                    Count++;
                }
            }

            public void Clear()
            {
                if (Count != 0)
                {
                    nodes.SetAll(false);
                    Count = 0;
                }
            }

            public bool Contains(Node item)
            {
                return nodes[GetNodeIndex(item, nameof(item))];
            }

            public void CopyTo(Node[] array, int arrayIndex)
            {
                checked
                {
                    var currentArrayIndex = arrayIndex;
                    for (var currentNodeIndex = 0; currentNodeIndex < nodes.Count; currentNodeIndex++)
                    {
                        if (nodes[currentNodeIndex])
                        {
                            array[currentArrayIndex] = GetNodeAt(currentNodeIndex);
                            currentArrayIndex++;
                        }
                    }
                }
            }

            public IEnumerator<Node> GetEnumerator()
            {
                for (var currentNodeIndex = 0; currentNodeIndex < nodes.Count; currentNodeIndex++)
                    if (nodes[currentNodeIndex])
                        yield return GetNodeAt(currentNodeIndex);
            }

            public bool Remove(Node item)
            {
                var nodeIndex = GetNodeIndex(item, nameof(item));
                if (nodes[nodeIndex])
                {
                    nodes[nodeIndex] = false;
                    Count--;
                    return true;
                }

                return false;
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private int GetNodeIndex(Node node, string nodeName)
            {
                if (node.Row < 0)
                    throw new ArgumentException("Row is negative.", nodeName);
                if (node.Row >= RowCount)
                    throw new ArgumentException("Row is larger than the grid.", nodeName);
                if (node.Column < 0)
                    throw new ArgumentException("Column is negative.", nodeName);
                if (node.Column >= ColumnCount)
                    throw new ArgumentException("Column is larger than the grid.", nodeName);

                return node.Row * ColumnCount + node.Column;
            }

            private Node GetNodeAt(int nodeIndex)
            {
                return new Node(nodeIndex / ColumnCount, nodeIndex % ColumnCount);
            }
        }
    }
}
