using System;
using System.Linq;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = @"C:\dev\git\personal\advent-of-code-2018\input\day06.txt";

            var day = new Day06();
            var result = day.CalculateLargestAreaSizeNearPointsWithinRange(input, 10000);
            
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
