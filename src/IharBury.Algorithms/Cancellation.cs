namespace IharBury.Algorithms
{
    public sealed class Cancellation
    {
        public bool IsCancellationRequested { get; private set; }

        public void Cancel()
        {
            IsCancellationRequested = true;
        }
    }
}
