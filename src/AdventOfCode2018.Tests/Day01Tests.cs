using System.Linq;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day01Tests
    {
        [Theory]
        [InlineData("+1,+1,+1", 3)]
        [InlineData("-1,-1,-1", -3)]
        [InlineData("+1,+1,-2", 0)]
        [InlineData("-1,-2,-3", -6)]
        public void CalculateFrequency(string input, int expectedResult)
        {
            // Arrange
            var inputArray = input.Split(',');
            var sut = new Day01();
            
            // Act
            var result = sut.CalculateFrequency(inputArray);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("+3,+3,+4,-2,-4", 10)]
        [InlineData("-6,+3,+8,+5,-6", 5)]
        [InlineData("+7,+7,-2,-7,-4", 14)]
        public void CalculateFirstFrequencyToAppearTwice(string input, int expectedResult)
        {
            // Arrange
            var inputArray = input.Split(',');
            var sut = new Day01();
            
            // Act
            var result = sut.CalculateFrequencyToAppearTwice(inputArray.ToList());

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
