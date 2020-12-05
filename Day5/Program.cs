using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day5
{
    public record Seat(int X, int Y)
    {
        public int Id { get; set; } = Y * 8 + X;
    };
    class Program
    {
        private static void Main(string[] args)
        {
            FirstWay();
            SecondWay();
        }

        private static void FirstWay()
        {
            var watch = Stopwatch.StartNew();
            var input = File.ReadAllLines("../../../input.txt");
            var highestSeatId = 0;
            var allSeats = new List<Seat>();

            for (var i = 2; i < 122; i++)
            {
                for (var j = 0; j < 8; j++) allSeats.Add(new Seat(j, i));
            }

            foreach (var line in input)
            {
                var seat = GetSeat(line);
                highestSeatId = Math.Max(highestSeatId, seat.Id);
                allSeats.Remove(seat);
            }

            Console.WriteLine($"highest seat id = {highestSeatId}");

            foreach (var seat in allSeats)
            {
                Console.WriteLine($"col: {seat.Y} row:{seat.X} id:{seat.Id}");
            }

            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }
        private static Seat GetSeat(string seat) =>
            new(
                GetColumn(seat.Substring(7, 3)),
                GetRow(seat.Substring(0, 7)));
        private static int GetRow(string rowLetters)
        {
            var min = 0; var max = 127;
            foreach (var character in rowLetters)
            {
                var count = max - min + 1;
                if (character == 'F') max -= count / 2;
                else if (character == 'B') min += count / 2;
            }
            return min;
        }
        private static int GetColumn(string columnLetters)
        {
            var min = 0; var max = 7;
            foreach (var character in columnLetters)
            {
                var count = max - min + 1;
                if (character == 'L') max -= count / 2;
                else if (character == 'R') min += count / 2;
            }
            return min;
        }

        private static void SecondWay()
        {
            var watch = Stopwatch.StartNew();

            var ids = new HashSet<int>(File.ReadAllLines("../../../input.txt")
                .Select(line =>
                    // line is full seat code
                    // ^3 - third element(in this case - chars) from end(^ - 'hat operator')
                    // [..x] - describe range, which we want to cut. in this case
                    // we want to get all elements from start to <third from end>

                    // next we replace letters for numbers and make from it binary number
                    // from binary number we get normal number (by convert)
                    // in first case we multiply by 8, becouse formula is row * 8 + column
                    Convert.ToInt32(line[..^3]
                        .Replace('F', '0')
                        .Replace('B', '1'), 2) * 8
                    +
                    // in this case we want to get all elements from <third from end> to end
                    Convert.ToInt32(line[^3..]
                        .Replace('L', '0')
                        .Replace('R', '1'), 2)));

            Console.WriteLine($"Part A: {ids.Max()}");
            // here we create range by enumberable from minimum id to maximum id,
            // and search for id which not exist in our hashset
            Console.WriteLine($"Part B: {Enumerable.Range(ids.Min(), ids.Max()).First(v => !ids.Contains(v))}");

            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }
    }
}
