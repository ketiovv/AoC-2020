using System;
using System.IO;

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
            Console.WriteLine($"part one answer, it's: {number} + {number2} = 2020");
            Console.WriteLine($"multiplied: {number} * {number2} = {number * number2}");
            overPartOne = true;
        }

        foreach (var numInString3 in allLinesOfFile)
        {
            var number3 = Convert.ToInt32(numInString3);

            if (number + number2 + number3 == 2020 && !overPartTwo)
            {
                Console.WriteLine($"part two answer, it's: {number} + {number2} + {number3} = 2020");
                Console.WriteLine($"multiplied: {number} * {number2} * {number3} = {number * number2 * number3}");
                overPartTwo = true;
            }
        }
    }
}