using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day10Tests
    {
        [Theory]
        [InlineData(@"position=< 9,  1> velocity=< 0,  2>", 9, 1, 0, 2)]
        [InlineData(@"position=< 7,  0> velocity=<-1,  0>", 7, 0, -1, 0)]
        [InlineData(@"position=< 3, -2> velocity=<-1,  1>", 3, -2, -1, 1)]
        [InlineData(@"position=< 6, 10> velocity=<-2, -1>", 6, 10, -2, -1)]
        public void ParseLine(string input, int pointX, int pointY, int speedX, int speedY)
        {
            // Arrange

            // Act
            var sut = new PointWithVector(input);

            // Assert
            sut.Point.Should().BeEquivalentTo(new Point(pointX, pointY));
            sut.Vector.Should().BeEquivalentTo(new Point(speedX, speedY));

        }
    }
}
