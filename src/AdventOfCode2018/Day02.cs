using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day02
    {
        public int CalculateChecksum(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            return CalculateChecksum(input);
        }

        public int CalculateChecksum(IEnumerable<string> inputLines)
        {
            var twoCount = 0;
            var threeCount = 0;
            foreach (var line in inputLines)
            {
                var counts = DuplicateCharacterCount(line);
                twoCount += counts.Item1;
                threeCount += counts.Item2;
            }

            return twoCount * threeCount;
        }

        public (int, int) DuplicateCharacterCount(string inputLine)
        {
            var twoCount = 0;
            var threeCount = 0;
            foreach (var character in inputLine)
            {
                var charCount = inputLine.Count(i => i == character);
                if (charCount == 2)
                {
                    twoCount = 1;
                }
                else if (charCount == 3)
                {
                    threeCount = 1;
                }
            }

            return (twoCount, threeCount);
        }

        public string GetMatchingCharacters(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            return GetMatchingCharacters(input.ToList());
        }


        public string GetMatchingCharacters(List<string> inputLines)
        {
            var highestNumberOfCharacterMatches = new List<char>();

            foreach (var lineOuter in inputLines)
            {
                foreach (var lineInner in inputLines)
                {
                    var characterMatches = new List<char>();
                    if (lineInner != lineOuter)
                    {
                        for (int i = 0; i < lineInner.Length; i++)
                        {
                            if (lineInner[i] == lineOuter[i])
                            {
                                characterMatches.Add(lineInner[i]);
                            }
                        }

                        if (characterMatches.Count > highestNumberOfCharacterMatches.Count)
                        {
                            highestNumberOfCharacterMatches = characterMatches;
                        }
                    }

                }
            }

            return string.Join(string.Empty, highestNumberOfCharacterMatches);
        }
    }
}
