using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace ScaleOfNotaion_Application
{
    class Convertor : BaseNumericSystemComands
    {
        public NumericSystems originalNumSystem { get; private set; }

        public string number { get; private set; }

        public Convertor(NumericSystems originalNumSystem, string number)
        {
            this.originalNumSystem = originalNumSystem;
            this.number = Formatter.RemoveExcessZeros(number);
        }

        public string ConvertToOtherSystem(NumericSystems otherNumSystem)
        {
            if (otherNumSystem == originalNumSystem)
            {
                return Formatter.GetFormat(number);
            }
            else
            if (otherNumSystem == NumericSystems.Decimal)
            {
                var (numb, fraction) = ConvertToDecimalSystem();
                return Formatter.GetFormat(numb, fraction);
            }
            else // (otherNumSystem != originalNumSystem)
            {
                var (integer_part, fraction_part) =
                    originalNumSystem != NumericSystems.Decimal ? ConvertToDecimalSystem() :
                    number.Contains('.') ?
                    (BigInteger.Parse(Regex.Match(number, "^(.*?)[.]").Groups[1].Value), double.Parse("0." + Regex.Match(number, "[.](.*?)$").Groups[1].Value)) :
                    (BigInteger.Parse(number), 0);

                StringBuilder
                    sb_integer = new StringBuilder(),
                    sb_fraction = new StringBuilder();

                while (integer_part != 0)
                {
                    sb_integer.Append(SymbolOf((byte)(integer_part % (int)otherNumSystem)));

                    integer_part /= (int)otherNumSystem;
                }

                while (sb_fraction.Length < 40 && fraction_part != 0)
                {
                    fraction_part *= (int)otherNumSystem;
                    var integer_number = (byte)Math.Truncate(fraction_part);                    

                    sb_fraction.Append(SymbolOf(integer_number));
                    fraction_part -= integer_number;
                }

                return Formatter.GetFormat(
                    string.Join("", sb_integer.ToString().Reverse()), sb_fraction.ToString()); 
            }
        }


        public (BigInteger, double) ConvertToDecimalSystem()
        {
            BigInteger integer_part = 0;
            double fraction_part = 0;

            char[]
                array1 = new char[0], 
                array2 = new char[0];


            if (number.Contains('.'))
            {
                array1 = Regex.Match(number, "^(.*?)[.]").Groups[1].Value.Reverse().ToArray();
                array2 = Regex.Match(number, "[.](.*?)$").Groups[1].Value.ToArray();
            }
            else
                array1 = number.Reverse().ToArray();


            for (int i = 0; i < array1.Length; i++)
            {
                integer_part += (long)Math.Pow((int)originalNumSystem, i) * NumberOf(array1[i]);
            }
            for (int i = 0; i < array2.Length; i++)
            {
                fraction_part += Math.Pow((int)originalNumSystem, -(i + 1)) * NumberOf(array2[i]);
            }

            return (integer_part, fraction_part);
        }
    }
}
