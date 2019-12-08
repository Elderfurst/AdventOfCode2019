using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day5
    {
        private static readonly int[] Inputs = File.ReadAllText(@"inputs/Day5.txt").Split(',').Select(int.Parse).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var diagnosticInputs = new List<int>
            {
                1
            };

            var computer = new IntCodeComputer(Inputs)
            {
                DiagnosticInputs = diagnosticInputs
            };

            var output = 0;

            while (output != -1)
            {
                output = computer.CalculateIntCode();
                Console.WriteLine(output);
            }
        }

        private void PartTwo()
        {
            var diagnosticInputs = new List<int>
            {
                5
            };

            var computer = new IntCodeComputer(Inputs)
            {
                DiagnosticInputs = diagnosticInputs
            };

            Console.WriteLine(computer.CalculateIntCode());
        }
    }
}
