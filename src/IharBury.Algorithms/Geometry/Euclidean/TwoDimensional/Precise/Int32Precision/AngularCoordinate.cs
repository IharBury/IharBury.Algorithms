using System;

namespace IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Precise.Int32Precision
{
    public struct AngularCoordinate : IEquatable<AngularCoordinate>, IComparable<AngularCoordinate>
    {
        public static bool operator ==(AngularCoordinate coordinate1, AngularCoordinate coordinate2)
        {
            return coordinate1.Equals(coordinate2);
        }

        public static bool operator !=(AngularCoordinate coordinate1, AngularCoordinate coordinate2)
        {
            return !(coordinate1 == coordinate2);
        }

        public static bool operator <(AngularCoordinate coordinate1, AngularCoordinate coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) < 0;
        }

        public static bool operator <=(AngularCoordinate coordinate1, AngularCoordinate coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) <= 0;
        }

        public static bool operator >(AngularCoordinate coordinate1, AngularCoordinate coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) > 0;
        }

        public static bool operator >=(AngularCoordinate coordinate1, AngularCoordinate coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) >= 0;
        }

        public static AngularCoordinate Zero => new AngularCoordinate(1, 0);
        public static AngularCoordinate Quarter => new AngularCoordinate(0, 1);
        public static AngularCoordinate Half => new AngularCoordinate(-1, 0);
        public static AngularCoordinate ThreeQuarters => new AngularCoordinate(0, -1);

        public AngularCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool IsZero => (Y == 0) && (X >= 0);

        public AngularCoordinate? TryGetOpposite()
        {
            checked
            {
                var normalized = Normalize();
                if ((normalized.X == int.MinValue) || (normalized.Y == int.MinValue))
                    return null;
                return new AngularCoordinate(-normalized.X, -normalized.Y);
            }
        }

        public AngularCoordinate Normalize()
        {
            checked
            {
                if (IsZero)
                    return new AngularCoordinate(1, 0);

                var gcd = X.Gcd(Y);
                return new AngularCoordinate((int)(X / gcd), (int)(Y / gcd));
            }
        }

        public bool Equals(AngularCoordinate other)
        {
            checked
            {
                if (IsZero)
                    return other.IsZero;

                return (Math.Sign(X) == Math.Sign(other.X)) &&
                    (Math.Sign(Y) == Math.Sign(other.Y)) &&
                    (X * (long)other.Y == other.X * (long)Y);
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is AngularCoordinate) && Equals((AngularCoordinate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var normalized = Normalize();
                return (normalized.X * 37987) ^ normalized.Y;
            }
        }

        public override string ToString() => $"({X},{Y})";

        public int CompareTo(AngularCoordinate other)
        {
            if (Equals(other))
                return 0;

            var orderingKind = GetOrderingKind();
            var otherOrderingKind = other.GetOrderingKind();
            if (orderingKind != otherOrderingKind)
                return orderingKind.CompareTo(otherOrderingKind);

            checked
            {

                return other.X * (long)Y > X * (long)other.Y ? 1 : -1;
            }
        }

        private OrderingKind GetOrderingKind()
        {
            switch (Math.Sign(X))
            {
                case 0:
                    switch (Math.Sign(Y))
                    {
                        case 0:
                            return OrderingKind.Zero;

                        case 1:
                            return OrderingKind.Quarter;

                        case -1:
                            return OrderingKind.ThreeQuarters;
                    }
                    break;

                case 1:
                    switch (Math.Sign(Y))
                    {
                        case 0:
                            return OrderingKind.Zero;

                        case 1:
                            return OrderingKind.InFirstQuarter;

                        case -1:
                            return OrderingKind.InFourthQuarter;
                    }
                    break;

                case -1:
                    switch (Math.Sign(Y))
                    {
                        case 0:
                            return OrderingKind.Half;

                        case 1:
                            return OrderingKind.InSecondQuarter;

                        case -1:
                            return OrderingKind.InThirdQuarter;
                    }
                    break;
            }

            throw new InvalidOperationException();
        }

        private enum OrderingKind
        {
            Zero,
            InFirstQuarter,
            Quarter,
            InSecondQuarter,
            Half,
            InThirdQuarter,
            ThreeQuarters,
            InFourthQuarter
        }
    }
}
