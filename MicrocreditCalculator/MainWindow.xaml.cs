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

namespace MicrocreditCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int LoanTerm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Raschet_btn_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = DaysStack;

            LoanTerm = Convert.ToInt32(SrokZaima.Text);
            for (int i = 0; i < LoanTerm; i++)
            {
                stackPanel.Children.Add(new TextBox { Name = "field_1" });
            }
        }
    }
}
