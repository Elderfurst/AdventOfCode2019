﻿using System;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day4
    {
        private readonly int _start = 245318;
        private readonly int _end = 765747;
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var totalValidPasswords = 0;

            for (var i = _start; i <= _end; i++)
            {
                if (ValidateNumber(i))
                {
                    totalValidPasswords++;
                }
            }

            Console.WriteLine(totalValidPasswords);
        }

        private void PartTwo()
        {
            var totalValidPasswords = 0;

            for (var i = _start; i <= _end; i++)
            {
                if (ValidateNumberStrict(i))
                {
                    totalValidPasswords++;
                }
            }

            Console.WriteLine(totalValidPasswords);
        }

        private bool ValidateNumber(int number)
        {
            var numberArray = number.ToString().Select(x => x.ToString()).ToArray();

            var sequential = false;

            for (var i = 1; i < numberArray.Length; i++)
            {
                var previousInt = int.Parse(numberArray[i - 1]);
                var currentInt = int.Parse(numberArray[i]);

                if (currentInt < previousInt)
                {
                    return false;
                }

                if (currentInt == previousInt)
                {
                    sequential = true;
                }
            }

            return sequential;
        }

        private bool ValidateNumberStrict(int number)
        {
            var numberArray = number.ToString().Select(x => x.ToString()).ToArray();

            for (var i = 1; i < numberArray.Length; i++)
            {
                var previousInt = int.Parse(numberArray[i - 1]);
                var currentInt = int.Parse(numberArray[i]);

                if (currentInt < previousInt)
                {
                    return false;
                }
            }

            var characterCounts = numberArray.GroupBy(x => x).Select(y => new { Letter = y.Key, Count = y.Count() });

            var doubleCharacters = characterCounts.Where(x => x.Count == 2);

            return doubleCharacters.Any();
        }
    }
}
