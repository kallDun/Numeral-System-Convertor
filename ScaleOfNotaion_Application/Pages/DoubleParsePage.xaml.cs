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


    }
}
