using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScaleOfNotaion_Application
{
    class Calculator : BaseNumericSystemComands
    {
        public Calculator(NumericSystems NumericSystem, string operand_1, string operand_2)
        {
            this.NumericSystem = NumericSystem;
            this.operand_1 = RemoveZerosInBegin(operand_1);
            this.operand_2 = RemoveZerosInBegin(operand_2);
        }

        public NumericSystems NumericSystem { get; private set; }

        public string operand_1 { get; private set; }

        public string operand_2 { get; private set; }


        public string Solve(Operations operation)
        {
            switch (operation)
            {
                case Operations.Plus: return Plus(operand_1, operand_2, NumericSystem);

                case Operations.Minus: return Minus(operand_1, operand_2, NumericSystem);

                case Operations.Multiply: return Multiply(operand_1, operand_2, NumericSystem);

                case Operations.Divide: return Divide(operand_1, operand_2, NumericSystem);

                default: return "";
            }
        }



        public static string Plus(string op_1, string op_2, NumericSystems NumSystem)
        {
            var operand_1 = op_1.Reverse().ToArray();
            var operand_2 = op_2.Reverse().ToArray();
            var result = new List<char>();


            int num_sys = (int)NumSystem;
            int buffer = 0;

            for (int i = 0; i < operand_1.Length || i < operand_2.Length; i++)
            {
                if (i < operand_1.Length) buffer += NumberOf(operand_1[i]);
                if (i < operand_2.Length) buffer += NumberOf(operand_2[i]);

                result.Add(SymbolOf((byte) (buffer % num_sys)));

                buffer /= num_sys;
            }

            if (buffer != 0) result.Add(SymbolOf((byte)buffer));

            result.Reverse();
            return string.Join("", result);
        }

        public static string Minus(string op_1, string op_2, NumericSystems NumSystem)
        {
            string sign = "";

            // if second operator biggen than first - change their place & make sign minus
            if (Compare(op_1, op_2) < 0)
            {
                var temp = op_1;
                op_1 = op_2;
                op_2 = temp;
                sign = "-";
            }

            // add zeros if number's length are not equal
            if (op_1.Length > op_2.Length)
            {
                var difference = op_1.Length - op_2.Length;

                for (int i = 0; i < difference; i++)
                {
                    op_2 = op_2.Insert(0, "0");
                }
            }

            return 
                sign + RemoveZerosInBegin(
                    Plus(op_1, GetAdditionalCode(op_2, NumSystem), NumSystem)
                    .Remove(0, 1));
        }

        public static string Multiply(string op_1, string op_2, NumericSystems NumSystem)
        {
            return "";
        }

        public static string Divide(string op_1, string op_2, NumericSystems NumSystem)
        {
            return "";
        }


        private static string GetAdditionalCode(string number, NumericSystems NumSystem) 
            => Plus(GetInvertedNumber(number, NumSystem), "1", NumSystem);

        private static string GetInvertedNumber(string number, NumericSystems NumSystem)
        {
            var result = "";

            for (int i = 0; i < number.Length; i++)
            {
                result += SymbolOf((byte)((byte)NumSystem - 1 - NumberOf(number[i])));
            }

            return result;
        }

    }
}
