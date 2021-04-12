using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ScaleOfNotaion_Application.Classes.Machine_Format;
using System.Linq;

namespace Machine_Code_Tests
{
    [TestClass]
    public class MachineCodeTesting
    {
        [TestMethod]
        public void CodeFormat()
        {
            MachineCode code_1 = new MachineCode("0 1111000 11001");

            Assert.AreEqual("10011", string.Join("", code_1.binary_code.Select(x => x ? 1 : 0)));
        }

        [TestMethod]
        public void Compare()
        {
            bool[] code_1 = "101000".Select(x => x == '1').ToArray();
            bool[] code_2 = "10010".Select(x => x == '1').ToArray();
            bool[] code_3 = "0101000".Select(x => x == '1').ToArray();
            bool[] code_4 = "10010".Select(x => x == '1').ToArray();

            Assert.IsTrue(Comparator.CompareReverseBoolMatrix(code_1, code_2) > 0);
            Assert.IsTrue(Comparator.CompareReverseBoolMatrix(code_2, code_3) < 0);
            Assert.IsTrue(Comparator.CompareReverseBoolMatrix(code_1, code_3) == 0);
            Assert.IsTrue(Comparator.CompareReverseBoolMatrix(code_2, code_4) == 0);
            Assert.IsTrue(Comparator.CompareReverseBoolMatrix(code_3, code_4) > 0);

        }

        [TestMethod]
        public void DisplaceMachineCode()
        {
            MachineCode code_1 = new MachineCode("0 1111000 110110");
            MachineCode code_2 = new MachineCode("0 1111010 110110");

            var (new_code_1, new_code_2) = MachineCodeFormatter.SetCommonDisplace(code_1, code_2);

            Assert.AreEqual("00011011", 
                string.Join("", new_code_1.binary_code.Select(x => x ? 1 : 0)));

            Assert.AreEqual("0101111",
                string.Join("", new_code_1.displace.Select(x => x ? 1 : 0)));

            Assert.AreEqual("011011",
                string.Join("", new_code_2.binary_code.Select(x => x ? 1 : 0)));

        }

        [TestMethod]
        public void Plus_1()
        {
            MachineCode code_1 = new MachineCode("0 1111000 11001");
            MachineCode code_2 = new MachineCode("0 1111000 110100");

            var result_code = MachineCalculator.Plus(code_1, code_2);

            Assert.AreEqual("0 1111000 1001101", result_code.ToString());
        }

    }
}
