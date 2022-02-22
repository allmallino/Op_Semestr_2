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
    internal class Author
    {
        private Window window;
        public Author()
        {
            Create_Components();
        }
        private void Create_Components()
        {
            window = new Window();
            window.Show();
            window.Title = "About Author";
            window.ResizeMode = ResizeMode.NoResize;
            window.Height = 382;
            window.Width = 800;
            window.Background = new SolidColorBrush(Color.FromRgb(0, 205, 255));
            Grid BaseGrid = new Grid();
            Label FirstLabel = new Label();
            FirstLabel.Content = "Автор вот этих всех костылей и неработающих идей:";
            FirstLabel.Height = 75;
            FirstLabel.Margin = new Thickness(-250, -280, 0, 0);
            FirstLabel.Width = 516;
            FirstLabel.FontSize = 20;
            Label SecondLabel = new Label { Content = "Александр Александрович Заварзин\nКП-12", Height = 88, Margin = new Thickness(-250, -200, 0, 0), Width = 511, FontSize = 30, FontFamily = new FontFamily("Impact") };
            Label ThirdLabel = new Label { Content = "Программа была создана в 2022 году", Height = 52, Margin = new Thickness(-375, 315, 0, -19.2), Width = 404, FontSize = 14, FontStyle = FontStyles.Italic };
            Button Back_Btn = new Button { Content = "Хаб", Height = 30, Margin = new Thickness(700, 315, 0, 0), Width = 50, FontFamily = new FontFamily("Impact"), FontSize = 18, BorderBrush = new SolidColorBrush(Colors.Black), BorderThickness = new Thickness(3), Background = new SolidColorBrush(Color.FromRgb(0, 232, 255)) };
            Back_Btn.Click += Button_Click;
            BaseGrid.Children.Add(FirstLabel);
            BaseGrid.Children.Add(SecondLabel);
            BaseGrid.Children.Add(ThirdLabel);
            BaseGrid.Children.Add(Back_Btn);
            window.Content = BaseGrid;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            window.Close();
        }
    }
}
