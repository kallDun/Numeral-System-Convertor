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
                if (number[i] != 0)
                {
                    countToRemove = i;
                    break;
                }
            }

            return number.Remove(0, countToRemove);
        }
    }
}
