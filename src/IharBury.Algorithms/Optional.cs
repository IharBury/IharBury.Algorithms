using System;

namespace IharBury.Algorithms
{
    public struct Optional<T>
    {
        public static implicit operator Optional<T>(T value)
        {
            return new Optional<T>(value);
        }

        private readonly T value;

        public Optional(T value)
        {
            HasValue = true;
            this.value = value;
        }

        public bool HasValue { get; }
        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("There is no value.");
                return value;
            }
        }

        public override string ToString() => HasValue ? $"{Value}" : $"Empty {typeof(T)}";

        public T OrDefault(T defaultValue) => HasValue ? Value : defaultValue;

        public T OrDefault(Func<T> getDefaultValue)
        {
            if (getDefaultValue == null)
                throw new ArgumentNullException(nameof(getDefaultValue));

            return HasValue ? Value : getDefaultValue();
        }
    }
}
