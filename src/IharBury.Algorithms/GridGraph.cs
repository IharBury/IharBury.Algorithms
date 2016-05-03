using System;

namespace IharBury.Algorithms
{
    public static class GridGraph
    {
        public static Graphs.AdjuncedNodeVisitorWithDistance<Node, long> GetAdjucentNodeVisitor(
            int rowCount, 
            int columnCount)
        {
            if (rowCount <= 0)
                throw new ArgumentException($"{nameof(rowCount)} <= 0", nameof(rowCount));
            if (columnCount <= 0)
                throw new ArgumentException($"{nameof(columnCount)} <= 0", nameof(columnCount));

            return (node, visit) =>
            {
                if (node.Row > 0)
                    visit(new Node(node.Row - 1, node.Column), 1);
                if (node.Row < rowCount - 1)
                    visit(new Node(node.Row + 1, node.Column), 1);
                if (node.Column > 0)
                    visit(new Node(node.Row, node.Column - 1), 1);
                if (node.Column < columnCount - 1)
                    visit(new Node(node.Row, node.Column + 1), 1);
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
                    throw new ArgumentException($"{nameof(row)} < 0", nameof(row));
                if (column < 0)
                    throw new ArgumentException($"{nameof(column)} < 0", nameof(column));

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
    }
}
