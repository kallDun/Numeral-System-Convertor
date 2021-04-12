using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application
{
    abstract class BaseNumericSystemComands
    {
        public static byte NumberOf(char n)
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

        public static char SymbolOf(byte n)
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


        /// <summary>
        /// Compare number_1 to number_2. Return positive if number_1 is bigger, 
        /// negative if number_2 bigger or 0 if they are equal.
        /// </summary>
        public static int Compare(string number_1, string number_2)
        {
            var (numb_1_int_part, numb_1_frac_part) = Formatter.SplitNumberByDot(number_1);
            var (numb_2_int_part, numb_2_frac_part) = Formatter.SplitNumberByDot(number_2);

            if (numb_1_int_part.Length != numb_2_int_part.Length)
            {
                return (numb_1_int_part.Length > numb_2_int_part.Length) ? 1 : -1;
            }
            else
            {
                var integer_compared = CompareNumberPart(numb_1_int_part, numb_2_int_part);

                if (integer_compared != 0) return integer_compared;
                else 
                    return CompareNumberPart(numb_1_frac_part, numb_2_frac_part);
            }
        }

        private static int CompareNumberPart(string part_1, string part_2)
        {
            for (int i = 0; i < part_1.Length && i < part_2.Length; i++)
            {
                if (NumberOf(part_1[i]) > NumberOf(part_2[i])) return 1;
                if (NumberOf(part_1[i]) < NumberOf(part_2[i])) return -1;
            }

            if (part_1.Length > part_2.Length) return 1;
            else if (part_1.Length < part_2.Length) return -1;
            else return 0;
        }


        public static string Displace(string number, int count)
        {
            if (count == 0) return number;
            var (integer_part, fraction_part) = Formatter.SplitNumberByDot(number);


            var isPositiveDisplace = count > 0;
            for (int i = 0; i < Math.Abs(count); i++)
            {
                if (isPositiveDisplace)
                {
                    string buffer;

                    if (Formatter.IsZero(fraction_part)) buffer = "0";
                    else
                    {
                        buffer = fraction_part[0].ToString();
                        fraction_part = fraction_part.Remove(0, 1);
                    }

                    integer_part = integer_part.Insert(integer_part.Length, buffer);
                }
                else
                {
                    string buffer;

                    if (Formatter.IsZero(integer_part)) buffer = "0";
                    else
                    {
                        buffer = integer_part[integer_part.Length - 1].ToString();
                        integer_part = integer_part.Remove(integer_part.Length - 1, 1);
                    }

                    fraction_part = fraction_part.Insert(0, buffer);
                }
            }

            return Formatter.GetFormat(integer_part, fraction_part);
        }



        public static string GetAdditionalCode(string number, NumericSystems NumSystem)
            => Calculator.Plus(GetInvertedNumber(number, NumSystem), "1", NumSystem);

        public static string GetInvertedNumber(string number, NumericSystems NumSystem)
        {
            var result = "";

            for (int i = 0; i < number.Length; i++)
            {
                result += SymbolOf((byte)((byte)NumSystem - 1 - NumberOf(number[i])));
            }

            return result;
        }
    }
}
