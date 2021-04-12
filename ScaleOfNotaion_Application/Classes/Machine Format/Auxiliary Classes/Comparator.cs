using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    public static class Comparator
    {
        public static int CompareBoolMatrix(bool[] mx_1, bool[] mx_2)
        {
            mx_1 = MachineCodeFormatter.RemoveExtededZerosAtTheEnd(mx_1);
            mx_2 = MachineCodeFormatter.RemoveExtededZerosAtTheEnd(mx_2);

            if (mx_1.Length != mx_2.Length)
            {
                return (mx_1.Length > mx_2.Length) ? 1 : -1;
            }

            for (int i = mx_1.Length - 1; i >= 0; i--)
            {
                if (mx_1[i] != mx_2[i]) return mx_1[i].CompareTo(mx_2[i]);
            }

            return 0;
        }

        public static int CompareReverseBoolMatrix(bool[] mx_1, bool[] mx_2)
            => CompareBoolMatrix(mx_1.Reverse().ToArray(), mx_2.Reverse().ToArray());
    }
}
