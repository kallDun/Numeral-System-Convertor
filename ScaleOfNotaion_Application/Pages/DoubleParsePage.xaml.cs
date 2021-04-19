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

    public partial class DoubleParsePage : Page
    {
        public DoubleParsePage()
        {
            InitializeComponent();
            ComboBox_IntialNumericSystem.ItemsSource = Enum.GetValues(typeof(NumericSystems)).Cast<NumericSystems>();
        }

        private void Convert_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_IntialNumericSystem.SelectedItem == null ||
                string.IsNullOrEmpty(TextBox_InitialNumber.Text))
            {
                MessageBox.Show("Missing data in fields!");
            }
            else
            {
                NumericSystems intialNumericSystem = (NumericSystems)ComboBox_IntialNumericSystem.SelectedItem;
                Validator validator = new Validator(intialNumericSystem, TextBox_InitialNumber.Text);

                if (validator.isValidate())
                {
                    FloatingNumberConvertor convertor = validator.GetFloatingNumberConvertor();

                    var (str, MachineCode) = convertor.Convert();
                    (MachineCode_TextBlock.Text, NumberResult_TextBlock.Text) = (MachineCode.ToString(), str);
                }
                else
                {
                    MessageBox.Show("Number is not validate to specified numeric system!");
                }
            }
        }
    }
}
