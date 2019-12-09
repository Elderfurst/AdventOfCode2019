using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntCodeComputer
    {
        public int[] Inputs { get; set; }
        public int Counter { get; set; }
        public int RelativeBase { get; set; }
        public List<int> DiagnosticInputs { get; set; }
        public int DiagnosticCounter { get; set; }

        public IntCodeComputer(int[] inputs)
        {
            var tempInput = new int[inputs.Length];

            Array.Copy(inputs, 0, tempInput, 0, inputs.Length);

            Inputs = tempInput;
            DiagnosticInputs = new List<int>();
            Counter = 0;
            RelativeBase = 0;
            DiagnosticCounter = 0;
        }

        public int CalculateIntCode()
        {
            while (Counter < Inputs.Length)
            {
                // Convert the first value of the command to an array of integers
                var Parameter = Inputs[Counter].ToString().Select(x => x.ToString()).Select(int.Parse).ToArray();

                var opCode = Parameter[^1];

                // Subtract 1 to transition to 0 index and then 2 for the opcode positions
                var modeCount = Parameter.Length - 3;

                var modes = new List<int>();

                for (var i = modeCount; i >= 0; i--)
                {
                    modes.Add(Parameter[i]);
                }

                switch (opCode)
                {
                    case 1:
                        var firstModeAdd = modes.Any() ? modes[0] : 0;
                        var firstParameterAdd = Inputs[Counter + 1];

                        var firstValueAdd = 0;

                        switch (firstModeAdd)
                        {
                            case 0:
                                firstValueAdd = Inputs[firstParameterAdd];
                                break;
                            case 1:
                                firstValueAdd = firstParameterAdd;
                                break;
                            case 2:
                                firstValueAdd = Inputs[firstParameterAdd + RelativeBase];
                                break;
                        }

                        var firstAdd = firstValueAdd;

                        var secondModeAdd = modes.Count() >= 2 ? modes[1] : 0;
                        var secondParameterAdd = Inputs[Counter + 2];

                        var secondValueAdd = 0;

                        switch (secondModeAdd)
                        {
                            case 0:
                                secondValueAdd = Inputs[secondParameterAdd];
                                break;
                            case 1:
                                secondValueAdd = secondParameterAdd;
                                break;
                            case 2:
                                secondValueAdd = Inputs[secondParameterAdd + RelativeBase];
                                break;
                        }

                        var secondAdd = secondValueAdd;

                        var outputParameterAdd = Inputs[Counter + 3];
                        Inputs[outputParameterAdd] = firstAdd + secondAdd;

                        Counter += 4;

                        break;
                    case 2:
                        var firstModeMult = modes.Any() ? modes[0] : 0;
                        var firstParameterMult = Inputs[Counter + 1];

                        var firstValueMult = 0;

                        switch (firstModeMult)
                        {
                            case 0:
                                firstValueMult = Inputs[firstParameterMult];
                                break;
                            case 1:
                                firstValueMult = firstParameterMult;
                                break;
                            case 2:
                                firstValueMult = Inputs[firstParameterMult + RelativeBase];
                                break;
                        }

                        var firstMult = firstValueMult;

                        var secondModeMult = modes.Count() >= 2 ? modes[1] : 0;
                        var secondParameterMult = Inputs[Counter + 2];

                        var secondValueMult = 0;

                        switch (secondModeMult)
                        {
                            case 0:
                                secondValueMult = Inputs[secondParameterMult];
                                break;
                            case 1:
                                secondValueMult = secondParameterMult;
                                break;
                            case 2:
                                secondValueMult = Inputs[secondParameterMult + RelativeBase];
                                break;
                        }

                        var secondMult = secondValueMult;

                        var outputParameterMult = Inputs[Counter + 3];
                        Inputs[outputParameterMult] = firstMult * secondMult;

                        Counter += 4;

                        break;
                    case 3:
                        var outputParameterInput = Inputs[Counter + 1];
                        Inputs[outputParameterInput] = DiagnosticInputs[DiagnosticCounter];

                        Counter += 2;
                        DiagnosticCounter++;

                        break;
                    case 4:
                        var mode = modes.Any() ? modes[0] : 0;
                        var outputParameter = Counter + 1;

                        var value = 0;

                        switch (mode)
                        {
                            case 0:
                                value = Inputs[outputParameter];
                                break;
                            case 1:
                                value = outputParameter;
                                break;
                            case 2:
                                value = Inputs[outputParameter + RelativeBase];
                                break;
                        }

                        var outputParameterOutput = value;

                        Counter += 2;

                        return Inputs[outputParameterOutput];
                    case 5:
                        var firstModeTrue = modes.Any() ? modes[0] : 0;
                        var firstParameterTrue = Inputs[Counter + 1];

                        var firstValueTrue = 0;

                        switch (firstModeTrue)
                        {
                            case 0:
                                firstValueTrue = Inputs[firstParameterTrue];
                                break;
                            case 1:
                                firstValueTrue = firstParameterTrue;
                                break;
                            case 2:
                                firstValueTrue = Inputs[firstParameterTrue + RelativeBase];
                                break;
                        }

                        var firstTrue = firstValueTrue;

                        var secondModeTrue = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterTrue = Inputs[Counter + 2];

                        var secondValueTrue = 0;

                        switch (secondModeTrue)
                        {
                            case 0:
                                secondValueTrue = Inputs[secondParameterTrue];
                                break;
                            case 1:
                                secondValueTrue = secondParameterTrue;
                                break;
                            case 2:
                                secondValueTrue = Inputs[secondParameterTrue + RelativeBase];
                                break;
                        }

                        var secondTrue = secondValueTrue;

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

                        var firstValueFalse = 0;

                        switch (firstModeFalse)
                        {
                            case 0:
                                firstValueFalse = Inputs[firstParameterFalse];
                                break;
                            case 1:
                                firstValueFalse = firstParameterFalse;
                                break;
                            case 2:
                                firstValueFalse = Inputs[firstParameterFalse + RelativeBase];
                                break;
                        }

                        var firstFalse = firstValueFalse;

                        var secondModeFalse = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterFalse = Inputs[Counter + 2];

                        var secondValueFalse = 0;

                        switch (secondModeFalse)
                        {
                            case 0:
                                secondValueFalse = Inputs[secondParameterFalse];
                                break;
                            case 1:
                                secondValueFalse = secondParameterFalse;
                                break;
                            case 2:
                                secondValueFalse = Inputs[secondParameterFalse + RelativeBase];
                                break;
                        }

                        var secondFalse = secondValueFalse;

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

                        var firstValueLess = 0;

                        switch (firstModeLess)
                        {
                            case 0:
                                firstValueLess = Inputs[firstParameterLess];
                                break;
                            case 1:
                                firstValueLess = firstParameterLess;
                                break;
                            case 2:
                                firstValueLess = Inputs[firstParameterLess + RelativeBase];
                                break;
                        }

                        var firstLess = firstValueLess;

                        var secondModeLess = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterLess = Inputs[Counter + 2];

                        var secondValueLess = 0;

                        switch (secondModeLess)
                        {
                            case 0:
                                secondValueLess = Inputs[secondParameterLess];
                                break;
                            case 1:
                                secondValueLess = secondParameterLess;
                                break;
                            case 2:
                                secondValueLess = Inputs[secondParameterLess + RelativeBase];
                                break;
                        }

                        var secondLess = secondValueLess;

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

                        var firstValueEqual = 0;

                        switch (firstModeEqual)
                        {
                            case 0:
                                firstValueEqual = Inputs[firstParameterEqual];
                                break;
                            case 1:
                                firstValueEqual = firstParameterEqual;
                                break;
                            case 2:
                                firstValueEqual = Inputs[firstParameterEqual + RelativeBase];
                                break;
                        }

                        var firstEqual = firstValueEqual;

                        var secondModeEqual = modes.Count >= 2 ? modes[1] : 0;
                        var secondParameterEqual = Inputs[Counter + 2];

                        var secondValueEqual = 0;

                        switch (secondModeEqual)
                        {
                            case 0:
                                secondValueEqual = Inputs[secondParameterEqual];
                                break;
                            case 1:
                                secondValueEqual = secondParameterEqual;
                                break;
                            case 2:
                                secondValueEqual = Inputs[secondParameterEqual + RelativeBase];
                                break;
                        }

                        var secondEqual = secondValueEqual;

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
                    case 9:
                        var modeBase = modes.Any() ? modes[0] : 0;
                        var parameterBase = Counter + 1;

                        switch (modeBase)
                        {
                            case 0:
                                RelativeBase += Inputs[parameterBase];
                                break;
                            case 1:
                                RelativeBase += parameterBase;
                                break;
                            case 2:
                                RelativeBase += Inputs[parameterBase + RelativeBase];
                                break;
                        }

                        Counter += 2;
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
