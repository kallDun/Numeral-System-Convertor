using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application
{
    class Calculator : BaseNumericSystemComands
    {
        public Calculator(NumericSystems NumericSystem, string operand_1, string operand_2, Operations operation)
        {
            this.NumericSystem = NumericSystem;
            this.operand_1 = operand_1;
            this.operand_2 = operand_2;
            this.operation = operation;
        }

        public NumericSystems NumericSystem { get; private set; }

        public string operand_1 { get; private set; }

        public string operand_2 { get; private set; }

        public Operations operation { get; private set; }


        public string Solve()
        {
            switch (operation)
            {
                case Operations.Plus: return Plus(operand_1, operand_2, NumericSystem);

                case Operations.Minus: return Minus();

                case Operations.Multiply: return Multiply();

                case Operations.Divide: return Divide();

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

        public static string Minus()
        {
            return "";
        }

        public static string Multiply()
        {
            return "";
        }

        public static string Divide()
        {
            return "";
        }

    }
}
