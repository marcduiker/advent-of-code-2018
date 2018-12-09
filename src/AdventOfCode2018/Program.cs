using System;
using System.Linq;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = @"C:\dev\git\personal\advent-of-code-2018\input\day08.txt";

            var day = new Day08();
            var result = day.CalculateRoot(input);
            
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
