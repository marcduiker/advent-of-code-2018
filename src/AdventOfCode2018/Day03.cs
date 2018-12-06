using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day03
    {
        private readonly List<Claim> claimsWithoutIntersections;

        public Day03()
        {
            claimsWithoutIntersections = new List<Claim>();
        }

        public int CalculateIntersections(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            var rectangles = input.Select(line => new Claim(line));

            return CalculateIntersections(rectangles.ToList());
        }

        public IEnumerable<Point> GetIntersectionPoints(Claim claim1, Claim claim2)
        {
            var intersection = claim1.Points.Intersect(claim2.Points);

            return intersection;
        }

        public IEnumerable<Point> ConvertToPoints(Rectangle rectangle)
        {
            for (int w = 0; w < rectangle.Width; w++)
            {
                for (int h = 0; h < rectangle.Height; h++)
                {
                    yield return new Point(rectangle.X + w, rectangle.Y + h);
                }
            }
        }

        public int CalculateIntersections(List<Claim> claims)
        {
            claimsWithoutIntersections.AddRange(claims);
            var intersectionPoints = new List<Point>();
            foreach (var claimOuter in claims)
            {
                foreach (var claimInner in claims)
                {
                    if (claimInner.Equals(claimOuter))
                        continue;
                    var intersection = GetIntersectionPoints(claimOuter, claimInner);
                    foreach (var point in intersection)
                    {
                        if (claimsWithoutIntersections.Contains(claimInner))
                        {
                            claimsWithoutIntersections.Remove(claimInner);
                        }

                        if (!intersectionPoints.Contains(point))
                        {
                            intersectionPoints.Add(point);
                        }
                    }
                }
            }

            return intersectionPoints.Count;
        }

        public Claim GetClaimWithoutIntersections(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            var claims = input.Select(line => new Claim(line)).ToList();

            return GetClaimsWithoutIntersections(claims).First();
        }

        public IEnumerable<Claim> GetClaimsWithoutIntersections(List<Claim> claims)
        {
            CalculateIntersections(claims);

            return claimsWithoutIntersections;
        }

        public class Claim
        {
            public Claim(string line)
            {
                Rectangle = ParseLine(line, out string id);
                Id = id;
                Points = ConvertToPoints(Rectangle);
            }

            public IEnumerable<Point> Points { get; }

            public string Id { get; }

            public Rectangle Rectangle { get; }

            public static Rectangle ParseLine(string line, out string id)
            {
                // #1 @ 393,863: 11x29,
                var split1 = line.Split('@');
                id = split1[0].Trim();
                var split2 = split1[1].Split(':');
                var coordinates = split2[0].Split(',');
                var sizes = split2[1].Split('x');

                var x = int.Parse(coordinates[0].Trim());
                var y = int.Parse(coordinates[1].Trim());
                var w = int.Parse(sizes[0].Trim());
                var h = int.Parse(sizes[1].Trim());

                return new Rectangle(x, y, w, h);
            }

            public IEnumerable<Point> ConvertToPoints(Rectangle rectangle)
            {
                for (int w = 0; w < rectangle.Width; w++)
                {
                    for (int h = 0; h < rectangle.Height; h++)
                    {
                        yield return new Point(rectangle.X + w, rectangle.Y + h);
                    }
                }
            }
        }
    }
}
