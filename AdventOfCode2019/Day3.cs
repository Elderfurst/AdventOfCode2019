using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day3
    {
        private static readonly string[][] Inputs = File.ReadAllLines(@"Inputs/Day3.txt").Select(x => x.Split(',')).ToArray();
        public void Run()
        {
            PartOneAndTwo();
        }

        private void PartOneAndTwo()
        {
            var coordinateLists = new List<List<Coordinate>>();

            foreach (var input in Inputs)
            {
                var inputList = new List<Coordinate>();

                var currentX = 0;
                var currentY = 0;
                var steps = 0;

                foreach (var instruction in input)
                {
                    var direction = instruction.Substring(0, 1);
                    var distance = int.Parse(instruction.Substring(1));

                    for (var i = 0; i < distance; i++)
                    {
                        steps++;

                        switch (direction)
                        {
                            case "U":
                                currentY++;
                                break;
                            case "D":
                                currentY--;
                                break;
                            case "L":
                                currentX--;
                                break;
                            case "R":
                                currentX++;
                                break;
                        }

                        inputList.Add(new Coordinate(currentX, currentY, steps));
                    }

                }

                coordinateLists.Add(inputList);
            }

            var commonality = coordinateLists[0].Intersect(coordinateLists[1]).ToList();

            var closestDistance = 10000000;

            foreach (var commonCoordinate in commonality)
            {
                var distance = Math.Abs(commonCoordinate.X - 0) + Math.Abs(commonCoordinate.Y - 0);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }

            // Part One Solution
            Console.WriteLine(closestDistance);

            var minimumSteps = 10000000;

            foreach (var commonCoordinate in commonality)
            {
                // This works by pulling first because the first time a coordinate is reached will be
                // the smallest number of steps to reach that specific coordinate for that specific wire
                var firstWire = coordinateLists[0].First(x => x.Equals(commonCoordinate));
                var secondWire = coordinateLists[1].First(x => x.Equals(commonCoordinate));

                if (firstWire.Steps + secondWire.Steps < minimumSteps)
                {
                    minimumSteps = (firstWire.Steps + secondWire.Steps);
                }
            }

            // Part Two Solution
            Console.WriteLine(minimumSteps);
        }
    }

    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Steps { get; set; }

        public Coordinate(int x, int y, int steps)
        {
            X = x;
            Y = y;
            Steps = steps;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            var parsedObj = (Coordinate) obj;

            return X == parsedObj.X && Y == parsedObj.Y;
        }

        public override int GetHashCode()
        {
            return X + Y;
        }
    }
}
