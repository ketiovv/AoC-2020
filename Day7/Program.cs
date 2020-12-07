using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static Dictionary<string, string> input =
            File.ReadAllLines("../../../input.txt")
                .Select(rule => rule.Trim('.')
                    .Replace(" bags", "")
                    .Replace(" bag", ""))
                .ToDictionary(
                    rule => rule.Split(" contain ")[0],
                    rule => rule.Split(" contain ")[1]);

        public static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine($"p1:{input.Keys.Count(Part1)}");
            Console.WriteLine($"p2:{Part2("shiny gold")}");
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }

        private static bool Part1(string bagColor) =>
            input[bagColor].Contains("shiny gold") ||
            input[bagColor].Split(", ")
                .Where(bag => bag != "no other")
                .Any(bag => Part1(bag.Substring(2)));

        private static int Part2(string bagColor)
        {
            var counter = 0;
            foreach (var bag in input[bagColor].Split(", "))
            {
                if (!bag.Equals("no other"))
                {
                    var numberOfThisBag = Convert.ToInt32(bag.Substring(0, 1));
                    counter += numberOfThisBag + numberOfThisBag * Part2(bag.Substring(2));
                }
                else
                    break;
            }
            return counter;
        }

    }
}