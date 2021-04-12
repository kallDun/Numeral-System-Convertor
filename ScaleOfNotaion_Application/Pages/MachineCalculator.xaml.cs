using ScaleOfNotaion_Application.Classes.Machine_Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScaleOfNotaion_Application
{
    /// <summary>
    /// Interaction logic for MachineCalculator.xaml
    /// </summary>
    public partial class MachineCalculatorPage : Page
    {
        public MachineCalculatorPage()
        {
            InitializeComponent();
            ComboBox_Operation.ItemsSource = Enum.GetValues(typeof(Operations)).Cast<Operations>();
        }

        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Operation.SelectedItem == null ||
                string.IsNullOrEmpty(TextBox_InitialOperand_1.Text) ||
                string.IsNullOrEmpty(TextBox_InitialOperand_2.Text))
            {
                MessageBox.Show("Missing data in fields!");
            }
            else
            {
                Operations operation = (Operations)ComboBox_Operation.SelectedItem;
                MachineCodeValidator validator_operand_1 = new MachineCodeValidator(TextBox_InitialOperand_1.Text);
                MachineCodeValidator validator_operand_2 = new MachineCodeValidator(TextBox_InitialOperand_2.Text);

                if (!validator_operand_1.isValidate())
                {
                    MessageBox.Show("First operand is not validate to machine representation of the code!");
                }
                else
                if (!validator_operand_2.isValidate())
                {
                    MessageBox.Show("Second operand is not validate to machine representation of the code!");
                }
                else
                {
                    var machine_code_1 = validator_operand_1.GetMachineCode();
                    var machine_code_2 = validator_operand_2.GetMachineCode();
                    var result_machine_code = MachineCalculator.Solve(machine_code_1, machine_code_2, operation);

                    Calculated_TextBlock.Text = 
                        result_machine_code != null ? result_machine_code.ToString() : "null";
                }
            }
        }
    }
}
