using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace ScaleOfNotaion_Application
{
    class Convertor
    {
        public NumericSystem originalNumSystem { get; private set; }

        public string number { get; private set; }

        public Convertor(NumericSystem originalNumSystem, string number)
        {
            this.originalNumSystem = originalNumSystem;
            this.number = number;                        
        }

        public string ConvertToOtherSystem(NumericSystem otherNumSystem)
        {
            if (otherNumSystem == originalNumSystem)
            {
                return number;
            }
            else
            if (otherNumSystem == NumericSystem.Decimal)
            {
                var (numb, fraction) = ConvertToDecimalSystem();

                return fraction == 0 ?
                    numb.ToString() :
                    $"{numb}{fraction}".Remove(numb.ToString().Length, 1);
            }
            else // (otherNumSystem != originalNumSystem)
            {
                var (integer_part, fraction_part) = originalNumSystem != NumericSystem.Decimal ? ConvertToDecimalSystem() : 
                    (BigInteger.Parse(Regex.Match(number, "^(.*?)[.]").Groups[1].Value), 
                    double.Parse("0." + Regex.Match(number, "[.](.*?)$").Groups[1].Value));

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

                return sb_fraction.Length > 0 ? 
                    $"{string.Join("", sb_integer.ToString().Reverse())}." +
                    $"{string.Join("", sb_fraction.ToString())}" : 
                    string.Join("", sb_integer.ToString().Reverse());
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


        private byte NumberOf(char n)
        {
            if (char.IsNumber(n)) return (byte)(n - 48);

            switch (n)
            {
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                default: return 0;
            }

        }

        private char SymbolOf(byte n)
        {
            if (n < 10) return (char)(n + 48);

            switch (n)
            {
                case 10: return 'A';
                case 11: return 'B';
                case 12: return 'C';
                case 13: return 'D';
                case 14: return 'E';
                case 15: return 'F';
                default: return '0';
            }
        }
    }
}
