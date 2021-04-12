using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public static class MachineCalculator
    {
        public static MachineCode Solve(MachineCode operand_1, MachineCode operand_2, Operations operation)
        {
            switch (operation)
            {
                case Operations.Plus: return Plus(operand_1, operand_2);

                case Operations.Minus: return Minus(operand_1, operand_2);

                case Operations.Multiply: return Multiply(operand_1, operand_2);

                case Operations.Divide: return Divide(operand_1, operand_2);

                default: return null;
            }
        }

        public static MachineCode Plus(MachineCode operand_1, MachineCode operand_2)
        {
            if (operand_1.sign != operand_2.sign)
                return Minus(operand_1, new MachineCode(!operand_2.sign, operand_2.displace, operand_2.binary_code));

            (operand_1, operand_2) = MachineCodeFormatter.SetCommonDisplace(operand_1, operand_2);


            var list_result = new List<bool>();
            var buffer = false;

            for (int i = 0; i < operand_1.binary_code.Length || i < operand_2.binary_code.Length; i++)
            {
                if (i >= operand_1.binary_code.Length)
                {
                    if (operand_2.binary_code[i]) list_result.Add(!buffer);
                    else
                    {
                        list_result.Add(buffer);
                        buffer = false;
                    }
                }
                else
                if (i >= operand_2.binary_code.Length)
                {
                    if (operand_1.binary_code[i]) list_result.Add(!buffer);
                    else
                    {
                        list_result.Add(buffer);
                        buffer = false;
                    }
                }
                else
                {
                    if (operand_1.binary_code[i] == operand_2.binary_code[i])
                    {
                        list_result.Add(buffer);
                        buffer = operand_1.binary_code[i] && operand_2.binary_code[i];
                    }
                    else list_result.Add(!buffer);
                }
            }

            if (buffer) list_result.Add(buffer);

            return new MachineCode(operand_1.sign, operand_1.displace, list_result.ToArray());
        }

        public static MachineCode Minus(MachineCode operand_1, MachineCode operand_2)
        {
            return null;
        }

        public static MachineCode Multiply(MachineCode operand_1, MachineCode operand_2)
        {
            return null;
        }

        public static MachineCode Divide(MachineCode operand_1, MachineCode operand_2)
        {
            return null;
        }
    }
}
