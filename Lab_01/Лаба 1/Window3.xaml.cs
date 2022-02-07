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

namespace Лаба_1
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private string Znak;
        private double FirstNum;
        private double SecondNum;
        private bool State = false;
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;
            CalcText.Content = CalcText.Content + "" + btn.Content;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            CalcText.Content = null;
            Small_CalcText.Content = null;
            FirstNum = 0;
            SecondNum = 0;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CalcText.Content.ToString().Length!=0)
                CalcText.Content = CalcText.Content.ToString().Remove(CalcText.Content.ToString().Length-1);
        }

        private void BtnZnak_Click(object sender, RoutedEventArgs e)
        {
            Znak = ((Button)e.Source).Content+"";
            if (FirstNum == 0||!State)
                FirstNum = Convert.ToDouble(CalcText.Content);
            Small_CalcText.Content = FirstNum + " " + Znak;
            CalcText.Content = null;
            State = true;
        }

        private void BtnEqual_Click(object sender, RoutedEventArgs e)
        {
            if(SecondNum==0||State)
                SecondNum = Convert.ToDouble(CalcText.Content);
            Small_CalcText.Content = FirstNum + " " + Znak + " " + SecondNum + "=";
            switch (Znak)
            {
                case "-":
                    FirstNum = FirstNum - SecondNum;
                    break;
                case "+":
                    FirstNum = FirstNum + SecondNum;
                    break;
                case "×":
                    FirstNum = FirstNum * SecondNum;
                    break;
                case "÷":
                    FirstNum = FirstNum / SecondNum;
                    break;
            }
            CalcText.Content = FirstNum;
            State = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CalcText.Content = (-1 * Convert.ToDouble(CalcText.Content));
            }
            catch { }
        }
    }
}
