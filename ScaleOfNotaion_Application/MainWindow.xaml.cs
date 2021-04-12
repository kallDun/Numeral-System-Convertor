using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            GoToPage("Convertor");
        }

        private void Goto_Convertor_Button_Click(object sender, RoutedEventArgs e) => GoToPage("Convertor");
        private void Goto_Calculator_Button_Click(object sender, RoutedEventArgs e) => GoToPage("Calculator");
        private void Goto_DoubleParse_Button_Click(object sender, RoutedEventArgs e) => GoToPage("DoubleParse");
        private void Goto_MachineCalculator_Button_Click(object sender, RoutedEventArgs e) => GoToPage("MachineCodeCalc");


        private readonly Dictionary<string, Page> PagesDictionary = new Dictionary<string, Page>
        {
            { "Convertor", new ConvertorPage() },
            { "Calculator", new CalculatorPage() },
            { "DoubleParse", new DoubleParsePage() },
            { "MachineCodeCalc", new MachineCalculatorPage() },
        };

        
        private void GoToPage(string page_name)
        {
            PagesDictionary.TryGetValue(page_name, out Page page);

            if (MainFrame.Content?.GetType() != page?.GetType())
            {
                MainFrame.NavigationService.RemoveBackEntry();
                MainFrame.Content = page;
            }
        }

        
    }
}
