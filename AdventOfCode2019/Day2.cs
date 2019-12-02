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

            for (var i = 0; i < inputs.Length; i += 4)
            {
                var firstLocation = inputs[i + 1];
                var secondLocation = inputs[i + 2];
                var outputLocation = inputs[i + 3];

                int first;
                int second;

                switch (inputs[i])
                {
                    case 1:
                        first = inputs[firstLocation];
                        second = inputs[secondLocation];
                        inputs[outputLocation] = first + second;
                        break;
                    case 2:
                        first = inputs[firstLocation];
                        second = inputs[secondLocation];
                        inputs[outputLocation] = first * second;
                        break;
                    case 99:
                        return inputs[0];
                    default:
                        Console.WriteLine("Something went wrong.");
                        Environment.Exit(1);
                        break;
                }
            }

            return -1;
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
