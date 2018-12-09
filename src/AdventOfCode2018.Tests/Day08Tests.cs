using System;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day08Tests
    {
        [Theory]
        [InlineData("0 1 99", 99)]
        [InlineData("1 1 0 1 3 2", 5)]
        [InlineData("1 1 0 1 99 2", 101)]
        [InlineData("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", 138)]
        public void CalculateSumOfMetaData(string input, int expectedOutcome)
        {
            // Arrange
            var sut = new Day08();
            
            // Act
            var result = sut.CalculateSumOfMetaData(input);

            // Assert
            result.Should().Be(expectedOutcome);
        }

        [Theory]
        [InlineData("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", 66)]
        public void CalculateSumOfRoot(string input, int expectedOutcome)
        {
            var sut = new Day08();

            var result = sut.CalculateSumOfRootNode(input);

            result.Should().Be(expectedOutcome);
        }

        [Theory]
        [InlineData("0 3 10 11 12", 33)]
        [InlineData("0 1 99 2", 99)]
        public void CalculateNodeValue(string nodeInput, int expectedOutcome)
        {
            // Arrange
            var inputNumbers = nodeInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var i = 0;
            var sut = new Day08();
            var node = Day08.CreateNode(i, inputNumbers);

            // Act
            sut.AddMetaData(node, i+2,inputNumbers);


            //Assert
            node.Value.Should().Be(expectedOutcome);
        }
    }
}
