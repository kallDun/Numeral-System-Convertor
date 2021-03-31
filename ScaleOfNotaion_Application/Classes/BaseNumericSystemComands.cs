using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static string RemoveZerosInBegin(string number)
        {
            int countToRemove = 0;

            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] != '0')
                {
                    countToRemove = i;
                    break;
                }
            }

            return number.Remove(0, countToRemove);
        }


        /// <summary>
        /// Compare number_1 to number_2. Return positive if number_1 is bigger, 
        /// negative if number_2 bigger or 0 if they are equal.
        /// </summary>
        public static int Compare(string number_1, string number_2)
        {
            number_1 = RemoveZerosInBegin(number_1);
            number_2 = RemoveZerosInBegin(number_2);

            if (number_1.Length != number_2.Length)
            {
                return number_1.Length > number_2.Length ? 1 : -1;
            }
            else
            {
                for (int i = 0; i < number_1.Length; i++)
                {
                    if (NumberOf(number_1[i]) > NumberOf(number_2[i])) return 1;
                    if (NumberOf(number_1[i]) < NumberOf(number_2[i])) return -1;
                }
                return 0;
            }
        }
    }
}
