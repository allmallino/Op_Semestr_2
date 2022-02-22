using System;
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

namespace Лаба_2
{
    internal class Calculator
    {
        private string Znak;
        private double FirstNum;
        private double SecondNum;
        private bool State = false;
        private Window window;
        private Label CalcText;
        private Label Small_CalcText;
        public Calculator()
        {
            Create_Components();
        }
        private void Create_Components()
        {
            window = new Window();
            window.Show();
            window.Title = "Calculator";
            window.ResizeMode = ResizeMode.NoResize;
            window.Height = 500;
            window.Width = 430;
            Grid BaseGrid = new Grid();
            BaseGrid.Background = new SolidColorBrush(Colors.LightGray);
            Grid Numbers = new Grid();
            Numbers.Margin = new Thickness(0, 150, 0, 0);
            Numbers.Width = 400;
            Numbers.Height = 300;
            Numbers.ShowGridLines = true;
            Button[,] buttons = new Button[5, 4];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    buttons[i, j] = new Button();
                    //buttons[i, j].Click += Btn_Click;
                    buttons[i, j].Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    buttons[i, j].BorderThickness = new Thickness(0);
                }
            RowDefinition[] rows = new RowDefinition[5];
            ColumnDefinition[] cols = new ColumnDefinition[4];
            for (int i = 0; i < 5; i++)
            {
                rows[i] = new RowDefinition();
                Numbers.RowDefinitions.Add(rows[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                cols[i] = new ColumnDefinition();
                Numbers.ColumnDefinitions.Add(cols[i]);
            }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);
                }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    Numbers.Children.Add(buttons[i, j]);
                }
            for (int i = 1; i < 4; i++)
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Content = j + 1 + (i - 1) * 3;
                    buttons[i, j].Click += Btn_Click;
                }
            Numbers.Children.Remove(buttons[0, 0]);
            buttons[0, 1].Content = "=";
            buttons[0, 1].Click += BtnEqual_Click;
            buttons[0, 2].Content = "C";
            buttons[0, 2].Click += BtnClear_Click;
            buttons[0, 3].Content = "⌫";
            buttons[0,3].Click += BtnDelete_Click;
            string[] st = new string[4] { "÷", "×", "-", "+" };
            for(int i =0;i < st.Length; i++)
            {
                buttons[1+i,3].Content=st[i];
                buttons[1+i,3].Click += BtnZnak_Click;
            }
            buttons[4, 0].Content = "+/-";
            buttons[4, 0].Click += Button_Click_1;
            buttons[4, 1].Content = "0";
            buttons[4, 1].Click += Btn_Click; 
            buttons[4, 2].Content = ",";
            buttons[4, 2].Click += Btn_Click;
            BaseGrid.Children.Add(Numbers);
            CalcText = new Label();
            CalcText.Height = 56;
            CalcText.Margin = new Thickness(0, -210, 0, 0);
            CalcText.Width = 400;
            CalcText.Background = new SolidColorBrush(Colors.White);
            CalcText.FontSize = 36;
            CalcText.FontFamily = new FontFamily("Times New Roman");
            CalcText.HorizontalContentAlignment = HorizontalAlignment.Right;
            Small_CalcText = new Label();
            Small_CalcText.Height = 27;
            Small_CalcText.Margin = new Thickness(0, -270, 0, 0);
            Small_CalcText.Width = 400;
            Small_CalcText.Background = new SolidColorBrush(Colors.White);
            Small_CalcText.FontFamily = new FontFamily("Times New Roman");
            Small_CalcText.HorizontalContentAlignment = HorizontalAlignment.Right;
            BaseGrid.Children.Add(CalcText);
            BaseGrid.Children.Add(Small_CalcText);
            Label UpLabel = new Label() { Content = "Калькулятор", Height = 69, Margin = new Thickness(-75,-400,0,0), Width = 341, FontSize = 24, FontFamily = new FontFamily("Segoe UI Black") };
            Button BackButton = new Button() { Content = "Хаб", Height = 30, Margin = new Thickness(350, -410, 0, 0), Width = 50, Background = new SolidColorBrush(Colors.White), FontFamily = new FontFamily("Segoe UI Black"), FontSize = 18 };
            BackButton.Click += Button_Click;
            BaseGrid.Children.Add(UpLabel);
            BaseGrid.Children.Add(BackButton);
            window.Content = BaseGrid;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            window.Close();
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
            if (CalcText.Content.ToString().Length != 0)
                CalcText.Content = CalcText.Content.ToString().Remove(CalcText.Content.ToString().Length - 1);
        }

        private void BtnZnak_Click(object sender, RoutedEventArgs e)
        {
            Znak = ((Button)e.Source).Content + "";
            if (FirstNum == 0 || !State)
                FirstNum = Convert.ToDouble(CalcText.Content);
            Small_CalcText.Content = FirstNum + " " + Znak;
            CalcText.Content = null;
            State = true;
        }

        private void BtnEqual_Click(object sender, RoutedEventArgs e)
        {
            if (SecondNum == 0 || State)
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
