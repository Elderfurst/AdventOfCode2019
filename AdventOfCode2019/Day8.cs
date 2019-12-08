using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    class Day8
    {
        private static readonly int[] _inputs = File.ReadAllText(@"Inputs/Day8.txt").Select(x => int.Parse(x.ToString())).ToArray();
        
        private static readonly List<int[,]> Layers = new List<int[,]>();

        private static readonly int Height = 6;

        private static readonly int Width = 25;
        
        public void Run()
        {
            ParseLayers();
            PartOne();
            PartTwo();
        }

        private void ParseLayers()
        {
            var numberOfLayers = _inputs.Length / (Width * Height);

            var pointer = 0;

            for (var i = 0; i < numberOfLayers; i++)
            {
                var newLayer = new int[Height, Width];

                for (var j = 0; j < Height; j++)
                {
                    for (var k = 0; k < Width; k++)
                    {
                        newLayer[j, k] = _inputs[pointer];
                        pointer++;
                    }
                }

                Layers.Add(newLayer);
            }
        }

        private void PartOne()
        {
            var leastZeroes = new int[Height, Width];

            var totalZeroes = int.MaxValue;

            foreach (var layer in Layers)
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
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    // Find the first layer with either a black or a white pixel
                    var pixel = Layers.First(x => x[i, j] == 0 || x[i, j] == 1)[i, j];

                    Console.Write(pixel);
                }

                Console.WriteLine();
            }
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
