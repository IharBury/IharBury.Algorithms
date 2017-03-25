﻿using IharBury.Algorithms.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision;
using Xunit;

namespace IharBury.Algorithms.Tests.Geometry.Euclidean.TwoDimensional.Approximate.DoublePrecision
{
    public sealed class LineSegmentSpecs
    {
        [Fact]
        public void ConstructedLineSegmentHasTheGivenBaseEndpointX()
        {
            Assert.Equal(2, new LineSegment(new Point(2, 3), new Point(4, 7)).BaseEndpoint.X);
        }

        [Fact]
        public void ConstructedLineSegmentHasTheGivenBaseEndpointY()
        {
            Assert.Equal(3, new LineSegment(new Point(2, 3), new Point(4, 7)).BaseEndpoint.Y);
        }

        [Fact]
        public void ConstructedLineSegmentHasTheGivenAnotherEndpointX()
        {
            Assert.Equal(4, new LineSegment(new Point(2, 3), new Point(4, 7)).AnotherEndpoint.X);
        }

        [Fact]
        public void ConstructedLineSegmentHasTheGivenAnotherEndpointY()
        {
            Assert.Equal(7, new LineSegment(new Point(2, 3), new Point(4, 7)).AnotherEndpoint.Y);
        }

        [Fact]
        public void SquaredLengthIsCalculatedCorrectly()
        {
            Assert.Equal(20, new LineSegment(new Point(2, 3), new Point(4, 7)).SquaredLength, 5);
        }

        [Fact]
        public void LengthIsCalculatedCorrectly()
        {
            Assert.Equal(4.4721359549995793928183473374626, new LineSegment(new Point(2, 3), new Point(4, 7)).Length, 5);
        }

        [Fact]
        public void MiddleIsCalculatedCorrectly()
        {
            var lineSegment = new LineSegment(new Point(2, 3), new Point(4, 7));
            Assert.True(lineSegment.Middle.HasSameCoordinatesAs(new Point(3, 5), 0.00001));
        }

        [Fact]
        public void LineSegmentHasSameCoordinatesAsCloseEnoughLineSegmentWithSameEndpointOrderWithTheGivenSquaredDistanceError()
        {
            var lineSegment1 = new LineSegment(new Point(2.001, 3.001), new Point(6.001, -2.001));
            var lineSegment2 = new LineSegment(new Point(2, 3), new Point(6, -2));
            Assert.True(lineSegment1.HasSameCoordinatesAs(lineSegment2, 0.00001));
        }

        [Fact]
        public void LineSegmentHasSameCoordinatesAsCloseEnoughLineSegmentWithAnotherEndpointOrderWithTheGivenSquaredDistanceError()
        {
            var lineSegment1 = new LineSegment(new Point(2.001, 3.001), new Point(6.001, -2.001));
            var lineSegment2 = new LineSegment(new Point(6, -2), new Point(2, 3));
            Assert.True(lineSegment1.HasSameCoordinatesAs(lineSegment2, 0.00001));
        }

        [Fact]
        public void LineSegmentDoesNotHaveSameCoordinatesAsFarEnoughLineSegmentWithTheGivenSquaredDistanceError()
        {
            var lineSegment1 = new LineSegment(new Point(2.01, 3.01), new Point(6.01, -2.01));
            var lineSegment2 = new LineSegment(new Point(2, 3), new Point(6, -2));
            Assert.False(lineSegment1.HasSameCoordinatesAs(lineSegment2, 0.00001));
        }

        [Fact]
        public void LineSegmentHasSameSizeAsSimilarEnoughLineSegmentWithTheGivenSquaredLengthError()
        {
            var lineSegment1 = new LineSegment(new Point(2, 3), new Point(2.000001, 5.000001));
            var lineSegment2 = new LineSegment(new Point(2, 3), new Point(4, 3));
            Assert.True(lineSegment1.HasSameSizeAs(lineSegment2, 0.00001));
        }

        [Fact]
        public void LineSegmentDoesNotHaveHasSameSizeAsDifferentEnoughLineSegmentWithTheGivenSquaredLengthError()
        {
            var lineSegment1 = new LineSegment(new Point(2, 3), new Point(2.01, 5.01));
            var lineSegment2 = new LineSegment(new Point(2, 3), new Point(4, 3));
            Assert.False(lineSegment1.HasSameSizeAs(lineSegment2, 0.00001));
        }
    }
}
