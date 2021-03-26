using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application
{
    class Validator
    {
        public NumericSystem originalNumSystem { get; private set; }

        public string number { get; private set; }

        public Validator(NumericSystem originalNumSystem, string number)
        {
            this.originalNumSystem = originalNumSystem;
            this.number = number;
        }

        public bool isValidate()
        {
            number = number.ToUpper();
            string pattern = "";

            switch (originalNumSystem)
            {
                case NumericSystem.Binary:
                    pattern = "^[0-1]+$";
                    break;
                case NumericSystem.Ternary:
                    pattern = "^[0-2]+$";
                    break;
                case NumericSystem.Quaternary:
                    pattern = "^[0-3]+$";
                    break;
                case NumericSystem.Fivefold:
                    pattern = "^[0-4]+$";
                    break;
                case NumericSystem.Octal:
                    pattern = "^[0-7]+$";
                    break;
                case NumericSystem.Decimal:
                    pattern = "^[0-9]+$";
                    break;
                case NumericSystem.DuoDecimal:
                    pattern = "^[0-9,A-B]+$";
                    break;
                case NumericSystem.Hexadecimal:
                    pattern = "^[0-9,A-F]+$";
                    break;
            }

            return Regex.IsMatch(number, pattern);
        }

        public Convertor GetConverter() => new Convertor(originalNumSystem, number);

    }
}
