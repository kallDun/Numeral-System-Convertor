using System.Text.RegularExpressions;

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
            return Regex.IsMatch(number, $"^[{GetPatternCore()}]+[.]?[{GetPatternCore()}]+$");
        }

        private string GetPatternCore()
        {
            switch (originalNumSystem)
            {
                case NumericSystem.Binary:
                    return "0-1";

                case NumericSystem.Ternary:
                    return "0-2";

                case NumericSystem.Quaternary:
                    return "0-3";

                case NumericSystem.Fivefold:
                    return "0-4";

                case NumericSystem.Octal:
                    return "0-7";

                case NumericSystem.Decimal:
                    return "0-9";

                case NumericSystem.DuoDecimal:
                    return "0-9,A-B";

                case NumericSystem.Hexadecimal:
                    return "0-9,A-F";

                default: return "";
            }
        }


        public Convertor GetConverter() => new Convertor(originalNumSystem, number);

    }
}
