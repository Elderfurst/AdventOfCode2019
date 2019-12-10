using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    class Day9
    {
        private static readonly long[] Inputs = File.ReadAllText(@"inputs/Day9.txt").Split(',').Select(long.Parse).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var computer = new IntCodeComputer(Inputs)
            {
                DiagnosticInputs = new List<long>
                {
                    1
                }
            };

            var output = 0L;

            while (output != -1)
            {
                output = computer.CalculateIntCode();

                Console.WriteLine(output);
            }
        }

        private void PartTwo()
        {

        }
    }
}
