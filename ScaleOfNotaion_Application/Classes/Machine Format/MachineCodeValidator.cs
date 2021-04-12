using System.Text.RegularExpressions;

namespace ScaleOfNotaion_Application.Classes.Machine_Format
{
    class MachineCodeValidator
    {
        public string machine_code { get; private set; }

        public MachineCodeValidator(string machine_code)
        {
            this.machine_code = machine_code;
        }

        public bool isValidate()
            => Regex.IsMatch(machine_code, @"^[0-1]{1}[\s]+[0-1]+[\s]+[0-1]+$");

        public MachineCode GetMachineCode() => new MachineCode(machine_code);

    }
}
