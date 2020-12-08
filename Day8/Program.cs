using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static List<List<string>> input =
            File.ReadAllLines("../../../input.txt").Select(line => line.Split(" ").ToList()).ToList();
        static int counterP1 = 0;

        static void Main(string[] args)
        {
            ValueAfterOneSequence(0);
            Part2();

            static void ValueAfterOneSequence(int indexOfLine)
            {
                var instruction = input[indexOfLine][0];
                var value = int.Parse(input[indexOfLine][1]);
                if (instruction == "nop")
                {
                    input[indexOfLine][0] = "none";
                    ValueAfterOneSequence(indexOfLine + 1);
                }
                else if (instruction == "acc")
                {
                    input[indexOfLine][0] = "none";
                    counterP1 += value;
                    ValueAfterOneSequence(indexOfLine + 1);
                }
                else if (instruction == "jmp")
                {
                    input[indexOfLine][0] = "none";
                    ValueAfterOneSequence(indexOfLine + value);
                }
                else if (instruction == "none")
                {
                    Console.WriteLine($"p1:{counterP1}");
                }
            }

            static void Part2()
            {
                var lines = File.ReadAllLines("../../../input.txt");
                var countP2 = 0;

                for (var i = 0; i < lines.Length; i++)
                {
                    countP2 = 0;
                    var indexOfLine = 0;
                    var oldLine = lines[i];
                    var passed = new bool[lines.Length];

                    if (lines[i].Contains("nop"))
                        lines[i] = lines[i].Replace("nop", "jmp");
                    else if (lines[i].Contains("jmp"))
                        lines[i] = lines[i].Replace("jmp", "nop");
                    else continue;

                    var checkNext = false;

                    while (indexOfLine < lines.Length)
                    {
                        if (indexOfLine < lines.Length && passed[indexOfLine])
                        {
                            checkNext = true;
                            break;
                        }
                        passed[indexOfLine] = true;

                        var instruction = lines[indexOfLine].Split(" ")[0];
                        var value = int.Parse(lines[indexOfLine].Split(" ")[1]);

                        if (instruction == "acc")
                        {
                            countP2 += value;
                            indexOfLine++;
                        }
                        else if (instruction == "jmp") indexOfLine += value;
                        else if (instruction == "nop") indexOfLine++;
                    }
                    if (!checkNext) break;
                    lines[i] = oldLine;
                }
                Console.WriteLine($"p2:{countP2}");
            }
        }
    }
}