using System;
using System.IO;
using System.Linq;

ThreeForeachesWay();

// usually faster one:)
LinqWay();

static void ThreeForeachesWay()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    var allLinesOfFile = File.ReadAllLines("../../../input.txt");

    var overPartOne = false;
    var overPartTwo = false;

    foreach (var numInString in allLinesOfFile)
    {
        var number = Convert.ToInt32(numInString);

        foreach (var numInString2 in allLinesOfFile)
        {
            var number2 = Convert.ToInt32(numInString2);

            if (number + number2 == 2020 && !overPartOne)
            {
                //Console.WriteLine($"part one answer, it's: {number} + {number2} = 2020");
                Console.WriteLine($"multiplied: {number} * {number2} = {number * number2}");
                overPartOne = true;
            }

            foreach (var numInString3 in allLinesOfFile)
            {
                var number3 = Convert.ToInt32(numInString3);

                if (number + number2 + number3 == 2020 && !overPartTwo)
                {
                    //Console.WriteLine($"part two answer, it's: {number} + {number2} + {number3} = 2020");
                    Console.WriteLine($"multiplied: {number} * {number2} * {number3} = {number * number2 * number3}");
                    overPartTwo = true;
                }
            }
        }
    }
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}

static void LinqWay()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    var input = File.ReadAllLines("../../../input.txt").Select(int.Parse).ToArray();

    var partOne =
        (from a in input
         from b in input
         where a + b == 2020
         select a * b).FirstOrDefault();

    var partTwo =
        (from a in input
         from b in input
         from c in input
         where a + b + c == 2020
         select a * b * c).FirstOrDefault();

    Console.WriteLine($"part one answer: {partOne}");
    Console.WriteLine($"part two answer: {partTwo}");
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}