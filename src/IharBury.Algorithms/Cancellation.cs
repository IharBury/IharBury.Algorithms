namespace IharBury.Algorithms
{
    public sealed class Cancellation : ICancellation
    {
        private static readonly ICancellation nullCancellation = new NullCancellation();

        public static ICancellation Null => nullCancellation;


        public bool IsRequested { get; private set; }

        public void Cancel()
        {
            IsRequested = true;
        }

        public void Reset()
        {
            IsRequested = false;
        }

        private sealed class NullCancellation : ICancellation
        {
            public bool IsRequested => false;
        }
    }
}
