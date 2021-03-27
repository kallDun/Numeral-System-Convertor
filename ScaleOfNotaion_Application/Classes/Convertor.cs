using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

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
                return fraction != 0 ? string.Format("{0:0}{1:.0000000000}", numb, fraction) : numb.ToString();
            }
            else // (otherNumSystem != originalNumSystem)
            {
                var (numb, fraction) = ConvertToDecimalSystem();

                StringBuilder
                    sb_integer = new StringBuilder(),
                    sb_fraction = new StringBuilder();

                while (numb != 0)
                {
                    sb_integer.Append(SymbolOf((byte)(numb % (int)otherNumSystem)));

                    numb /= (int)otherNumSystem;
                }

                while (sb_fraction.Length < 20 && fraction != 0)
                {
                    fraction *= (int)otherNumSystem;
                    var integer_number = (byte)Math.Truncate(fraction);                    

                    sb_fraction.Append(SymbolOf(integer_number));
                    fraction -= integer_number;
                }

                return sb_fraction.Length > 0 ? 
                    $"{string.Join("", sb_integer.ToString().Reverse())}." +
                    $"{string.Join("", sb_fraction.ToString())}" : 
                    string.Join("", sb_integer.ToString().Reverse());
            }
        }


        public (BigInteger, double) ConvertToDecimalSystem()
        {
            BigInteger result = 0;
            double fraction = 0;

            List<char> 
                array1 = new List<char>(), 
                array2 = new List<char>();

            if (number.Contains('.'))
            {
                bool isAfterDot = false;
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] != '.')
                    {
                        if (isAfterDot) array2.Add(number[i]);
                        else array1.Add(number[i]);
                    }
                    else isAfterDot = true;                    
                }
                array1.Reverse();
            }
            else
            {
                array1 = number.Reverse().ToList();
            }            

            for (int i = 0; i < array1.Count; i++)
            {
                result += (long)Math.Pow((int)originalNumSystem, i) * NumberOf(array1[i]);
            }
            for (int i = 0; i < array2.Count; i++)
            {
                fraction += Math.Pow((int)originalNumSystem, -(i + 1)) * NumberOf(array2[i]);
            }

            return (result, fraction);
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
