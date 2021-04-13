using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public static class MachineCodeFormatter
    {

        public static bool[] RemoveExtededZerosInBegin(bool[] matrix)
        {
            var zeros_count = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i]) 
                { 
                    zeros_count = i;
                    break;
                }
            }

            var new_matrix = new bool[matrix.Length - zeros_count];
            for (int i = 0; i < new_matrix.Length; i++)
            {
                new_matrix[i] = matrix[i + zeros_count];
            }

            return new_matrix;
        }
        
        public static bool[] RemoveExtededZerosAtTheEnd(bool[] matrix)
        {
            var zeros_count = 0;
            for (int i = matrix.Length - 1; i >= 0; i--)
            {
                if (matrix[i]) 
                { 
                    zeros_count = i;
                    break;
                }
            }

            Array.Resize(ref matrix, zeros_count + 1);
            return matrix;
        }

        
        public static bool[] GetBoolMatrix(string code) => code.Select(x => x == '1').Reverse().ToArray();
        
        public static string GetNumberFromMatrix(bool[] matrix) => string.Join("", matrix.Select(x => x ? 1 : 0));

        public static (MachineCode, MachineCode) SetCommonDisplace(MachineCode op_1, MachineCode op_2)
        {
            int dislace_1_numb = GetNumberFromBinary(op_1.displace);
            int dislace_2_numb = GetNumberFromBinary(op_2.displace);

            int after_dot_numb_1 = op_1.binary_code.Length - dislace_1_numb;
            int after_dot_numb_2 = op_2.binary_code.Length - dislace_2_numb;
            var delta_dot = Math.Abs(after_dot_numb_1 - after_dot_numb_2);

            if (after_dot_numb_1 != after_dot_numb_2)
                if (after_dot_numb_1 < after_dot_numb_2)
                {
                    op_1 = DisplaceMachineCode(op_1, delta_dot);
                }
                else
                {
                    op_2 = DisplaceMachineCode(op_2, delta_dot);
                }

            return (op_1, op_2);
        }

        public static MachineCode DisplaceMachineCode(MachineCode code, int count)
        {
            if (count < 0) throw new IndexOutOfRangeException();

            var added_code = new bool[count];
            var binary_code = added_code.Concat(code.binary_code).ToArray();

            var displace = 
                Calculator.Plus(string.Join("", GetNumberFromMatrix(code.displace).Reverse()),
                GetNumberFromMatrix(GetMatrixFromNumber(count)), NumericSystems.Binary);

            return new MachineCode(code.sign, GetBoolMatrix(displace), binary_code);
        }


        public static int GetNumberFromBinary(bool[] code)
            => int.Parse(Convertor.Convert(NumericSystems.Binary, NumericSystems.Decimal, GetNumberFromMatrix(code.Reverse().ToArray())));
        public static bool[] GetMatrixFromNumber(int number)
            => GetBoolMatrix(Convertor.Convert(NumericSystems.Decimal, NumericSystems.Binary, number.ToString()));
        public static int GetNumbersAfterDot(MachineCode code)
            => code.binary_code.Length - GetNumberFromBinary(code.displace) - 1;

    }
}
