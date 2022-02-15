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
using System.Windows.Shapes;
using System.IO;

namespace Практика_1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private int Tries = 3;
        private int TryNow = 1;
        private List<double>[] y;
        private DateTime t = new DateTime(1);

        public Window2()
        {
            InitializeComponent();
            y = new List<double>[Tries];
            for(int i =0; i < Tries; i++)
            {
                y[i] = new List<double>();
            }
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }

        private void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (TryNow <= Tries)
            {
                try
                {
                    if (InputField.Text == VerifField.Text)
                    {
                        InputField.Clear();
                        SymbolCount.Content = "0";
                        DateTime dt = DateTime.Now;
                        TimeSpan time = dt - t;
                        y[TryNow - 1].Add(Math.Round(Convert.ToDouble(time.ToString(@"ss\,ffff")), 4));
                        string b = "";
                        for (int i = 0; i < VerifField.Text.Length - 1; i++)
                        {
                            b += y[TryNow - 1][i] + " ";
                        }
                        Lb.Content = b;
                        t = new DateTime(1);
                        TryNow++;
                        TryField.Content = "Спроба №" + TryNow + "/" + Tries;
                    }
                    else if (InputField.Text[InputField.Text.Length - 1] == VerifField.Text[InputField.Text.Length - 1])
                    {
                        if (t == new DateTime(1))
                        {
                            t = DateTime.Now;
                        }
                        else
                        {
                            DateTime dt = DateTime.Now;
                            TimeSpan time = dt - t;
                            y[TryNow - 1].Add(Math.Round(Convert.ToDouble(time.ToString(@"ss\,ffff")), 4));
                            t = dt;
                        }
                        SymbolCount.Content = InputField.Text.Length;
                    }
                    else
                    {
                        MessageBox.Show("Помилка в написанні слова. Спробуйте написати його знову.");
                        InputField.Clear();
                        y[TryNow - 1].Clear();
                        SymbolCount.Content = "0";
                        t = new DateTime(1);
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка в написанні слова. Спробуйте написати його знову.");
                    InputField.Clear();
                    y[TryNow - 1].Clear();
                    SymbolCount.Content = "0";
                    t = new DateTime(1);
                }
            }
            
            if(TryNow>Tries)
            {
                double sum, M, S, Tp;
                List<double> k;
                for (int i = 0; i < y.Length; i++)
                {
                    for(int j=0; j < y[i].Count; j++)
                    {
                        k = y[i];
                        k.RemoveAt(j);
                        sum = 0;
                        for (int n = 0; n < k.Count; n++)
                        {
                            sum += k[n];
                        }
                        M = sum /k.Count;
                        sum = 0;
                        for (int n = 0; n < k.Count; n++)
                        {
                            sum += Math.Pow(k[n] - M, 2);
                        }
                        S = Math.Sqrt(sum / (k.Count - 1));
                        Tp = Math.Abs((y[i][j] - M) / (S / Math.Sqrt(k.Count)));
                        if (Tp > 2.31)
                        {
                            y[i].RemoveAt(j);
                            j--;
                        }
                    }
                }
                string s = "";
                StreamWriter wr = new StreamWriter("Example.txt");
                sum = 0;
                for (int i = 0; i < y.Length; i++)
                {
                    sum = 0;
                    for (int j = 0; j < y[i].Count; j++)
                    {
                        sum += y[i][j];
                    }
                    M = sum /y[i].Count;
                    sum = 0;
                    for (int j = 0; j < y[i].Count; j++)
                    {
                        sum += Math.Pow(y[i][j]-M, 2);
                    }
                    S = sum;
                    s += M + " " + S + "\n";
                }
                s=s.Remove(s.Length-1);
                wr.Write(s);
                wr.Close();
                SymbolCount.Content = "0";
                TryField.Content = "Спроби закінчилися";
                InputField.IsEnabled = false;
                MessageBox.Show("Програма навчена, якщо бажаєте перезаписати - перезайдіть, або змініть кількість способів");
            }
        }

        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int[] Nums = { 3, 4, 5, 6, 7 };
            TryField.Content = "Спроба № 1/" + Nums[CountProtection.SelectedIndex];
            TryNow = 1;
            Tries = Nums[CountProtection.SelectedIndex];
            SymbolCount.Content = "0";
            InputField.Clear();
            InputField.IsEnabled = true;
            t = new DateTime(1);
            y = new List<double>[Tries];
            for (int i = 0; i < Tries; i++)
            {
                y[i] = new List<double>();
            }
        }
    }
}
