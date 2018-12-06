using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day06Tests
    {
        [Fact]
        public void GetMapCoordinates()
        {
            // Arrange
            var input = new Dictionary<int, Point>
            {
                {1, new Point(1,2) },
                {2, new Point(5,7) }
            };

            // Act
            var sut = new Map(input);

            // Assert
            sut.TopLeft.X.Should().Be(1);
            sut.TopLeft.Y.Should().Be(2);
            sut.BottomRight.X.Should().Be(5);
            sut.BottomRight.Y.Should().Be(7);
        }
        
        [Fact]
        public void CalculateDistanceBetweenTwoPoints()
        {
            // Arrange
            var point1 = new Point(1, 1);
            var point2 = new Point(2, 4);
            var sut = new Day06();
            
            // Act
            var result = sut.CalculateDistance(point1, point2);

            // Assert
            result.Should().Be(4);
        }

        [Theory]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "4,1", 1)]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "5,1", 0)]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "1,4", 0)]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "0,4", 0)]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "2,5", 0)]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "8,6", 0)]
        [InlineData("1,1:1,6:8,3:3,4:5,5:8,9", "6,1", 3)]
        public void GetClosestPointForMapCoordinate(string inputPoints, string coordinate, int expectedResult)
        {
            // Arrange
            var inputList = inputPoints.Split(':');
            var sut = new Day06();
            var points = Day06.GetPoints(inputList);
            var coordinatePoint = Day06.GetPoints(new List<string>{coordinate}).First().Value;

            // Act
            var result = sut.GetClosestPointForMapCoordinate(coordinatePoint, points, out int pointOnCoordinate);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void GetLargestArea()
        {
            // Arrange
            var areas = new List<Area>
            {
                new Area(1, new Point(1,2)),
                new Area(2, new Point(6,4))
            };
            areas[0].Points.Add(new Point(3,3));
            var sut = new Day06();

            // Act
            var result = sut.GetLargestArea(areas);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void CalculateLargestAreaSize()
        {
            // Arrange
            var input = new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9"
            };
            var sut = new Day06();

            // Act
            var result = sut.CalculateLargestAreaSize(input);

            // Assert
            result.Should().Be(17);
        }

        [Fact]
        public void CalculateLargestAreaSizeNearPointsWithinRange()
        {
            // Arrange
            var input = new List<string>
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9"
            };
            var sut = new Day06();
            const int range = 32;

            // Act
            var result = sut.CalculateLargestAreaSizeNearPointsWithinRange(input, range);

            // Assert
            result.Should().Be(16);
        }
    }
}
