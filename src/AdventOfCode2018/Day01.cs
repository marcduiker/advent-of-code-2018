using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day01
    {
        public int CalculateFrequency(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            return CalculateFrequency(input);
        }

        public int CalculateFrequencyToAppearTwice(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            return CalculateFrequencyToAppearTwice(input.ToList());
        }

        public int CalculateFrequency(IEnumerable<string> inputLines)
        {
            int frequency = 0;
            foreach (var line in inputLines)
            {
                var parsed = int.Parse(line);
                frequency += parsed;
            }

            return frequency;
        }

        public int CalculateFrequencyToAppearTwice(List<string> inputLines)
        {
            int frequency = 0;
            var calculatedFrequencies = new List<int>();
            bool foundDoubleFrequency = false;
            do
            {
                foreach (var line in inputLines)
                {
                    var parsed = int.Parse(line);
                    frequency += parsed;
                    if (!calculatedFrequencies.Contains(frequency)){
                        calculatedFrequencies.Add(frequency);
                    }
                    else
                    {
                        foundDoubleFrequency = true;
                        break;
                    }
                }
            } while (!foundDoubleFrequency);

            return frequency;
        }
    }
}
