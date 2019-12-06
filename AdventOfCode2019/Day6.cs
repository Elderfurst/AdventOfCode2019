using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day6
    {
        private static readonly List<Orbit> _orbits = File.ReadAllText(@"Inputs/Day6.txt").Split("\r\n").Select(x => new Orbit(x.Split(')')[0], x.Split(')')[1])).ToList();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var temp = _orbits;
        }

        private void PartTwo()
        {

        }
    }

    class Orbit
    {
        public string Orbitee { get; set; }
        public string Orbiter { get; set; }

        public Orbit(string orbitee, string orbiter)
        {
            Orbitee = orbitee;
            Orbiter = orbiter;
        }
    }
}
