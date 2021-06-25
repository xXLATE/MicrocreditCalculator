using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            this.SummaZaima.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
            this.SrokZaima.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
        }

        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Raschet_btn_Click(object sender, RoutedEventArgs e)
        {
            StackPanel daysStack = DaysStack;

            try
            {
                LoanTerm = Convert.ToInt32(SrokZaima.Text);
            }
            catch
            {
                MessageBox.Show("Введите сумму займа и кол-во дней");
                return;
            }

            if (SummaZaima.Text == "")
            {
                MessageBox.Show("Введите сумму займа");
                return;
            }
            if (Convert.ToDouble(SummaZaima.Text) > 500000)
            {
                MessageBox.Show("Сумма займа не может быть больше 500.000");
                return;
            }

            for (int i = 0; i < LoanTerm; i++)
            {
                daysStack.Children.Add(new TextBox { Name = "StavkaFields" });
            }
        }

        private void CalculateBid_Click(object sender, RoutedEventArgs e)
        {
            double[] bets = new double[LoanTerm];
            int[] persents = new int[LoanTerm];
            int[] sums = new int[LoanTerm];

            StackPanel daysStack = DaysStack;
            StackPanel persentsStack = PersentsStack;
            StackPanel sumStack = SumStack;

            int counter = 0;
            foreach (TextBox box1 in daysStack.Children)
            {
                if (box1.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
                if (Convert.ToDouble(box1.Text) > 1)
                {
                    MessageBox.Show("Принимаются значения не более 1%\nПример: 0,9");
                    return;
                }
                else
                {
                    bets[counter] = Convert.ToDouble(box1.Text);
                    counter++;
                }
            }

            try
            {
                persents[0] = Convert.ToInt32(bets[0] * 100);
            }
            catch
            {
                MessageBox.Show("Введите сумму займа и кол-во дней");
                return;
            }

            
            persentsStack.Children.Add(new TextBox { Text = Convert.ToString(persents[0]) });

            for (int i = 1; i < LoanTerm; i++)
            {
                persents[i] = Convert.ToInt32((bets[i] * 100) + persents[i - 1]);
                persentsStack.Children.Add(new TextBox { Text = Convert.ToString(persents[i]), IsReadOnly = true });
            }

            for (int i = 0; i < LoanTerm; i++)
            {
                sums[i] = persents[i] + Convert.ToInt32(SummaZaima.Text);
                sumStack.Children.Add(new TextBox { Text = Convert.ToString(sums[i]), IsReadOnly = true });
            }

            SummaViplati.Text = Convert.ToString(sums[LoanTerm - 1]);
            SummaProcentov.Text = Convert.ToString(persents[LoanTerm - 1]);
            Stavka.Text = Convert.ToString(Convert.ToDouble(persents[LoanTerm - 1]) / Convert.ToDouble(SummaZaima.Text) / Convert.ToDouble(LoanTerm) * 100) + "%";

            double maxSizePayment = (Convert.ToDouble(SummaZaima.Text) * 1.5) + Convert.ToDouble(SummaZaima.Text);

            if (Convert.ToDouble(SummaZaima.Text) > maxSizePayment)
            {
                MessageBox.Show("Размер выплаты превышает лимит");
                return;
            }


            //UIElement element = daysStack.Children[0];
            //TextBox myBox = (TextBox)element;

            //int counter = 0;
            //double previusVal = 0;
            //foreach (TextBox box1 in daysStack.Children)
            //{
            //    if (counter == 0)
            //    {
            //        string temp = Convert.ToString(Convert.ToDouble(box1.Text) * 100);
            //        persentsStack.Children.Add(new TextBox { Text = temp });
            //        previusVal = Convert.ToDouble(temp);
            //        counter++;
            //    }

            //    if ((counter != 0) && (counter < LoanTerm))
            //    {
            //        string temp = Convert.ToString(previusVal + Convert.ToDouble(box1.Text) * 100);
            //        persentsStack.Children.Add(new TextBox { Text = temp });
            //        previusVal = Convert.ToDouble(temp);
            //        counter++;
            //    }


            //double previusVal = 0;

            //if (counter == 0)
            //{
            //    previusVal = Convert.ToDouble(myBox.Text) * 100;
            //    persentsStack.Children.Add(new TextBox { Text = Convert.ToString(previusVal) });
            //    counter++;
            //}

            //if ((counter < LoanTerm-1) && (counter != 0))
            //{
            //    string temp = Convert.ToString(previusVal + Convert.ToDouble(box1.Text) * 100);
            //    previusVal = previusVal + Convert.ToDouble(box1.Text) * 100;
            //    persentsStack.Children.Add(new TextBox { Text = temp });
            //    counter++;
            //}
            // }
        }
    }
}
