using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    class Day8
    {
        private static readonly int[] _inputs = File.ReadAllText(@"Inputs/Day8.txt").Select(x => int.Parse(x.ToString())).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var width = 25;
            var height = 6;

            var layers = new List<int[,]>();

            var numberOfLayers = _inputs.Length / (width * height);

            var pointer = 0;

            for (var i = 0; i < numberOfLayers; i++)
            {
                var newLayer = new int[height, width];

                for (var j = 0; j < height; j++)
                {
                    for (var k = 0; k < width; k++)
                    {
                        newLayer[j, k] = _inputs[pointer];
                        pointer++;
                    }
                }

                layers.Add(newLayer);
            }

            var leastZeroes = new int[height, width];

            var totalZeroes = int.MaxValue;

            foreach (var layer in layers)
            {
                var zeroes = CountNumbers(layer, 0);

                if (zeroes < totalZeroes)
                {
                    totalZeroes = zeroes;
                    leastZeroes = layer;
                }
            }

            Console.WriteLine(CountNumbers(leastZeroes, 1) * CountNumbers(leastZeroes, 2));
        }

        private void PartTwo()
        {

        }

        private int CountNumbers(int[,] layer, int number)
        {
            var numbers = 0;

            foreach (var entry in layer)
            {
                if (entry == number)
                {
                    numbers++;
                }
            }

            return numbers;
        }
    }
}
