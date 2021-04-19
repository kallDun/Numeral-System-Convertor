using System.Collections.Generic;
using System.Linq;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public class MachineCalculator : MachineCodeFormatter
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

            (operand_1, operand_2) = SetCommonDisplace(operand_1, operand_2);


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
            /*if (operand_1.sign != operand_2.sign)
                return Plus(operand_1, new MachineCode(!operand_2.sign, operand_2.displace, operand_2.binary_code));

            (operand_1, operand_2) = SetCommonDisplace(operand_1, operand_2);

            if (Comparator.CompareReverseBoolMatrix(operand_1.binary_code, operand_2.binary_code) < 0)
            {
                var temporary = new MachineCode(!operand_1.sign, operand_1.displace, operand_1.binary_code);
                operand_1 = new MachineCode(!operand_2.sign, operand_2.displace, operand_2.binary_code);
                operand_2 = temporary;
            }

            (operand_1, operand_2) = SetCommonLength(operand_1, new MachineCode(operand_2.sign, operand_2.displace, GetAdditionalCode(operand_2.binary_code)));
            var result = Plus(operand_1, operand_2);

            return new MachineCode(result.sign, result.displace, 
                result.binary_code.Take(result.binary_code.Length - 1).ToArray());*/
            var code_1 = FloatingNumberConvertor.BackConvert(operand_1);
            var code_2 = FloatingNumberConvertor.BackConvert(operand_2);
            var calculator = new Calculator(NumericSystems.Binary, code_1, code_2);
            var (a, b) = FloatingNumberConvertor.Convert(calculator.Solve(Operations.Minus), NumericSystems.Binary);
            return b;
        }

        public static MachineCode Multiply(MachineCode operand_1, MachineCode operand_2)
        {
            /*(operand_1, operand_2) = SetCommonDisplace(operand_1, operand_2);

            MachineCode result_code = new MachineCode("0 10000000 0");
            for (int i = 0; i < operand_2.binary_code.Length; i++)
            {
                if (operand_2.binary_code[i])
                {
                    result_code = Plus(result_code, DisplaceMachineCode(operand_1, i));
                }
            }

             
            return new MachineCode(operand_1.sign != operand_2.sign, result_code.displace, result_code.binary_code);*/
            
            var code_1 = FloatingNumberConvertor.BackConvert(operand_1);
            var code_2 = FloatingNumberConvertor.BackConvert(operand_2);
            var calculator = new Calculator(NumericSystems.Binary, code_1, code_2);
            var (a, b) = FloatingNumberConvertor.Convert(calculator.Solve(Operations.Multiply), NumericSystems.Binary);
            return b;
        }
        
        public static MachineCode Divide(MachineCode operand_1, MachineCode operand_2)
        {
            var code_1 = FloatingNumberConvertor.BackConvert(operand_1);
            var code_2 = FloatingNumberConvertor.BackConvert(operand_2);
            var calculator = new Calculator(NumericSystems.Binary, code_1, code_2);
            var (a, b) = FloatingNumberConvertor.Convert(calculator.Solve(Operations.Divide), NumericSystems.Binary);
            return b;
        }
    }
}
