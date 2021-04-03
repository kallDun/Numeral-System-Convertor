using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScaleOfNotaion_Application
{
    class Calculator : BaseNumericSystemComands
    {
        public Calculator(NumericSystems NumericSystem, string operand_1, string operand_2)
        {
            this.NumericSystem = NumericSystem;
            this.operand_1 = Formatter.RemoveExcessZeros(operand_1);
            this.operand_2 = Formatter.RemoveExcessZeros(operand_2);
        }

        public NumericSystems NumericSystem { get; private set; }

        public string operand_1 { get; private set; }

        public string operand_2 { get; private set; }


        public string Solve(Operations operation)
        {
            switch (operation)
            {
                case Operations.Plus: return Plus(operand_1, operand_2, NumericSystem);

                case Operations.Minus: return Minus(operand_1, operand_2, NumericSystem);

                case Operations.Multiply: return Multiply(operand_1, operand_2, NumericSystem);

                case Operations.Divide: return Divide(operand_1, operand_2, NumericSystem);

                default: return "";
            }
        }



        public static string Plus(string op_1, string op_2, NumericSystems NumSystem)
        {
            // factor numbers into integer & fraction parts
            var (op_1_int_part, op_1_frac_part) = Formatter.SplitNumberByDot(op_1);
            var (op_2_int_part, op_2_frac_part) = Formatter.SplitNumberByDot(op_2);
            var dotIndex = Formatter.DigitsAfterDotAlignment(ref op_1_frac_part, ref op_2_frac_part);

            var operand_1 = op_1_int_part.Concat(op_1_frac_part).Reverse().ToArray();
            var operand_2 = op_2_int_part.Concat(op_2_frac_part).Reverse().ToArray();
            var result = new List<char>();
            
            // create buffer
            int num_sys = (int)NumSystem;
            int buffer = 0;

            // going in cycle for max count between 1 & 2 operators times
            for (int i = 0; i < operand_1.Length || i < operand_2.Length; i++)
            {
                // add value of next elements in every operator if it's not end
                if (i < operand_1.Length) buffer += NumberOf(operand_1[i]);
                if (i < operand_2.Length) buffer += NumberOf(operand_2[i]);

                // add to result remainder of the division
                result.Add(SymbolOf((byte)(buffer % num_sys)));

                // save to buffer integer of the division
                buffer /= num_sys;
            }

            // throw off buffer to result if it's not zero
            if (buffer != 0) result.Add(SymbolOf((byte)buffer));

            result.Insert(dotIndex, '.');
            result.Reverse();
            return Formatter.GetFormat(string.Join("", result));
        }

        public static string Minus(string op_1, string op_2, NumericSystems NumSystem)
        {
            string sign = "";

            // if second operator biggen than first - change their place & make sign minus
            if (Compare(op_1, op_2) < 0)
            {
                var temp = op_1;
                op_1 = op_2;
                op_2 = temp;
                sign = "-";
            }

            // convert numbers into integer & fraction parts
            var (op_1_int_part, op_1_frac_part) = Formatter.SplitNumberByDot(op_1);
            var (op_2_int_part, op_2_frac_part) = Formatter.SplitNumberByDot(op_2);
            Formatter.DigitsAfterDotAlignment(ref op_1_frac_part, ref op_2_frac_part);

            // add zeros if number's length are not equal
            if (op_1_int_part.Length > op_2_int_part.Length)
            {
                var difference = op_1_int_part.Length - op_2_int_part.Length;

                for (int i = 0; i < difference; i++)
                {
                    op_2_int_part = op_2_int_part.Insert(0, "0");
                }
            }

            // find integer part
            var integer = Plus(op_1_int_part, GetAdditionalCode(op_2_int_part, NumSystem), NumSystem).Remove(0, 1);

            string fraction;
            // find out if 1 fraction more than another
            if (Compare(op_1_frac_part, op_2_frac_part) < 0)
            {
                fraction = GetAdditionalCode(
                    Plus(op_2_frac_part, GetAdditionalCode(op_1_frac_part, NumSystem), NumSystem)
                    .Remove(0, 1), NumSystem);

                integer = Minus(integer, "1", NumSystem);
            }
            else fraction = Plus(op_1_frac_part, GetAdditionalCode(op_2_frac_part, NumSystem), NumSystem).Remove(0, 1);

            return sign + Formatter.GetFormat(integer, fraction);
        }

        public static string Multiply(string op_1, string op_2, NumericSystems NumSystem)
        {
            // split <first operand> into integer & fraction parts 
            var (op_1_int_part, op_1_frac_part) = Formatter.SplitNumberByDot(op_1);

            string result = "0";

            // adding integer part <int_part> times
            var multiply_list = op_1_int_part.Reverse().ToArray();
            for (int i = 0; i < multiply_list.Length; i++)
            {
                for (int j = 0; j < NumberOf(multiply_list[i]); j++)
                {
                    result = Plus(result, Displace(op_2, i), NumSystem);
                }
            }

            // adding fraction part <frac_part> times
            for (int i = 0; i < op_1_frac_part.Length; i++)
            {
                for (int j = 0; j < NumberOf(op_1_frac_part[i]); j++)
                {
                    result = Plus(result, Displace(op_2, -(i + 1)), NumSystem);
                }
            }

            return result;
        }

        public static string Divide(string op_1, string op_2, NumericSystems NumSystem)
        {
            string result_ = "";

            var numbers_to_Displace = Formatter.DigitsAfterDotAlignment(op_1, op_2);
            op_1 = Displace(op_1, numbers_to_Displace);
            op_2 = Displace(op_2, numbers_to_Displace);

            int integer_number_count = 1;
            bool is_first_time = true;
            int now_int_number, last_int_number = 0, base_displace = 0;


            while (!Formatter.IsZero(op_1) && result_.Length - integer_number_count < 20)
            {
                // if second OP bigger than first
                while (Compare(op_1, op_2) < 0)
                {
                    op_1 = Displace(op_1, 1);
                    base_displace++;
                }


                // create dividing part - number from <second operand>, that can be maximum divided by <first operand>
                string dividing_part = op_2;
                now_int_number = 1 - base_displace;
                while (Compare(Displace(dividing_part, 1), op_1) < 0)
                {
                    dividing_part = Displace(dividing_part, 1);
                    now_int_number++;
                }

                if (is_first_time) 
                {
                    integer_number_count = now_int_number;
                    is_first_time = false;
                }
                else
                {
                    var discharge = last_int_number - now_int_number;
                    for (int i = 0; i < discharge - 1; i++)
                    {
                        result_ += '0';
                    }
                }
                last_int_number = now_int_number;


                // <first operand> minus <dividing part> (toResult) times
                var toResult = 0;

                while (Compare(op_1, dividing_part) >= 0)
                {
                    op_1 = Minus(op_1, dividing_part, NumSystem);
                    toResult++;
                }

                result_ += SymbolOf((byte)toResult);
            }


            // displace result by <integer number count>
            return Displace(result_, integer_number_count - result_.Length);
        }

    }
}
