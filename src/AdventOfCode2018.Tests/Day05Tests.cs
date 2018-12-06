using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day05Tests
    {
        [Theory]
        [InlineData("aA", 0)]
        [InlineData("abBA", 0)]
        [InlineData("abAB", 4)]
        [InlineData("aabAAB", 6)]
        [InlineData("dabAcCaCBAcCcaDA", 10)]
        public void React(string input, int expectedLength)
        {
            // Arrange
            var sut = new Polymer(input);

            // Act
            var result = sut.React();

            // Assert
            result.Should().Be(expectedLength);
        }


        [Theory]
        [InlineData("dabAcCaCBAcCcaDA", "aA", 6)]
        [InlineData("dabAcCaCBAcCcaDA", "bB", 8)]
        [InlineData("dabAcCaCBAcCcaDA", "cC", 4)]
        [InlineData("dabAcCaCBAcCcaDA", "dD", 6)]
        public void Reduce(string input, string charsToRemove, int expectedLength)
        {
            // Arrange
            var sut = new Polymer(input, charsToRemove.ToCharArray());

            // Act
            var result = sut.React();

            // Assert
            result.Should().Be(expectedLength);
        }

    }
}
