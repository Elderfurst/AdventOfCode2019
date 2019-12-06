using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day5
    {
        private static readonly int[] _inputs = File.ReadAllText(@"inputs/Day5.txt").Split(',').Select(int.Parse).ToArray();
        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            CalculateIntCode(1);
        }

        private void PartTwo()
        {
            CalculateIntCode(5);
        }

        private void CalculateIntCode(int diagnosticInput)
        {
            var inputs = new int[_inputs.Length];

            Array.Copy(_inputs, 0, inputs, 0, _inputs.Length);

            var counter = 0;

            while (counter < inputs.Length)
            {
                // Convert the first value of the command to an array of integers
                var instruction = inputs[counter].ToString().Select(x => x.ToString()).Select(int.Parse).ToArray();

                var opCode = instruction[^1];

                // Subtract 1 to transition to 0 index and then 2 for the opcode positions
                var modeCount = instruction.Length - 3;

                var modes = new List<int>();

                for (var i = modeCount; i >= 0; i--)
                {
                    modes.Add(instruction[i]);
                }

                switch (opCode)
                {
                    case 1:
                        var firstModeAdd = modes.Any() ? modes[0] : 0;
                        var firstInstructionAdd = inputs[counter + 1];
                        var firstAdd = firstModeAdd == 0 ? inputs[firstInstructionAdd] : firstInstructionAdd;

                        var secondModeAdd = modes.Count() >= 2 ? modes[1] : 0;
                        var secondInstructionAdd = inputs[counter + 2];
                        var secondAdd = secondModeAdd == 0 ? inputs[secondInstructionAdd] : secondInstructionAdd;

                        var outputInstructionAdd = inputs[counter + 3];
                        inputs[outputInstructionAdd] = firstAdd + secondAdd;

                        counter += 4;

                        break;
                    case 2:
                        var firstModeMult = modes.Any() ? modes[0] : 0;
                        var firstInstructionMult = inputs[counter + 1];
                        var firstMult = firstModeMult == 0 ? inputs[firstInstructionMult] : firstInstructionMult;

                        var secondModeMult = modes.Count() >= 2 ? modes[1] : 0;
                        var secondInstructionMult = inputs[counter + 2];
                        var secondMult = secondModeMult == 0 ? inputs[secondInstructionMult] : secondInstructionMult;

                        var outputInstructionMult = inputs[counter + 3];
                        inputs[outputInstructionMult] = firstMult * secondMult;

                        counter += 4;

                        break;
                    case 3:
                        var outputInstructionInput = inputs[counter + 1];
                        inputs[outputInstructionInput] = diagnosticInput;

                        counter += 2;

                        break;
                    case 4:
                        var mode = modes.Any() ? modes[0] : 0;
                        var outputInstruction = inputs[counter + 1];
                        var outputInstructionOutput = mode == 0 ? inputs[counter + 1] : counter + 1;

                        Console.WriteLine(inputs[outputInstructionOutput]);
                        counter += 2;

                        break;
                    case 5:
                        var firstModeTrue = modes.Any() ? modes[0] : 0;
                        var firstParameterTrue = inputs[counter + 1];
                        var firstTrue = firstModeTrue == 0 ? inputs[firstParameterTrue] : firstParameterTrue;

                        var secondModeTrue = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterTrue = inputs[counter + 2];
                        var secondTrue = secondModeTrue == 0 ? inputs[secondParameterTrue] : secondParameterTrue;
                        
                        if (firstTrue != 0)
                        {
                            counter = secondTrue;
                        }
                        else
                        {
                            counter += 3;
                        }

                        break;
                    case 6:
                        var firstModeFalse = modes.Any() ? modes[0] : 0;
                        var firstParameterFalse = inputs[counter + 1];
                        var firstFalse = firstModeFalse == 0 ? inputs[firstParameterFalse] : firstParameterFalse;

                        var secondModeFalse = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterFalse = inputs[counter + 2];
                        var secondFalse = secondModeFalse == 0 ? inputs[secondParameterFalse] : secondParameterFalse;

                        if (firstFalse == 0)
                        {
                            counter = secondFalse;
                        }
                        else
                        {
                            counter += 3;
                        }

                        break;
                    case 7:
                        var firstModeLess = modes.Any() ? modes[0] : 0;
                        var firstParameterLess = inputs[counter + 1];
                        var firstLess = firstModeLess == 0 ? inputs[firstParameterLess] : firstParameterLess;

                        var secondModeLess = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterLess = inputs[counter + 2];
                        var secondLess = secondModeLess == 0 ? inputs[secondParameterLess] : secondParameterLess;

                        var thirdParameterLess = inputs[counter + 3];

                        if (firstLess < secondLess)
                        {
                            inputs[thirdParameterLess] = 1;
                        }
                        else
                        {
                            inputs[thirdParameterLess] = 0;
                        }

                        counter += 4;

                        break;
                    case 8:
                        var firstModeEqual = modes.Any() ? modes[0] : 0;
                        var firstParameterEqual = inputs[counter + 1];
                        var firstEqual = firstModeEqual == 0 ? inputs[firstParameterEqual] : firstParameterEqual;

                        var secondModeEqual = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterEqual = inputs[counter + 2];
                        var secondEqual = secondModeEqual == 0 ? inputs[secondParameterEqual] : secondParameterEqual;

                        var thirdParameterEqual = inputs[counter + 3];

                        if (firstEqual == secondEqual)
                        {
                            inputs[thirdParameterEqual] = 1;
                        }
                        else
                        {
                            inputs[thirdParameterEqual] = 0;
                        }

                        counter += 4;

                        break;
                    case 99:
                        Console.WriteLine(inputs[0]);
                        break;
                    default:
                        counter = inputs.Length + 1;
                        break;
                }
            }
        }
    }
}
