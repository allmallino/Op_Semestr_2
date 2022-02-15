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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private int Tries = 3;
        private int TryNow = 1;
        private List<double>[] y;
        private DateTime t = new DateTime(1);
        private double[,] OldMS;
        private int N0=0, N1=0, N2=0,r=0;
        private double[] P;

        public Window1()
        {
            InitializeComponent();
            y = new List<double>[Tries];
            for (int i = 0; i < Tries; i++)
            {
                y[i] = new List<double>();
            }
            StreamReader stream = new StreamReader("Example.txt");
            string[] Lines = stream.ReadToEnd().Split('\n');
            stream.Close();
            OldMS = new double[Lines.Length, 2];
            for (int i = 0; i < Lines.Length; i++)
            {
                OldMS[i, 0] = Convert.ToDouble((Lines[i].Split(' '))[0]);
                OldMS[i, 1] = Convert.ToDouble((Lines[i].Split(' '))[1]);
            }
            P = new double[Tries];
        }

        private void InputField_PreviewKeyUp(object sender, KeyEventArgs e)
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
                        t = new DateTime(1);
                        double sum = 0, M, Sx, S, b, p = 0,Tp;
                        List<double> k;
                        for (int j = 0; j < y[TryNow - 1].Count; j++)
                        {
                            k = y[TryNow - 1];
                            k.RemoveAt(j);
                            sum = 0;
                            for (int n = 0; n < k.Count; n++)
                            {
                                sum += k[n];
                            }
                            M = sum / k.Count;
                            sum = 0;
                            for (int n = 0; n < k.Count; n++)
                            {
                                sum += Math.Pow(k[n] - M, 2);
                            }
                            S = Math.Sqrt(sum / (k.Count - 1));
                            Tp = Math.Abs((y[TryNow - 1][j] - M) / (S/Math.Sqrt(k.Count)));
                            if (Tp > 2.31)
                            {
                                y[TryNow - 1].RemoveAt(j);
                                j--;
                            }
                        }
                        sum = 0;
                        for (int j = 0; j < y[TryNow - 1].Count; j++)
                        {
                            sum += y[TryNow - 1][j];
                        }
                        M = sum / y[TryNow - 1].Count;
                        sum = 0;
                        for (int j = 0; j < y[TryNow - 1].Count; j++)
                        {
                            sum += Math.Pow(y[TryNow - 1][j] - M, 2);
                        }
                        Sx = sum / (y[TryNow - 1].Count - 1);
                        for (int i = 0; i < OldMS.GetLength(0); i++)
                        {
                            if (Math.Max(Sx, OldMS[i, 1]) / Math.Min(Sx, OldMS[i, 1]) <= 3.79)
                            {
                                r++;
                            }
                            S = Math.Sqrt((Sx + OldMS[i, 1] / (y[TryNow - 1].Count - 1)) * (y[TryNow - 1].Count - 1) / (2 * y[TryNow - 1].Count - 1));
                            b = Math.Abs(OldMS[i, 0]-M) / (S * Math.Sqrt(2.0 / y[TryNow - 1].Count));
                            if (b > 2.36)
                            {
                                N1++;
                            }
                            else
                            {
                                p++;
                                N2++;
                            }
                            N0++;
                        }
                        P[TryNow - 1] = p / OldMS.GetLength(0);
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

            if (TryNow > Tries)
            {
                double ps=0;
                for(int i = 0; i < P.Length; i++)
                {
                    ps += P[i];
                }
                ps = ps / P.Length;
                StatisticsBlock.Content = ps;
                if (IsAuthor.IsChecked == true)
                {
                    P1Field.Content = N1 * 1.0 / N0;
                    P2Field.Content = null;
                }
                else
                {
                    P1Field.Content = null;
                    P2Field.Content = N2 * 1.0 / N0;
                }
                if (r >= N0 / 2.0)
                {
                    DispField.Content = "рівні";
                }
                else
                {
                    DispField.Content = "неоднорідні";
                }
                SymbolCount.Content = "0";
                TryField.Content = "Спроби закінчилися";
                InputField.IsEnabled = false;
                MessageBox.Show("Авторизація проведена, якщо бажаєте перерахувати - перезайдіть, або змініть кількість способів");
            }
        }

        private void IsAuthor_KeyUp(object sender, RoutedEventArgs e)
        {
            if (!InputField.IsEnabled)
            {
                if (IsAuthor.IsChecked == true)
                {
                    P1Field.Content = N1 * 1.0 / N0;
                    P2Field.Content = null;
                }
                else
                {
                    P1Field.Content = null;
                    P2Field.Content = N2 * 1.0 / N0;
                }
            }
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
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
            P = new double[Tries];
            t = new DateTime(1);
            y = new List<double>[Tries];
            for (int i = 0; i < Tries; i++)
            {
                y[i] = new List<double>();
            }
            N0 = 0;  N1 = 0; N2 = 0; r = 0;
    }
    }
}
