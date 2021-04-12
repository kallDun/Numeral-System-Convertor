using System.Text.RegularExpressions;

namespace ScaleOfNotaion_Application
{
    class Validator
    {
        public NumericSystems originalNumSystem { get; private set; }

        public string number { get; private set; }

        public Validator(NumericSystems originalNumSystem, string number)
        {
            this.originalNumSystem = originalNumSystem;
            this.number = Formatter.RemoveExcessZeros(number);
        }

        public bool isValidate()
        {
            number = number.ToUpper();
            return 
                Regex.IsMatch(number, $"^[{GetPatternCore()}]+[.]?[{GetPatternCore()}]+$") ||
                Regex.IsMatch(number, $"^[{GetPatternCore()}]+$");
        }

        private string GetPatternCore()
        {
            switch (originalNumSystem)
            {
                case NumericSystems.Binary:
                    return "0-1";

                case NumericSystems.Ternary:
                    return "0-2";

                case NumericSystems.Quaternary:
                    return "0-3";

                case NumericSystems.Fivefold:
                    return "0-4";

                case NumericSystems.Octal:
                    return "0-7";

                case NumericSystems.Decimal:
                    return "0-9";

                case NumericSystems.DuoDecimal:
                    return "0-9,A-B";

                case NumericSystems.Hexadecimal:
                    return "0-9,A-F";

                default: return "";
            }
        }


        public Convertor GetConvertor() => new Convertor(originalNumSystem, number);

        public FloatingNumberConvertor GetFloatingNumberConvertor() => new FloatingNumberConvertor(originalNumSystem, number);

    }
}
