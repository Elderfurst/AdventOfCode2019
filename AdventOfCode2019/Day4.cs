using System;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day4
    {
        private readonly int Start = 245318;
        private readonly int End = 765747;
        public void Run()
        {
            PartOneAndTwo();
        }

        private void PartOneAndTwo()
        {
            var totalValidPasswords = 0;
            var totalValidPasswordsStrict = 0;

            for (var i = Start; i <= End; i++)
            {
                if (ValidateNumber(i))
                {
                    totalValidPasswords++;
                }
                if(ValidateNumberStrict(i))
                {
                    totalValidPasswordsStrict++;
                }
            }

            Console.WriteLine(totalValidPasswords);
            Console.WriteLine(totalValidPasswordsStrict);
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
