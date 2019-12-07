using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntCodeComputer
    {
        public int[] Inputs { get; set; }
        public int Counter { get; set; }
        public List<int> DiagnosticInputs { get; set; }
        public int DiagnosticCounter { get; set; }

        public IntCodeComputer(int[] inputs)
        {
            var tempInput = new int[inputs.Length];

            Array.Copy(inputs, 0, tempInput, 0, inputs.Length);

            Inputs = tempInput;
            DiagnosticInputs = new List<int>();
            Counter = 0;
            DiagnosticCounter = 0;
        }

        public int CalculateIntCode()
        {
            while (Counter < Inputs.Length)
            {
                // Convert the first value of the command to an array of integers
                var instruction = Inputs[Counter].ToString().Select(x => x.ToString()).Select(int.Parse).ToArray();

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
                        var firstInstructionAdd = Inputs[Counter + 1];
                        var firstAdd = firstModeAdd == 0 ? Inputs[firstInstructionAdd] : firstInstructionAdd;

                        var secondModeAdd = modes.Count() >= 2 ? modes[1] : 0;
                        var secondInstructionAdd = Inputs[Counter + 2];
                        var secondAdd = secondModeAdd == 0 ? Inputs[secondInstructionAdd] : secondInstructionAdd;

                        var outputInstructionAdd = Inputs[Counter + 3];
                        Inputs[outputInstructionAdd] = firstAdd + secondAdd;

                        Counter += 4;

                        break;
                    case 2:
                        var firstModeMult = modes.Any() ? modes[0] : 0;
                        var firstInstructionMult = Inputs[Counter + 1];
                        var firstMult = firstModeMult == 0 ? Inputs[firstInstructionMult] : firstInstructionMult;

                        var secondModeMult = modes.Count() >= 2 ? modes[1] : 0;
                        var secondInstructionMult = Inputs[Counter + 2];
                        var secondMult = secondModeMult == 0 ? Inputs[secondInstructionMult] : secondInstructionMult;

                        var outputInstructionMult = Inputs[Counter + 3];
                        Inputs[outputInstructionMult] = firstMult * secondMult;

                        Counter += 4;

                        break;
                    case 3:
                        var outputInstructionInput = Inputs[Counter + 1];
                        Inputs[outputInstructionInput] = DiagnosticInputs[DiagnosticCounter];

                        Counter += 2;
                        DiagnosticCounter++;

                        break;
                    case 4:
                        var mode = modes.Any() ? modes[0] : 0;
                        var outputInstruction = Inputs[Counter + 1];
                        var outputInstructionOutput = mode == 0 ? Inputs[Counter + 1] : Counter + 1;

                        Counter += 2;

                        return Inputs[outputInstructionOutput];
                    case 5:
                        var firstModeTrue = modes.Any() ? modes[0] : 0;
                        var firstParameterTrue = Inputs[Counter + 1];
                        var firstTrue = firstModeTrue == 0 ? Inputs[firstParameterTrue] : firstParameterTrue;

                        var secondModeTrue = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterTrue = Inputs[Counter + 2];
                        var secondTrue = secondModeTrue == 0 ? Inputs[secondParameterTrue] : secondParameterTrue;

                        if (firstTrue != 0)
                        {
                            Counter = secondTrue;
                        }
                        else
                        {
                            Counter += 3;
                        }

                        break;
                    case 6:
                        var firstModeFalse = modes.Any() ? modes[0] : 0;
                        var firstParameterFalse = Inputs[Counter + 1];
                        var firstFalse = firstModeFalse == 0 ? Inputs[firstParameterFalse] : firstParameterFalse;

                        var secondModeFalse = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterFalse = Inputs[Counter + 2];
                        var secondFalse = secondModeFalse == 0 ? Inputs[secondParameterFalse] : secondParameterFalse;

                        if (firstFalse == 0)
                        {
                            Counter = secondFalse;
                        }
                        else
                        {
                            Counter += 3;
                        }

                        break;
                    case 7:
                        var firstModeLess = modes.Any() ? modes[0] : 0;
                        var firstParameterLess = Inputs[Counter + 1];
                        var firstLess = firstModeLess == 0 ? Inputs[firstParameterLess] : firstParameterLess;

                        var secondModeLess = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterLess = Inputs[Counter + 2];
                        var secondLess = secondModeLess == 0 ? Inputs[secondParameterLess] : secondParameterLess;

                        var thirdParameterLess = Inputs[Counter + 3];

                        if (firstLess < secondLess)
                        {
                            Inputs[thirdParameterLess] = 1;
                        }
                        else
                        {
                            Inputs[thirdParameterLess] = 0;
                        }

                        Counter += 4;

                        break;
                    case 8:
                        var firstModeEqual = modes.Any() ? modes[0] : 0;
                        var firstParameterEqual = Inputs[Counter + 1];
                        var firstEqual = firstModeEqual == 0 ? Inputs[firstParameterEqual] : firstParameterEqual;

                        var secondModeEqual = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterEqual = Inputs[Counter + 2];
                        var secondEqual = secondModeEqual == 0 ? Inputs[secondParameterEqual] : secondParameterEqual;

                        var thirdParameterEqual = Inputs[Counter + 3];

                        if (firstEqual == secondEqual)
                        {
                            Inputs[thirdParameterEqual] = 1;
                        }
                        else
                        {
                            Inputs[thirdParameterEqual] = 0;
                        }

                        Counter += 4;

                        break;
                    case 99:
                        Counter = int.MaxValue;
                        return -1;
                    default:
                        Counter = int.MaxValue;
                        return -1;
                }
            }

            return -2;
        }
    }
}
