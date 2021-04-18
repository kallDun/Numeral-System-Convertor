using System.Collections.Generic;
using System.Linq;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public class MatrixTransformations
    {

        public static int GetNumberFromBinary(bool[] code)
            => int.Parse(Convertor.Convert(NumericSystems.Binary, NumericSystems.Decimal, GetStringNumberFromMatrix(code.Reverse())));

        public static bool[] GetMatrixFromNumber(int number)
            => GetBoolMatrix(Convertor.Convert(NumericSystems.Decimal, NumericSystems.Binary, number.ToString()));

        public static int GetNumbersAfterDot(MachineCode code)
            => code.binary_code.Length - GetNumberFromBinary(code.displace) - 1;

        public static bool[] GetBoolMatrix(string code) => code.Select(x => x == '1').Reverse().ToArray();

        public static string GetStringNumberFromMatrix(IEnumerable<bool> matrix) => string.Join("", matrix.Select(x => x ? 1 : 0));
    
    }
}