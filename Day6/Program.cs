using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


FirstWay();

static void FirstWay()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    var groups = File.ReadAllText("../../../input.txt")
        .Split("\r\n\r\n")
        .Select(line => line.Split("\r\n").ToList())
        .ToList();

    var partOne = 0;
    var partTwo = 0;

    foreach (var group in groups)
    {
        var yesAnswersQuestionsInGroup = new List<char>();
        foreach (var question in
            group.SelectMany(person =>
            person.Where(question =>
                !yesAnswersQuestionsInGroup.Contains(question)))) yesAnswersQuestionsInGroup.Add(question);

        partOne += yesAnswersQuestionsInGroup.Count;
        partTwo += yesAnswersQuestionsInGroup
            .Select(question => group.Count(person => person.Contains(question)))
            .Count(counter => counter == group.Count);
    }

    Console.WriteLine($"p1:{partOne}");
    Console.WriteLine($"p2:{partTwo}");

    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}