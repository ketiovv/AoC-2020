using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

FirstWay();


static void FirstWay()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();

    var data =
        File.ReadAllLines("../../../input.txt")
            .Select(line => new List<string>(
                line.Select(ch => ch.ToString())))
            .ToList();

    long trees11 = CountTrees(data, 1, 1);
    long trees31 = CountTrees(data, 3, 1);
    long trees51 = CountTrees(data, 5, 1);
    long trees71 = CountTrees(data, 7, 1);
    long trees12 = CountTrees(data, 1, 2);
    long result = trees11 * trees31 * trees51 * trees71 * trees12;
    Console.WriteLine($"{trees11} * {trees31} * {trees51} * {trees71} * {trees12} = {result}");

    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}

static int CountTrees(List<List<string>> data, int stepRight, int stepDown)
{
    var stepsDown = data.Count / stepDown;
    var neededLength = (stepsDown * stepRight) + 1;
    var sequenceLength = data[0].Count;

    while (data[0].Count <= neededLength)
    {
        foreach (var line in data)
        {
            line.AddRange(line.GetRange(0, sequenceLength));
        }
    }

    var x = 0;
    var y = 0;
    var treeCount = 0;

    for (var i = 0; i < stepsDown; i++)
    {
        if (data[y][x] == "#") treeCount++;
        x += stepRight;
        y += stepDown;
    }

    return treeCount;
}