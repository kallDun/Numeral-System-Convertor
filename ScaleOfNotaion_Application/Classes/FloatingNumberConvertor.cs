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
                number.Length - 1  - Regex.Match(number, "[.](.*?)$").Groups[1].Value.Length :
                number.Length;
            displace--;

            var floating_number = Displace(number, -displace);
            string result_floating_number = $"{floating_number} * {(int)intitialNumericSystem}^{displace}";


            string machine_code = "0" + " ";

            machine_code += Calculator.Plus(
                Convertor.Convert(NumericSystems.Decimal, NumericSystems.Binary, displace.ToString()),
                "1111111",
                NumericSystems.Binary);

            var num = Convertor
                .Convert(intitialNumericSystem, NumericSystems.Binary,
                Displace(floating_number, floating_number.Length - 2));

            machine_code += " " + num;
            machine_code += $"(0 x {32 - num.Length})";

            return (result_floating_number, machine_code);
        }

    }
}
