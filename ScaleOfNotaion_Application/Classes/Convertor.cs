using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
                return ConvertToDecimalSystem().ToString();
            }
            else // (otherNumSystem != originalNumSystem)
            {
                BigInteger numb = ConvertToDecimalSystem();

                var sb = new StringBuilder();

                while (numb != 0)
                {
                    sb.Append(SymbolOf((byte)(numb % (int)otherNumSystem)));

                    numb /= (int)otherNumSystem;
                }

                return string.Join("", sb.ToString().Reverse());
            }
        }


        public BigInteger ConvertToDecimalSystem()
        {
            BigInteger result = 0;
            var array = number.Reverse().ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                result += (long)Math.Pow((int)originalNumSystem, i) * NumberOf(array[i]);
            }

            return result;
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
