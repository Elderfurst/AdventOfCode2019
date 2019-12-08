using System.IO;
using System.Linq;
using System;

namespace AdventOfCode2019
{
    public class Day1
    {
        private static readonly double[] Inputs = File.ReadAllLines(@"Inputs/Day1.txt").Select(double.Parse).ToArray();
        public void Run()
        {
            PartOneAndTwo();
        }

        private void PartOneAndTwo()
        {
            var totalFuelPartOne = 0.0;
            var totalFuelPartTwo = 0.0;

            foreach (var input in Inputs)
            {
                totalFuelPartOne += Math.Floor(input / 3) - 2;
                totalFuelPartTwo += CalculateFuel(input);
            }

            Console.WriteLine(totalFuelPartOne);
            Console.WriteLine(totalFuelPartTwo);
        }

        private double CalculateFuel(double input)
        {
            var fuel = Math.Floor(input / 3) - 2;
            
            if (fuel <= 0)
            {
                return 0;
            }

            return fuel + CalculateFuel(fuel);
        }
    }
}
