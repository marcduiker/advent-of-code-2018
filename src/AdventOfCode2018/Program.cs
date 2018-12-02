using System;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = @"C:\dev\git\personal\advent-of-code-2018\input\day02.txt";

            var day = new Day02();
            string result =  day.GetMatchingCharacters(input);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
