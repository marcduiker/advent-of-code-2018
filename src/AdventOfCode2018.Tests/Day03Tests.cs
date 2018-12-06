using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2018.Tests
{
    public class Day03Tests
    {

        [Fact]
        public void IntersectTwoDimensionalArray()
        {
            var rect1 = new Rectangle(1, 3, 4, 4);
            var rect2 = new Rectangle(3, 1, 4, 4);
            rect2.Intersect(rect1);
            var result = rect2;
            result.X.Should().Be(3);
            result.Y.Should().Be(3);
            result.Height.Should().Be(2);
            result.Width.Should().Be(2);
        }

        [Theory]
        [InlineData("#1 @ 1,3: 4x4", 1, 3, 4, 4, "#1")]
        [InlineData("#2 @ 3,1: 4x4", 3, 1, 4, 4, "#2")]
        [InlineData("#3 @ 5,5: 2x2", 5, 5, 2, 2, "#3")]
        [InlineData("#435 @ 393,863: 11x29", 393, 863, 11, 29, "#435")]
        public void InitializeClaim(string input, int x, int y, int width, int height, string id)
        {
            var claim = new Day03.Claim(input);

            claim.Id.Should().Be(id);
            claim.Rectangle.X.Should().Be(x);
            claim.Rectangle.Y.Should().Be(y);
            claim.Rectangle.Height.Should().Be(height);
            claim.Rectangle.Width.Should().Be(width);
        }

        [Fact]
        public void CalculateDifference()
        {
            var claim1 = new Day03.Claim("#1 @ 1,3: 4x4");
            var claim2 = new Day03.Claim("#2 @ 3,1: 4x4");
            var sut = new Day03();
            var result = sut.GetIntersectionPoints(claim1, claim2);

            result.Count().Should().Be(4);
        }

        [Fact]
        public void CalculateIntersections()
        {
            var claim1 = new Day03.Claim("#1 @ 1,3: 4x4"); 
            var claim2 = new Day03.Claim("#2 @ 3,1: 4x4"); 
            var claim3 = new Day03.Claim("#3 @ 3,3: 3x2");
            var list = new List<Day03.Claim> {claim1, claim2, claim3};
            var sut = new  Day03();

            var result = sut.CalculateIntersections(list);
            result.Should().Be(6);
        }

        [Fact]
        public void GetClaimWithoutIntersections()
        {
            var claim1 = new Day03.Claim("#1 @ 1,3: 4x4");
            var claim2 = new Day03.Claim("#2 @ 3,1: 4x4");
            var claim3 = new Day03.Claim("#3 @ 5,5: 2x2");
            var list = new List<Day03.Claim> { claim1, claim2, claim3 };
            var sut = new Day03();

            var result = sut.GetClaimsWithoutIntersections(list).First();
            result.Id.Should().Be("#3");
        }
    }
}
