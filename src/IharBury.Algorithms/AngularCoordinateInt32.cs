using System;

namespace IharBury.Algorithms
{
    public struct AngularCoordinateInt32 : IEquatable<AngularCoordinateInt32>, IComparable<AngularCoordinateInt32>
    {
        public static bool operator ==(AngularCoordinateInt32 coordinate1, AngularCoordinateInt32 coordinate2)
        {
            return coordinate1.Equals(coordinate2);
        }

        public static bool operator !=(AngularCoordinateInt32 coordinate1, AngularCoordinateInt32 coordinate2)
        {
            return !(coordinate1 == coordinate2);
        }

        public static bool operator <(AngularCoordinateInt32 coordinate1, AngularCoordinateInt32 coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) < 0;
        }

        public static bool operator <=(AngularCoordinateInt32 coordinate1, AngularCoordinateInt32 coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) <= 0;
        }

        public static bool operator >(AngularCoordinateInt32 coordinate1, AngularCoordinateInt32 coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) > 0;
        }

        public static bool operator >=(AngularCoordinateInt32 coordinate1, AngularCoordinateInt32 coordinate2)
        {
            return coordinate1.CompareTo(coordinate2) >= 0;
        }

        public static AngularCoordinateInt32 Zero => new AngularCoordinateInt32(1, 0);
        public static AngularCoordinateInt32 Quarter => new AngularCoordinateInt32(0, 1);
        public static AngularCoordinateInt32 Half => new AngularCoordinateInt32(-1, 0);
        public static AngularCoordinateInt32 ThreeQuarters => new AngularCoordinateInt32(0, -1);

        public AngularCoordinateInt32(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool IsZero => (Y == 0) && (X >= 0);

        public AngularCoordinateInt32? TryGetOpposite()
        {
            checked
            {
                var normalized = Normalize();
                if ((normalized.X == int.MinValue) || (normalized.Y == int.MinValue))
                    return null;
                return new AngularCoordinateInt32(-normalized.X, -normalized.Y);
            }
        }

        public AngularCoordinateInt32 Normalize()
        {
            checked
            {
                if (IsZero)
                    return new AngularCoordinateInt32(1, 0);

                var gcd = X.Gcd(Y);
                return new AngularCoordinateInt32((int)(X / gcd), (int)(Y / gcd));
            }
        }

        public bool Equals(AngularCoordinateInt32 other)
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
            return (obj is AngularCoordinateInt32) && Equals((AngularCoordinateInt32)obj);
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

        public int CompareTo(AngularCoordinateInt32 other)
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
