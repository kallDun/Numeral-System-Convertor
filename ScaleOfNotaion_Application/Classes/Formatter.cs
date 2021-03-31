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
        private static string RemoveZerosInBegin(string integer)
        {
            int countToRemove = 0;

            for (int i = 0; i < integer.Length; i++)
            {
                if (integer[i] != '0')
                {
                    countToRemove = i;
                    break;
                }
            }

            return integer.Remove(0, countToRemove);
        }
        private static string RemoveZerosInEnd(string fraction)
        {
            int CountToRemove = 0;

            var array = fraction.Reverse().ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != '0')
                {
                    CountToRemove = i;
                    break;
                }
            }

            return fraction.Remove(fraction.Length - CountToRemove, CountToRemove);
        }

        public static string RemoveExcessZeros(string number)
        {
            var (integer_part, fraction_part) = SplitNumberByDot(number);
            return GetFormat(RemoveZerosInBegin(integer_part), RemoveZerosInEnd(fraction_part));
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

        public static int DigitsAfterDotAlignment(string number_1, string number_2)
        {
            var (op_1_int_part, op_1_frac_part) = SplitNumberByDot(number_1);
            var (op_2_int_part, op_2_frac_part) = SplitNumberByDot(number_2);

            return DigitsAfterDotAlignment(ref op_1_frac_part, ref op_2_frac_part);
        }

        public static (string, string) SplitNumberByDot(string number)
            => number.Contains('.') ?
            (RemoveZerosInBegin(Regex.Match(number, "^(.*?)[.]").Groups[1].Value), 
            RemoveZerosInEnd(Regex.Match(number, "[.](.*?)$").Groups[1].Value)) :
            (RemoveZerosInBegin(number), "0");

        public static bool IsZero(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] != '0' && number[i] != '.')
                {
                    return false;
                }
            }

            return true;
        }


        public static string GetFormat(string integer_part, string fraction_part)
        {
            if (integer_part == "") integer_part = "0";
            if (fraction_part == "") fraction_part = "0";
            integer_part = RemoveZerosInBegin(integer_part);
            fraction_part = RemoveZerosInEnd(fraction_part);
            return IsZero(fraction_part) ? integer_part : integer_part + '.' + fraction_part;
        }

        public static string GetFormat(string number)
        {
            var (integer_part, fraction_part) = SplitNumberByDot(number);
            return GetFormat(integer_part, fraction_part);
        }

        public static string GetFormat(BigInteger integer, double fraction) 
            => GetFormat(integer.ToString(), (fraction > 0) ? fraction.ToString().Remove(0, 2) : fraction.ToString());
    }
}
