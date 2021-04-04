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

            string result_floating_number = $"{Displace(number, -displace)} * {(int)intitialNumericSystem}^{displace}";
            string machine_code = Calculator.Minus(
                "1111111",
                Convertor.Convert(NumericSystems.Decimal, NumericSystems.Binary, displace.ToString()), 
                NumericSystems.Binary);


            return (result_floating_number, machine_code);
        }

    }
}
