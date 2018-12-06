using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day06
    {
        public int CalculateLargestAreaSize(string inputPath)
        {
            var inputLines = File.ReadAllLines(inputPath);

            return CalculateLargestAreaSize(inputLines);
        }

        public int CalculateLargestAreaSizeNearPointsWithinRange(string inputPath, int range)
        {
            var inputLines = File.ReadAllLines(inputPath);

            return CalculateLargestAreaSizeNearPointsWithinRange(inputLines, range);
        }

        public int CalculateLargestAreaSize(IEnumerable<string> inputList)
        {
            // read points into list
            var points = GetPoints(inputList);

            // find corners of map
            var map = new Map(points);

            // iterate over map coordinate array
            var areas = new List<Area>();
            int padding = 1000;
            for (int y = map.TopLeft.Y - padding; y < map.BottomRight.Y + padding; y++)
            {
                for (int x = map.TopLeft.X - padding; x < map.BottomRight.X + padding; x++)
                {
                    var coordinate = new Point(x, y);
                    var closestPointKey = GetClosestPointForMapCoordinate(coordinate, points, out int idOnCoordinate);
                    if (closestPointKey != 0)
                    {
                        if (areas.All(a => a.Id != closestPointKey))
                        {
                            var area = new Area(closestPointKey, coordinate);
                            areas.Add(area);
                        }
                        else
                        {
                            areas.First(a => a.Id == closestPointKey).Points.Add(coordinate);
                        }
                    }
                }
            }

            // Find corner areas
            var closestToTopLeft = GetClosestPointForMapCoordinate(map.TopLeft, points, out int idOnCoordinateTL);
            var closestToBottomRight = GetClosestPointForMapCoordinate(map.BottomRight, points, out int idOnCoordinateBR);
            var topRightPoint = new Point(map.BottomRight.X, map.TopLeft.Y);
            var closestToTopRight = GetClosestPointForMapCoordinate(topRightPoint, points, out int idOnCoordinateTR);
            var bottomLeftPoint = new Point(map.TopLeft.X, map.BottomRight.Y);
            var closestToBottomLeft = GetClosestPointForMapCoordinate(bottomLeftPoint, points, out int idOnCoordinateBL);

            var idsToRemove = new List<int>
            {
                idOnCoordinateTL != 0 ? idOnCoordinateTL : closestToTopLeft,
                idOnCoordinateBR != 0 ? idOnCoordinateBR : closestToBottomRight,
                idOnCoordinateTR != 0 ? idOnCoordinateTR : closestToTopRight,
                idOnCoordinateBL != 0 ? idOnCoordinateBL : closestToBottomLeft
            };

            var sortedAreas = areas.OrderByDescending(a => a.Points.Count);

            // removed corner areas
            var cleanedAreas = areas.Where(a => !idsToRemove.Contains(a.Id) && a.Points.Count < 10000).ToList();

            int largestArea = GetLargestArea(cleanedAreas);

            return largestArea;
        }

        public int CalculateLargestAreaSizeNearPointsWithinRange(IEnumerable<string> inputList, int distanceRange)
        {
            // read points into list
            var points = GetPoints(inputList);

            // find corners of map
            var map = new Map(points);

            // iterate over map coordinate array
            var areaWithInDistanceRange = new Area(0);
            int padding = 100;
            for (int y = map.TopLeft.Y - padding; y < map.BottomRight.Y + padding; y++)
            {
                for (int x = map.TopLeft.X-padding; x < map.BottomRight.X+padding; x++)
                {
                    var coordinate = new Point(x, y);
                    var distanceSum = 0;
                    foreach (var point in points)
                    {
                        distanceSum += CalculateDistance(coordinate, point.Value);
                    }
                    
                    if (distanceSum < distanceRange)
                    {
                        areaWithInDistanceRange.Points.Add(coordinate);
                    }
                }
            }

            return areaWithInDistanceRange.Points.Count;
        }

        public int GetLargestArea(List<Area> areas)
        {
            return areas.OrderByDescending(a => a.Points.Count).FirstOrDefault().Points.Count;
        }

        public int GetClosestPointForMapCoordinate(Point mapCoordinate, Dictionary<int, Point> points, out int idOnCoordinate)
        {
            idOnCoordinate = 0;
            int shortestDistancePointKey = 0;
            int shortestDistance = 10000;
            foreach (var point in points)
            {
                if (point.Value == mapCoordinate)
                {
                    idOnCoordinate = point.Key;
                    shortestDistancePointKey = point.Key;
                    break;
                }
                var distance = CalculateDistance(mapCoordinate, point.Value);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestDistancePointKey = point.Key;
                }
                else if (distance == shortestDistance)
                {
                    shortestDistance = distance;
                    shortestDistancePointKey = 0;
                    
                }
            }

            return shortestDistancePointKey;
        }

        public static Dictionary<int, Point> GetPoints(IEnumerable<string> inputList)
        {
            var points = new Dictionary<int, Point>();

            int pointId = 1;
            foreach (var input in inputList)
            {
                var split = input.Split(',');
                points.Add(pointId, new Point(int.Parse(split[0]), int.Parse(split[1])));
                pointId++;
            }

            return points;
        }

        public int CalculateDistance(Point point1, Point point2)
        {
            int xDistance = Math.Abs(point1.X - point2.X);
            int yDistance = Math.Abs(point1.Y - point2.Y);

            return xDistance + yDistance;
        }
    }

    public class Area
    {
        public Area(int id)
        {
            Id = id;
            Points = new List<Point>();
        }

        public Area(int id, Point point)
        {
            Id = id;
            Points = new List<Point> { point };
        }

        public int Id { get; }

        public List<Point> Points { get; }
    }

    public class Map
    {
        public Map(Dictionary<int, Point> points)
        {
            int lowX = points.Values.OrderBy(p => p.X).Select(p => p.X).FirstOrDefault();
            int highX = points.Values.OrderByDescending(p => p.X).Select(p => p.X).FirstOrDefault();
            int lowY = points.Values.OrderBy(p => p.Y).Select(p => p.Y).FirstOrDefault();
            int highY = points.Values.OrderByDescending(p => p.Y).Select(p => p.Y).FirstOrDefault();

            TopLeft = new Point(lowX, lowY);
            BottomRight = new Point(highX, highY);
        }

        public Point TopLeft { get; }

        public Point BottomRight { get; }
    }
}
