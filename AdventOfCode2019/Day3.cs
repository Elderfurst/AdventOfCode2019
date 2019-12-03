using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day3
    {
        private static readonly string[][] _inputs = File.ReadAllText(@"Inputs/Day3.txt").Split('\n').Select(x => x.Split(',')).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var coordinateLists = new List<List<Coordinate>>();

            foreach (var input in _inputs)
            {
                var inputList = new List<Coordinate>();

                var currentX = 0;
                var currentY = 0;

                foreach (var instruction in input)
                {
                    var direction = instruction.Substring(0, 1);
                    var distance = int.Parse(instruction.Substring(1));

                    for (var i = 0; i < distance; i++)
                    {
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

                        inputList.Add(new Coordinate(currentX, currentY));
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

            Console.WriteLine(closestDistance);
        }

        private void PartTwo()
        {

        }
    }

    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
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
