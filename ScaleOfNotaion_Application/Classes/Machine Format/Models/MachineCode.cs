using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public class MachineCode : IComparable
    {
        public bool sign { get; private set; }

        public bool[] displace { get; private set; }

        public bool[] binary_code { get; private set; }
        

        public MachineCode(bool sign, bool[] displace, bool[] binary_code)
        {
            this.sign = sign;
            this.displace = displace;
            this.binary_code = binary_code;
        }

        public MachineCode(string s)
        {
            var str = s.Split();

            sign = str[0] == "1";
            displace = MachineCodeFormatter.RemoveExtededZerosAtTheEnd(MachineCodeFormatter.GetBoolMatrix(str[1]));
            binary_code = MachineCodeFormatter.RemoveExtededZerosAtTheEnd(MachineCodeFormatter.GetBoolMatrix(str[2]));
        }


        public int CompareTo(object obj)
        {
            if (obj is MachineCode)
            {
                var machine_code = obj as MachineCode;

                var sign_comparable = sign.CompareTo(machine_code.sign);
                if (sign_comparable != 0) return sign_comparable;

                var displace_comparable = Comparator.CompareBoolMatrix(displace, machine_code.displace);
                if (displace_comparable != 0) return displace_comparable;

                var binary_code_comparable = Comparator.CompareBoolMatrix(binary_code, machine_code.binary_code);
                return binary_code_comparable;
            }
            else return -1;
        }


        public override string ToString() => $"{(sign ? 1 : 0)} " +
            $"{string.Join("", displace.Reverse().Select(x => x ? 1 : 0))} " +
            $"{string.Join("", binary_code.Reverse().Select(x => x ? 1 : 0))}";
    }
}
