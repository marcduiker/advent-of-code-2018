using System;
using System.IO;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayFile = "day10.txt";
            var input = Path.Combine(@"C:\dev\git\personal\advent-of-code-2018\input\", dayFile);

            var day = new Day10();
            
            day.GetMessage(input);

            //Console.WriteLine(result.ToString());
            Console.ReadKey();
        }
    }
}
