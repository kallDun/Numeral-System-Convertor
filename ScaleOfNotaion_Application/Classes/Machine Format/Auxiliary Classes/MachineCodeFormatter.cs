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
            var compare = Comparator.CompareBoolMatrix(op_1.displace, op_2.displace);
            if (compare == 0) return (op_1, op_2);


            var numb_1 = compare < 0 ? op_1 : op_2;
            var numb_2 = compare < 0 ? op_2 : op_1;

            int displace_int = int.Parse(
                Convertor.Convert(NumericSystems.Binary, NumericSystems.Decimal, 
                Calculator.Minus(
                    string.Join("", GetNumberFromMatrix(numb_1.displace).Reverse()), 
                    string.Join("", GetNumberFromMatrix(numb_2.displace).Reverse()), 
                    NumericSystems.Binary)));

            if (compare < 0)
                op_1 = DisplaceMachineCode(op_1, displace_int);
            else
                op_2 = DisplaceMachineCode(op_2, displace_int);

            return (op_1, op_2);
        }

        public static MachineCode DisplaceMachineCode(MachineCode code, int count)
        {
            if (count < 0) throw new IndexOutOfRangeException();

            var added_code = new bool[count];
            var binary_code = added_code.Concat(code.binary_code).ToArray();

            var displace = 
                Calculator.Plus(string.Join("", GetNumberFromMatrix(code.displace).Reverse()), 
                Convertor.Convert(NumericSystems.Decimal, NumericSystems.Binary, count.ToString()), 
                NumericSystems.Binary);

            return new MachineCode(code.sign, GetBoolMatrix(displace), binary_code);
        }

    }
}
