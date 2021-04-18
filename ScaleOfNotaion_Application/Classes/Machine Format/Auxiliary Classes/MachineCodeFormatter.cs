using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public class MachineCodeFormatter : MatrixTransformations
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

            var binary_code = (new bool[count]).Concat(code.binary_code).ToArray();

            var displace = 
                Calculator.Plus(string.Join("", GetStringNumberFromMatrix(code.displace).Reverse()),
                GetStringNumberFromMatrix(GetMatrixFromNumber(count)), NumericSystems.Binary);

            return new MachineCode(code.sign, GetBoolMatrix(displace), binary_code);
        }

        public static (MachineCode, MachineCode) SetCommonLength(MachineCode operand_1, MachineCode operand_2)
        {
            if (operand_1.binary_code.Length - operand_2.binary_code.Length != 0)
            {
                if (operand_1.binary_code.Length > operand_2.binary_code.Length)
                {
                    operand_2 = new MachineCode(operand_2.sign, operand_2.displace,
                        operand_2.binary_code.Concat(new bool[operand_1.binary_code.Length - operand_2.binary_code.Length]).ToArray());
                }
                else
                {
                    operand_1 = new MachineCode(operand_1.sign, operand_1.displace,
                        operand_1.binary_code.Concat(new bool[operand_2.binary_code.Length - operand_1.binary_code.Length]).ToArray());
                }
            }

            return (operand_1, operand_2);
        }


        public static bool[] GetAdditionalCode(bool[] code)
            => GetBoolMatrix(Calculator.Plus(GetInvertedCode(code), "1", NumericSystems.Binary));

        public static string GetInvertedCode(bool[] code)
            => GetStringNumberFromMatrix(code.Select(x => !x).Reverse().ToArray());

    }
}
