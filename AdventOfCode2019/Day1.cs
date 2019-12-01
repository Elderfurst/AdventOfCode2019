using System.IO;
using System.Linq;
using System;

namespace AdventOfCode2019
{
    public class Day1
    {
        private static readonly double[] _inputs = File.ReadAllText(@"Inputs/Day1.txt").Split('\n').Select(double.Parse).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var totalFuel = 0.0;

            foreach (var input in _inputs)
            {
                totalFuel += Math.Floor(input / 3) - 2;
            }

            Console.WriteLine(totalFuel);
        }

        private void PartTwo()
        {
            var totalFuel = 0.0;

            foreach (var input in _inputs)
            {
                totalFuel += CalculateFuel(input);
            }

            Console.WriteLine(totalFuel);
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
