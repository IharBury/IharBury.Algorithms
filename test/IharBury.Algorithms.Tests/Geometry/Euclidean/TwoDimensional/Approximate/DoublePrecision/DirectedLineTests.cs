using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class DirectedLineTests
    {
        [Fact]
        public void ConstructedDirectedLineHasTheGivenBasePointX()
        {
            Assert.Equal(2, new DirectedLine(new Point(2, 3), new Point(4, 7)).BasePoint.X);
        }

        [Fact]
        public void ConstructedDirectedLineHasTheGivenBasePointY()
        {
            Assert.Equal(3, new DirectedLine(new Point(2, 3), new Point(4, 7)).BasePoint.Y);
        }

        [Fact]
        public void ConstructedDirectedLineHasTheGivenForwardPointX()
        {
            Assert.Equal(4, new DirectedLine(new Point(2, 3), new Point(4, 7)).ForwardPoint.X);
        }

        [Fact]
        public void ConstructedDirectedLineHasTheGivenForwardPointY()
        {
            Assert.Equal(7, new DirectedLine(new Point(2, 3), new Point(4, 7)).ForwardPoint.Y);
        }

        [Fact]
        public void WhenAPointIsToTheLeftItIsDetected()
        {
            Assert.True(new DirectedLine(new Point(1, 1), new Point(2, 1)).HasToTheLeft(new Point(3, 3), 0.00001));
        }

        [Fact]
        public void WhenAPointIsCloseEnoughToTheRightItIsApproximatelyToTheLeft()
        {
            Assert.True(new DirectedLine(new Point(1, 1), new Point(1, 2)).HasToTheLeft(new Point(1.001, 3), 0.00001));
        }

        [Fact]
        public void WhenAPointIsFarEnoughToTheRightItIsNotApproximatelyToTheLeft()
        {
            Assert.False(new DirectedLine(new Point(1, 1), new Point(1, 2)).HasToTheLeft(new Point(3, 3), 0.00001));
        }
    }
}
