using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using static System.String;

FirstWay();

static void FirstWay()
{
    var watch = System.Diagnostics.Stopwatch.StartNew();

    var validPassportCount = 0;
    var passports =
        File.ReadAllText("../../../input.txt")
            .Split("\r\n\r\n")
            .Select(p => p.Replace("\n", "")
                                .Replace("\r", " "))
            .Select(line => line.Split(" ")).ToArray();

    foreach (var passport in passports)
    {
        if (passport.Length == 8)
        {
            if (AutomaticValidation(passport)) validPassportCount++;
        }
        else if (passport.Length == 7)
        {
            var valid = true;
            foreach (var s in passport)
            {
                if (s.Contains("cid")) valid = false;
            }

            if (valid && AutomaticValidation(passport)) validPassportCount++;
        }
    }

    Console.WriteLine(validPassportCount);
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}

static bool AutomaticValidation(string[] passport)
{
    foreach (var prop in passport)
    {
        var value = prop.Split(":")[1];
        switch (prop.Split(":")[0])
        {
            case "byr" when !(int.Parse(value) is >= 1920 and <= 2002):
            case "iyr" when !(int.Parse(value) is >= 2010 and <= 2020):
            case "eyr" when !(int.Parse(value) is >= 2020 and <= 2030):
            case "hgt" when !new Regex("(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)$").IsMatch(value):
            case "hcl" when !new Regex("#([0-9]|[a-f]){6}").IsMatch(value):
            case "ecl" when !new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value):
            case "pid" when Join("", value.Where(char.IsDigit)).Length != 9:
                return false;
        }
    }
    return true;
}