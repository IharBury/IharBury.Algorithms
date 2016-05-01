using System.Linq;
using Xunit;

namespace IharBury.Algorithms.Tests
{
    public class PrimeGeneratorInt32Tests
    {
        [Fact]
        public void FirstInt32PrimesCanBeEnumerated()
        {
            var primes = new PrimeGeneratorInt32().Take(7).ToList();
            Assert.Equal(2, primes[0]);
            Assert.Equal(3, primes[1]);
            Assert.Equal(5, primes[2]);
            Assert.Equal(7, primes[3]);
            Assert.Equal(11, primes[4]);
            Assert.Equal(13, primes[5]);
            Assert.Equal(17, primes[6]);
        }
    }
}
