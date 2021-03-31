using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application
{
    static class Formatter
    {
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

        public static int DigitsAfterDotAlignment(ref string op_1_frac_part, ref string op_2_frac_part)
        {
            int numbersAfterDot = Math.Max(op_1_frac_part.Length, op_2_frac_part.Length);

            for (int i = 0; i < numbersAfterDot; i++)
            {
                if (op_1_frac_part.Length <= i) op_1_frac_part += '0';
                if (op_2_frac_part.Length <= i) op_2_frac_part += '0';
            }

            return numbersAfterDot;
        }

        public static (string, string) SplitNumberByDot(string number)
            => number.Contains('.') ?
            (RemoveZerosInBegin(Regex.Match(number, "^(.*?)[.]").Groups[1].Value), Regex.Match(number, "[.](.*?)$").Groups[1].Value) :
            (RemoveZerosInBegin(number), "0");

        public static string GetFormat(string integer_part, string fraction_part)
        {
            bool isFractionPartZero = true;
            for (int i = 0; i < fraction_part.Length; i++)
            {
                if (fraction_part[i] != '0') 
                {
                    isFractionPartZero = false;
                    break;
                }
            }

            integer_part = RemoveZerosInBegin(integer_part);
            return isFractionPartZero ? integer_part : integer_part + '.' + fraction_part;
        }

        public static string GetFormat(string number)
        {
            var (integer_part, fraction_part) = SplitNumberByDot(number);
            return GetFormat(integer_part, fraction_part);
        }

        public static string GetFormat(BigInteger integer, double fraction) 
            => GetFormat(integer.ToString(), fraction.ToString().Remove(0, 2));
    }
}
