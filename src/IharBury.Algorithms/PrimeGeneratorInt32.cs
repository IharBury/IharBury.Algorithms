using System.Collections;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public sealed class PrimeGeneratorInt32 : IEnumerable<int>
    {
        private List<int> primes = new List<int> { 2, 3 };
        private bool areAllPrimesFound;

        public IEnumerator<int> GetEnumerator()
        {
            var primeIndex = 0;
            while (true)
            {
                if (primeIndex == primes.Count)
                {
                    if (!TryFindNextPrime())
                        yield break;
                }

                yield return primes[primeIndex];
                primeIndex++;
            }
        }

        private bool TryFindNextPrime()
        {
            if (areAllPrimesFound)
                return false;

            var candidate = primes[primes.Count - 1];
            while (candidate <= int.MaxValue - 2)
            {
                candidate += 2;
                if (IsCandidatePrime(candidate))
                {
                    primes.Add(candidate);
                    return true;
                }
            }

            areAllPrimesFound = true;
            return false;
        }

        private bool IsCandidatePrime(int candidate)
        {
            foreach (var prime in primes)
            {
                if (candidate / prime < prime)
                    break;
                if (candidate % prime == 0)
                    return false;
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
