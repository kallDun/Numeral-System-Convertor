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
    public partial class CalculatorPage : Page
    {
        public CalculatorPage()
        {
            InitializeComponent();
            ComboBox_NumericSystem.ItemsSource = Enum.GetValues(typeof(NumericSystems)).Cast<NumericSystems>();
            ComboBox_Operation.ItemsSource = Enum.GetValues(typeof(Operations)).Cast<Operations>();
        }

        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_NumericSystem.SelectedItem == null ||
                ComboBox_Operation.SelectedItem == null ||
                string.IsNullOrEmpty(TextBox_InitialOperand_1.Text) ||
                string.IsNullOrEmpty(TextBox_InitialOperand_2.Text))
            {
                MessageBox.Show("Missing data in fields!");
            }
            else
            {
                NumericSystems numericSystem = (NumericSystems)ComboBox_NumericSystem.SelectedItem;
                Operations operation = (Operations)ComboBox_Operation.SelectedItem;

                Validator validator_operand_1 = new Validator(numericSystem, TextBox_InitialOperand_1.Text);
                Validator validator_operand_2 = new Validator(numericSystem, TextBox_InitialOperand_2.Text);

                if (!validator_operand_1.isValidate())
                {
                    MessageBox.Show("First operand is not validate to specified numeric system!");
                }
                else
                if (!validator_operand_2.isValidate())
                {
                    MessageBox.Show("Second operand is not validate to specified numeric system!");
                }
                else
                {
                    Calculator calculator = new Calculator(numericSystem, 
                        TextBox_InitialOperand_1.Text, TextBox_InitialOperand_2.Text);

                    Calculated_TextBlock.Text = calculator.Solve(operation);
                }
            }
        }
    }
}
