using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day7
    {
        private static readonly int[] Inputs = File.ReadAllText(@"inputs/Day7.txt").Split(',').Select(int.Parse).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            var numbers = new int[] { 0, 1, 2, 3, 4 };

            var permutations = new List<int[]>();

            HeapsAlgorithm(numbers.Length, numbers, permutations);

            var maxThrust = 0;

            foreach (var permutation in permutations)
            {
                var aDiagnosticInputs = new List<int>
                {
                    permutation[0], 0
                };
                var aComputer = new IntCodeComputer(Inputs)
                {
                    DiagnosticInputs = aDiagnosticInputs
                };

                var aOutput = aComputer.CalculateIntCode();

                var bDiagnosticInputs = new List<int>
                {
                    permutation[1], aOutput
                };
                var bComputer = new IntCodeComputer(Inputs)
                {
                    DiagnosticInputs = bDiagnosticInputs
                };

                var bOutput = bComputer.CalculateIntCode();

                var cDiagnosticInputs = new List<int>
                {
                    permutation[2], bOutput
                };
                var cComputer = new IntCodeComputer(Inputs)
                {
                    DiagnosticInputs = cDiagnosticInputs
                };

                var cOutput = cComputer.CalculateIntCode();

                var dDiagnosticInputs = new List<int>
                {
                    permutation[3], cOutput
                };
                var dComputer = new IntCodeComputer(Inputs)
                {
                    DiagnosticInputs = dDiagnosticInputs
                };

                var dOutput = dComputer.CalculateIntCode();

                var eDiagnosticInputs = new List<int>
                {
                    permutation[4], dOutput
                };
                var eComputer = new IntCodeComputer(Inputs)
                {
                    DiagnosticInputs = eDiagnosticInputs
                };

                var eOutput = eComputer.CalculateIntCode();

                if (eOutput > maxThrust)
                {
                    maxThrust = eOutput;
                }
            }

            Console.WriteLine(maxThrust);
        }

        private void PartTwo()
        {
            var numbers = new int[] { 5, 6, 7, 8, 9 };

            var permutations = new List<int[]>();

            HeapsAlgorithm(numbers.Length, numbers, permutations);

            var maxThrust = 0;

            foreach (var permutation in permutations)
            {
                var aComputer = new IntCodeComputer(Inputs);

                var bComputer = new IntCodeComputer(Inputs);

                var cComputer = new IntCodeComputer(Inputs);

                var dComputer = new IntCodeComputer(Inputs);

                var eComputer = new IntCodeComputer(Inputs);

                var permMaxThrust = 0;

                var stayLooping = true;

                while (stayLooping)
                {
                    if (!aComputer.DiagnosticInputs.Any())
                    {
                        aComputer.DiagnosticInputs = new List<int>
                        {
                            permutation[0], 0
                        };
                    }
                    else
                    {
                        aComputer.DiagnosticInputs.Add(permMaxThrust);
                    }

                    var aOutput = aComputer.CalculateIntCode();

                    if (aOutput == -1)
                    {
                        stayLooping = false;
                        break;
                    }

                    if (!bComputer.DiagnosticInputs.Any())
                    {
                        bComputer.DiagnosticInputs = new List<int>
                        {
                            permutation[1], aOutput
                        };
                    }
                    else
                    {
                        bComputer.DiagnosticInputs.Add(aOutput);
                    }

                    var bOutput = bComputer.CalculateIntCode();

                    if (bOutput == -1)
                    {
                        stayLooping = false;
                        break;
                    }

                    if (!cComputer.DiagnosticInputs.Any())
                    {
                        cComputer.DiagnosticInputs = new List<int>
                        {
                            permutation[2], bOutput
                        };
                    }
                    else
                    {
                        cComputer.DiagnosticInputs.Add(bOutput);
                    }

                    var cOutput = cComputer.CalculateIntCode();

                    if (cOutput == -1)
                    {
                        stayLooping = false;
                        break;
                    }

                    if (!dComputer.DiagnosticInputs.Any())
                    {
                        dComputer.DiagnosticInputs = new List<int>
                        {
                            permutation[3], cOutput
                        };
                    }
                    else
                    {
                        dComputer.DiagnosticInputs.Add(cOutput);
                    }

                    var dOutput = dComputer.CalculateIntCode();

                    if (dOutput == -1)
                    {
                        stayLooping = false;
                        break;
                    }

                    if (!eComputer.DiagnosticInputs.Any())
                    {
                        eComputer.DiagnosticInputs = new List<int>
                        {
                            permutation[4], dOutput
                        };
                    }
                    else
                    {
                        eComputer.DiagnosticInputs.Add(dOutput);
                    }

                    var eOutput = eComputer.CalculateIntCode();

                    if (eOutput == -1)
                    {
                        stayLooping = false;
                        break;
                    }

                    permMaxThrust = eOutput;
                }

                if (permMaxThrust > maxThrust)
                {
                    maxThrust = permMaxThrust;
                }
            }

            Console.WriteLine(maxThrust);
        }

        private void HeapsAlgorithm(int count, int[] values, List<int[]> permutations)
        {
            if (count == 1)
            {
                var valuesCopy = new int[values.Length];

                Array.Copy(values, 0, valuesCopy, 0, values.Length);

                permutations.Add(valuesCopy);
            }

            for (var i = 0; i < count; i++)
            {
                HeapsAlgorithm(count - 1, values, permutations);

                if (count % 2 == 1)
                {
                    var temp = values[0];
                    values[0] = values[count - 1];
                    values[count - 1] = temp;
                }
                else
                {
                    var temp = values[i];
                    values[i] = values[count - 1];
                    values[count - 1] = temp;
                }
            }
        }
    }
}
