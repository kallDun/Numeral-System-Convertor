using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ScaleOfNotaion_Application
{
    class Convertor : BaseNumericSystemComands
    {
        public NumericSystems intitialNumericSystem { get; private set; }

        public string number { get; private set; }

        public Convertor(NumericSystems intitialNumericSystem, string number)
        {
            this.intitialNumericSystem = intitialNumericSystem;
            this.number = number;
        }

        public string ConvertToOtherSystem(NumericSystems otherNumSystem)
        {
            if (otherNumSystem == intitialNumericSystem)
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
                BigInteger integer_part;
                double fraction_part;

                if (intitialNumericSystem != NumericSystems.Decimal)
                {
                    (integer_part, fraction_part) = ConvertToDecimalSystem();
                }
                else
                {
                    var (str_int, str_frac) = Formatter.SplitNumberByDot(number);
                    (integer_part, fraction_part) = (BigInteger.Parse(str_int), double.Parse("0." + str_frac));
                }

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
                var (str_int, str_frac) = Formatter.SplitNumberByDot(number);
                array1 = str_int.Reverse().ToArray();
                array2 = str_frac.ToArray();
            }
            else
                array1 = number.Reverse().ToArray();


            for (int i = 0; i < array1.Length; i++)
            {
                integer_part += (long)Math.Pow((int)intitialNumericSystem, i) * NumberOf(array1[i]);
            }
            for (int i = 0; i < array2.Length; i++)
            {
                fraction_part += Math.Pow((int)intitialNumericSystem, -(i + 1)) * NumberOf(array2[i]);
            }

            return (integer_part, fraction_part);
        }


        public static string Convert(NumericSystems intitialNumericSystem, NumericSystems otherNumSystem, string number)
        {
            Convertor convertor = new Convertor(intitialNumericSystem, number);
            return convertor.ConvertToOtherSystem(otherNumSystem);
        }
    }
}
