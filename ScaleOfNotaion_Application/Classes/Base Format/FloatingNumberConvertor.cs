using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application
{
    class FloatingNumberConvertor : BaseNumericSystemComands
    {
        public NumericSystems intitialNumericSystem { get; private set; }

        public string number { get; private set; }

        public FloatingNumberConvertor(NumericSystems intitialNumericSystem, string number)
        {
            this.intitialNumericSystem = intitialNumericSystem;
            this.number = Formatter.RemoveExcessZeros(number);
        }


        public (string, string) Convert()
        {
            var displace = number.Contains('.') ?
                number.Length - 1 - Regex.Match(number, "[.](.*?)$").Groups[1].Value.Length :
                number.Length;
            displace--;
            var floating_number = Displace(number, -displace);

            string result_floating_number = $"{floating_number} * {(int)intitialNumericSystem}^{displace}";
            string machine_code = "0" + " ";

            var binary_number = Convertor.Convert(intitialNumericSystem, NumericSystems.Binary, number);
            var binary_displace = binary_number.Contains('.') ?
                binary_number.Length - 1 - Regex.Match(binary_number, "[.](.*?)$").Groups[1].Value.Length :
                binary_number.Length;
            binary_displace -= 1;


            
            machine_code += Calculator.Plus(
                Convertor.Convert(NumericSystems.Decimal, NumericSystems.Binary, binary_displace.ToString()),
                "1111111", NumericSystems.Binary);


            var num = Displace(binary_number, binary_number.Length - binary_displace - 1);
            machine_code += " " + num;
            machine_code += $"(0 x {32 - num.Length})";

            return (result_floating_number, machine_code);
        }

    }
}
