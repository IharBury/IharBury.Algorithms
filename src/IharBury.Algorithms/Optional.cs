using System;

namespace IharBury.Algorithms
{
    public struct Optional<T>
    {
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
                    throw new InvalidOperationException($"!{nameof(HasValue)}");
                return value;
            }
        }

        public override string ToString() => HasValue ? $"{Value}" : $"Empty {typeof(T)}"; 
    }
}
