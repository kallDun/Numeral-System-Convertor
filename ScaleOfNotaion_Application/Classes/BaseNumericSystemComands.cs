using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application
{
    class BaseNumericSystemComands
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

            numb_1_int_part = Formatter.RemoveZerosInBegin(numb_1_int_part);
            numb_2_int_part = Formatter.RemoveZerosInBegin(numb_2_int_part);

            if (numb_1_int_part.Length != numb_2_int_part.Length)
            {
                return numb_1_int_part.Length > numb_2_int_part.Length ? 1 : -1;
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
            for (int i = 0; i < part_1.Length || i < part_2.Length; i++)
            {
                if (NumberOf(part_1[i]) > NumberOf(part_2[i])) return 1;
                if (NumberOf(part_1[i]) < NumberOf(part_2[i])) return -1;
            }

            if (part_1.Length > part_2.Length) return 1;
            else if (part_1.Length < part_2.Length) return -1;
            else return 0;
        }
    }
}
