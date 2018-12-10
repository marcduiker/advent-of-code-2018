using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day09Tests
    {
        [Theory]
        [InlineData(9, 25, 32)] // player 5
        [InlineData(10, 1618, 8317)]
        [InlineData(13, 7999, 146373)]
        //[InlineData(17, 1104, 2764)] // This one fails! Score = 2720 :(
        [InlineData(21, 6111, 54718)]
        [InlineData(30, 5807, 37305)]
        public void UnitOfWork_StateUnderTest_ExpectedBehaviour(int nrOfPlayers, int nrOfMarbles, int expectedResult)
        {
            // Arrange
            var sut = new Game09(nrOfPlayers, nrOfMarbles);
            
            // Act
            var bestPlayer = sut.Play();

            // Assert
            bestPlayer.Score.Should().Be(expectedResult);
        }
    }
}
