using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day02Tests
    {
        [Theory]
        [InlineData("abcdef", 0, 0)]
        [InlineData("bababc", 1, 1)]
        [InlineData("abbcde", 1, 0)]
        [InlineData("abcccd", 0, 1)]
        [InlineData("aabcdd", 1, 0)]
        [InlineData("abcdee", 1, 0)]
        [InlineData("ababab", 0, 1)]
        public void DuplicateCharacterCount(string input, int twoCount, int threeCount)
        {
            // Arrange
            var sut = new Day02();

            // Act
            var result = sut.DuplicateCharacterCount(input);

            // Assert
            result.Item1.Should().Be(twoCount);
            result.Item2.Should().Be(threeCount);
        }

        [Theory]
        [InlineData("abcdef,bababc,abbcde,abcccd,aabcdd,abcdee,ababab", 12)]
        public void CalculateCheckSum(string input, int expectedChecksum)
        {
            // Arrange
            var inputLines = input.Split(',');
            var sut = new Day02();

            // Act
            var result = sut.CalculateChecksum(inputLines);

            // Assert
            result.Should().Be(expectedChecksum);
        }

        [Theory]
        [InlineData("abcde,fghij,klmno,pqrst,fguij,axcye,wvxyz", "fgij")]
        [InlineData("abcde,aecdb,aeddb,abccc,eaccc", "aedb")]
        public void GetMatchingCharacters(string input, string expectedMatch)
        {
            // Arrange
            var inputLines = input.Split(',');
            var sut = new Day02();

            // Act
            var result = sut.GetMatchingCharacters(inputLines.ToList());

            // Assert
            result.Should().Be(expectedMatch);
        }
    }
}
