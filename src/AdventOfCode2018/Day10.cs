using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day10
    {
        private readonly List<PointWithVector> points = new List<PointWithVector>();
        private int iteration = 0;

        public void GetMessage(string inputPath)
        {
            var inputLines = File.ReadAllLines(inputPath);
            
            GetMessage(inputLines);
        }

        public void GetMessage(IEnumerable<string> inputLines)
        {
            
            foreach (var line in inputLines)
            {
                var pwv = new PointWithVector(line);
                points.Add(pwv);
            }

            while (true)
            {
                var withinRange = MovePoints();
                if (withinRange)
                {
                    Draw();
                    Console.ReadKey();
                }
            } 
        }

        private bool MovePoints()
        {
            var withinRange = false;
            foreach (var point in points)
            {
                point.Move();
            }

            iteration++;

            var groupedByY = points.GroupBy(p => p.Point.Y);
            withinRange = groupedByY.Any(p => p.Count() > 20);
            return withinRange;
        }

        private void Draw()
        {
            var delta = 2;
            var topLeftPoint = new Point(points.Min(p => p.Point.X) - delta, points.Min(p => p.Point.Y) - delta);
            var bottomRightPoint = new Point(points.Max(p => p.Point.X) + delta, points.Max(p => p.Point.Y) + delta);

            for (int y = topLeftPoint.Y; y < bottomRightPoint.Y; y++)
            {
                for (int x = topLeftPoint.X; x < bottomRightPoint.X; x++)
                {
                    if (points.Any(p => p.Point.X == x && p.Point.Y == y))
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }

                Console.Write(Environment.NewLine);
            }

            Console.WriteLine($"Iteration: {iteration}");
        }
    }

    public class PointWithVector
    {
        
        public PointWithVector(string input)
        {
            Point = GetPoint(input);
            var vCharIndex = input.IndexOf('v');
            Vector = GetPoint(input, vCharIndex);
        }

        public Point Point { get; set; }

        public Point Vector { get; set; }

        public void Move()
        {
            Point = new Point(Point.X + Vector.X, Point.Y + Vector.Y);
        }

        private static Point GetPoint(string input, int startIndex=0)
        {
            var firstLessThan = input.IndexOf('<', startIndex);
            var firstGreaterThan = input.IndexOf('>', startIndex);
            var firstComma = input.IndexOf(',', startIndex);
            var px = input.Substring(firstLessThan + 1, firstComma - firstLessThan-1).Trim();
            var py = input.Substring(firstComma + 1, firstGreaterThan - firstComma-1).Trim();

            return new Point(int.Parse(px), int.Parse(py));
        }
    }

    /*
     *  position=< 9,  1> velocity=< 0,  2>
        position=< 7,  0> velocity=<-1,  0>
        position=< 3, -2> velocity=<-1,  1>
        position=< 6, 10> velocity=<-2, -1>
        position=< 2, -4> velocity=< 2,  2>
        position=<-6, 10> velocity=< 2, -2>
        position=< 1,  8> velocity=< 1, -1>
        position=< 1,  7> velocity=< 1,  0>
        position=<-3, 11> velocity=< 1, -2>
        position=< 7,  6> velocity=<-1, -1>
        position=<-2,  3> velocity=< 1,  0>
        position=<-4,  3> velocity=< 2,  0>
        position=<10, -3> velocity=<-1,  1>
        position=< 5, 11> velocity=< 1, -2>
        position=< 4,  7> velocity=< 0, -1>
        position=< 8, -2> velocity=< 0,  1>
        position=<15,  0> velocity=<-2,  0>
        position=< 1,  6> velocity=< 1,  0>
        position=< 8,  9> velocity=< 0, -1>
        position=< 3,  3> velocity=<-1,  1>
        position=< 0,  5> velocity=< 0, -1>
        position=<-2,  2> velocity=< 2,  0>
        position=< 5, -2> velocity=< 1,  2>
        position=< 1,  4> velocity=< 2,  1>
        position=<-2,  7> velocity=< 2, -2>
        position=< 3,  6> velocity=<-1, -1>
        position=< 5,  0> velocity=< 1,  0>
        position=<-6,  0> velocity=< 2,  0>
        position=< 5,  9> velocity=< 1, -2>
        position=<14,  7> velocity=<-2,  0>
        position=<-3,  6> velocity=< 2, -1>
     *
     *
     *
     */
}
