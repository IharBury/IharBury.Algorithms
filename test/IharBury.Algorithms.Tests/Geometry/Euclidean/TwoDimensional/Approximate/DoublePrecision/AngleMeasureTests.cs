using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class AngleMeasureTests
    {
        [Fact]
        public void ConstructedAngleMeasureHasTheGivenMeasureInRadians()
        {
            Assert.Equal(5.92, new AngleMeasure(5.92).InRadians);
        }
    }
}
