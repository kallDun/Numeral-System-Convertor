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
    public partial class ConvertorPage : Page
    {
        public ConvertorPage()
        {
            InitializeComponent();
            ComboBox_ConvertedFrom.ItemsSource = Enum.GetValues(typeof(NumericSystem)).Cast<NumericSystem>();
            ComboBox_ConvertedTo.ItemsSource = Enum.GetValues(typeof(NumericSystem)).Cast<NumericSystem>();
        }

        private void Convert_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_ConvertedFrom.SelectedItem == null || 
                ComboBox_ConvertedTo.SelectedItem == null || 
                string.IsNullOrEmpty(TextBox_InitialNumber.Text))
            {
                MessageBox.Show("Missing data in fields!");
            }
            else
            {
                NumericSystem numericSystemConvertFrom = (NumericSystem)ComboBox_ConvertedFrom.SelectedItem;
                NumericSystem numericSystemConvertTo = (NumericSystem)ComboBox_ConvertedTo.SelectedItem;
                Validator validator = new Validator(numericSystemConvertFrom, TextBox_InitialNumber.Text);

                if (validator.isValidate())
                {
                    Convertor convertor = validator.GetConvertor();

                    Converted_TextBlock.Text = convertor.ConvertToOtherSystem(numericSystemConvertTo);
                }
                else
                {
                    MessageBox.Show("Number is not validate to specified numeric system!");
                }
            }
        }
    }
}
