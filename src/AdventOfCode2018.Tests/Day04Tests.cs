using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day04Tests
    {
        [Fact]
        public void ExtractDataFromInput()
        {
            // Arrange
            var inputLine = "[1518-09-02 23:58] Guard #733 begins shift";
            

            // Act
            var result = new GuardLine(inputLine); ;

            // Assert
            result.DateTime.Should().Be(new DateTime(1518, 9, 2, 23, 58, 0));
            result.Id.Should().Be("733");
        }

        [Fact]
        public void FillSleepGapsWithGuardLines()
        {
            // Arrange
            var inputLines = new List<GuardLine>
            {
                new GuardLine("[1518-09-02 23:58] Guard #733 begins shift"),
                new GuardLine("[1518-09-03 00:01] falls asleep"),
                new GuardLine("[1518-09-03 00:04] wakes up")
            };
            var sut = new Day04();

            // Act
            var result = sut.FillSleepGapsWithGuardLines(inputLines);

            // Assert
            result.Count.Should().Be(5);
            result.Count(g => g.IsSleeping).Should().Be(3);
        }

        [Fact]
        public void GetGuardWhichSleepsMost()
        {
            // Arrange
            var inputLines = new List<string>
            {
                "[1518-09-02 23:58] Guard #733 begins shift",
                "[1518-09-02 23:59] falls asleep",
                "[1518-09-03 00:02] wakes up",
                "[1518-09-04 00:00] Guard #733 begins shift",
                "[1518-09-04 00:01] falls asleep",
                "[1518-09-04 00:02] wakes up",
                "[1518-09-06 00:00] Guard #733 begins shift",
                "[1518-09-06 00:03] falls asleep",
                "[1518-09-06 00:05] wakes up"
            };

            var sut = new Day04();

            // Act
            var result = sut.GetGuardWhichSleepsMost(inputLines, out string message);

            // Assert
            result.Count().Should().Be(5);
            result.FirstOrDefault().Minute.Should().Be(1);
            result.FirstOrDefault().Count.Should().Be(2);
        }
    }
}
