using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2
    {
        private static readonly int[] _inputs = File.ReadAllText(@"Inputs/Day2.txt").Split(',').Select(int.Parse).ToArray();
        public void Run()
        {
            Console.WriteLine(PartOne(12, 2));
            PartTwo();
        }

        private int PartOne(int noun, int verb)
        {
            var inputs = new int[_inputs.Length];

            Array.Copy(_inputs, 0, inputs, 0, _inputs.Length);

            inputs[1] = noun;
            inputs[2] = verb;

            var computer = new IntCodeComputer(inputs);

            computer.CalculateIntCode();

            return computer.Inputs[0];
        }

        private void PartTwo()
        {
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var result = PartOne(i, j);

                    if (result == 19690720)
                    {
                        Console.WriteLine(100 * i + j);
                    }
                }
            }
        }
    }
}
