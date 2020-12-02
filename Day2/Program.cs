using System;
using System.IO;
using System.Linq;

FirstWay();

static void FirstWay()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines("../../../input.txt");

    var p1Valid = 0;
    var p2Valid = 0;

    foreach (var line in lines)
    {
        var splitted = line.Split(':').Select(s => s.Trim()).ToArray();

        var policy = splitted[0];
        var password = splitted[1];

        var rangeOfLetter = policy.Split(" ")[0].Trim();
        var letter = policy.Split(" ")[1].Trim();

        var letterCount = 0;
        var lowest = Convert.ToInt32(rangeOfLetter.Split("-")[0].Trim());
        var highest = Convert.ToInt32(rangeOfLetter.Split("-")[1].Trim());

        var position1 = lowest - 1;
        var position2 = highest - 1;

        // Part 1
        foreach (var ch in password) if (ch.ToString() == letter) letterCount++;
        if (letterCount >= lowest && letterCount <= highest) p1Valid++;

        // Part 2
        if ((password[position1].ToString() == letter && password[position2].ToString() != letter) ||
            (password[position1].ToString() != letter && password[position2].ToString() == letter)) p2Valid++;
    }

    Console.WriteLine($"PART1: valid: {p1Valid}");
    Console.WriteLine($"PART2: valid: {p2Valid}");

    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}