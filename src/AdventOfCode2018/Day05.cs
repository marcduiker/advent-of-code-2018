using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day05
    {
        public int Reduce(string inputPath)
        {
            var inputText = File.ReadAllText(inputPath);
            var polymers = new List<int>();
            for (int i = 0; i < 26; i++)
            {
                var char1 = (char)(i + 65);
                var char2 = (char)(char1 + 32);
                var polymer = new Polymer(inputText.Trim(), new []{char1, char2});
                polymers.Add(polymer.React());
            }
            
            var result = polymers.OrderBy(i=>i).First();
            return result;
        }

        public int CalculateLength(string inputPath)
        {
            var inputText = File.ReadAllText(inputPath);
            var polymer = new Polymer(inputText.Trim());
            var result = polymer.React();
            return result;
        }
    }

    public class Polymer
    {
        private readonly StringBuilder builder;
        public Polymer(string input, char[] charsToRemove = null)
        {

            Input = charsToRemove == null ? input : string.Join(string.Empty, input.Where(i => !charsToRemove.Contains(i)));
            builder = new StringBuilder(Input);
        }

        public string Input { get; set; }

        public int React()
        {
            IterateOverUnits();
            
            return builder.Length;
        }

        private void IterateOverUnits()
        {
            for (int i = 0; i < builder.Length; i++)
            {
                var current = builder[i];
                if (i < builder.Length - 1)
                {
                    var next = builder[i+1];
                    if (Math.Abs(next - current) == 32)
                    {
                        builder.Remove(i, 2);
                        i = -1;
                    }
                }
            }
        }
    }
}
