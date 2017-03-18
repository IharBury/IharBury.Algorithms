using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class Optional
    {
        /// <summary>
        /// Constructs <see cref="Optional{T}"/> representing the given value.
        /// </summary>
        /// <param name="value">The value to be represented.</param>
        public static Optional<T> From<T>(T value) => new Optional<T>(value);
    }

    /// <summary>
    /// Represents a value of type <typeparamref name="T"/> that can be missing.
    /// A missing value is still strongly typed and is not represented as <c>null</c>.
    /// </summary>
    /// <typeparam name="T">Type of the value when it is present.</typeparam>
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        public static implicit operator Optional<T>(T value)
        {
            return new Optional<T>(value);
        }

        private static readonly IEqualityComparer<T> valueEqualityComparer = EqualityComparer<T>.Default;

        /// <summary>
        /// Represents the missing value.
        /// </summary>
        public static Optional<T> None => new Optional<T>();

        private readonly T value;

        /// <summary>
        /// Constructs <see cref="Optional{T}"/> representing the given value.
        /// </summary>
        /// <param name="value">The value to be represented.</param>
        public Optional(T value)
        {
            HasValue = true;
            this.value = value;
        }

        /// <summary>
        /// Whether the value is present.
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// The represented value when it is present.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the value is missing.</exception>
        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("There is no value.");

                return value;
            }
        }

        public override string ToString() => HasValue ? $"{Value}" : $"Empty {typeof(Optional<T>)}";

        /// <summary>
        /// Returns the represented value or the given default value when the value is missing.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        public T OrDefault(T defaultValue = default(T)) => HasValue ? Value : defaultValue;

        /// <summary>
        /// Returns the represented value or a default value when the value is missing.
        /// </summary>
        /// <param name="getDefaultValue">
        /// The delegate that should return the default value.
        /// Cannot be <c>null</c>.
        /// Is not invoked when the value is present.
        /// </param>
        /// <returns></returns>
        public T OrDefault(Func<T> getDefaultValue)
        {
            if (getDefaultValue == null)
                throw new ArgumentNullException(nameof(getDefaultValue));

            return HasValue ? Value : getDefaultValue();
        }

        /// <summary>
        /// Two optional values are considered equal when they represent the same present value
        /// or when they both represent a missing value.
        /// </summary>
        public bool Equals(Optional<T> other) =>
            HasValue ? other.HasValue && valueEqualityComparer.Equals(value, other.value) : !other.HasValue;

        /// <summary>
        /// Two optional values are considered equal when they represent the same present value
        /// or when they both represent a missing value.
        /// An optional value is not considered equal to the value it represents as the type is different.
        /// </summary>
        public override bool Equals(object obj) => (obj is Optional<T> other) && Equals(other);

        /// <summary>
        /// Two optional values are considered equal when they represent the same present value
        /// or when they both represent a missing value.
        /// </summary>
        public override int GetHashCode() =>
            HasValue ? unchecked(88_657 * (value == null ? 104_173 : valueEqualityComparer.GetHashCode(value))) : 103_043;
    }
}
