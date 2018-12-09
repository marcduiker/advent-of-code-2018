using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day07Tests
    {
        [Fact]
        public void DetermineConstructionOrder()
        {
            // Arrange
            var input = new List<string>
            {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin."
            };
            var sut = new Day07();

            // Act
            var result = sut.DetermineConstructionOrder(input);

            // Assert
            result.Should().Be("CABDFE");

        }

        [Theory]
        [InlineData("Step C must be finished before step A can begin.", 'C', 'A')]
        public void ParseLine(string input, char expectedFirst, char expectedSecond)
        {
            // Arrange
            var sut = new Day07();

            // Act
            var result = sut.ParseLine(input);

            // Assert
            result.Should().Be((expectedFirst, expectedSecond));
        }
    }
}
