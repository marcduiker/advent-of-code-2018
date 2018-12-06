using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day04
    {
        public IEnumerable<GuardResult> GetGuardWhichSleepsMost(string inputPath, out string message)
        {
            var inputLines = File.ReadAllLines(inputPath);
            var result = GetGuardWhichSleepsMost(inputLines, out message);
            return result;
        }

        public IEnumerable<GuardResult> GetGuardWhichSleepsMost(IEnumerable<string> inputLines, out string message)
        {
            message = string.Empty;
            var orderedGuardLines = inputLines
                .Select(line => new GuardLine(line))
                .OrderBy(g => g.DateTime).ToList();

            var guardLinesWithIds = UpdateGuardLinesWithId(orderedGuardLines);

            var guardLinesFilled = FillSleepGapsWithGuardLines(guardLinesWithIds).
                Where(g => g.IsSleeping);
          
            var groupedByMinute = guardLinesFilled.GroupBy(g => g.Minute);
            var results = new List<GuardResult>();
            foreach (var group in groupedByMinute)
            {
                var groupById = group.GroupBy(g => g.Id);
                foreach (var idGroup in groupById)
                {
                    var sum = idGroup.Count(g => g.IsSleeping);
                    var guardResult = new GuardResult(idGroup.Key, sum, group.Key);
                    message += guardResult.ToString() + Environment.NewLine;
                    results.Add(guardResult);
                }
            }

            message = string.Join(Environment.NewLine, results.OrderByDescending(g => g.Count).Select(g => g.ToString()));

            return results.OrderByDescending(g=>g.Count);
        }

        public List<GuardLine> FillSleepGapsWithGuardLines(List<GuardLine> orderedGuardLines)
        {
            var guardLinesWithSleepMinutes = new List<GuardLine>();
            for (int i = 0; i < orderedGuardLines.Count(); i++)
            {
                GuardLine next = null;
                var current = orderedGuardLines[i];
                guardLinesWithSleepMinutes.Add(current);
                if (i < orderedGuardLines.Count - 1)
                {
                    next = orderedGuardLines[i + 1];
                    if (next.Id == current.Id && current.IsSleeping && !next.IsSleeping &&
                        next.DateTime > current.DateTime + new TimeSpan(0,0,1,0))
                    {
                        var minutes = (next.DateTime - current.DateTime).Minutes;
                        for (int m = 1; m < minutes ; m++)
                        {
                            var oneMinute = new TimeSpan(0, 0, m, 0);
                            var sleepingGuardLine = new GuardLine(current.Id, current.IsSleeping,
                                current.DateTime.Add(oneMinute));
                            guardLinesWithSleepMinutes.Add(sleepingGuardLine);
                        }
                    }
                }
            }

            return guardLinesWithSleepMinutes;
        }

        private List<GuardLine> UpdateGuardLinesWithId(IEnumerable<GuardLine> guardLines)
        {
            var guardsWithIds = new List<GuardLine>();

            var currentGuardId = string.Empty;
            foreach (var guardLine in guardLines)
            {
                if (!string.IsNullOrEmpty(guardLine.Id))
                {
                    currentGuardId = guardLine.Id;
                }

                guardsWithIds.Add(new GuardLine(currentGuardId, guardLine.IsSleeping, guardLine.DateTime));
            }

            return guardsWithIds;
        }

        
    }

    public class GuardResult
    {
        public GuardResult(string id, int count, int minute)
        {
            Id = id;
            Count = count;
            Minute = minute;
        }

        public string Id { get; set; }
        public int Count { get; set; }
        public int Minute { get; set; }

        public override string ToString()
        {
            return $"Count: {Count} Id: {Id} Minute: {Minute}";
        }
    }

    public class GuardLine
    {
        public GuardLine(string inputLine)
        {
            Id = ExtractId(inputLine);
            DateTime = ExtractDateFromInput(inputLine);
            IsSleeping = Sleeping(inputLine);
        }

        public GuardLine(string id, bool sleeping, DateTime dateTime)
        {
            Id = id;
            IsSleeping = sleeping;
            DateTime = dateTime;
        }

        public string Id { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsSleeping { get; set; }

        public int Minute => DateTime.Minute;

        private static DateTime ExtractDateFromInput(string inputLine)
        {
            // [1518-05-06 00:51]
            var dateString = inputLine.Split(']')[0].Substring(1);

            return DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        private static string ExtractId(string inputLine)
        {
            //[1518-09-02 23:58] Guard #733 begins shift
            string id= string.Empty;
            
            if (inputLine.Contains('#'))
            {
                id = inputLine.Split('#')[1].Split(' ')[0];
            }

            return id;
        }

        private static bool Sleeping(string inputLine)
        {
            if (inputLine.Contains("asleep"))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string action;
            if (!string.IsNullOrEmpty(Id))
            {
                return $"[{DateTime:yyyy-MM-dd HH:mm}] Guard #{Id} begins shift";
            }

            if (IsSleeping)
            {
                action = "falls asleep";
            }
            else
            {
                action = "begins/wakes up";
            }

            return $"[{DateTime:yyyy-MM-dd HH:mm}] #{Id} {action}";
        }
    }
}
