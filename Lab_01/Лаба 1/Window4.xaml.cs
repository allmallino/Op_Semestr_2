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
using System.Threading;

namespace Лаба_1
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        private BitmapImage[] images = new BitmapImage[5];
        private int num = 0;
        public Window4()
        {
            InitializeComponent();
            images[0] = new BitmapImage(new Uri("photo_2021-12-15_13-28-08.jpg", UriKind.Relative));
            images[1] = new BitmapImage(new Uri("photo_2021-10-10_22-22-26.jpg", UriKind.Relative));
            images[2] = new BitmapImage(new Uri("photo_2021-08-24_21-07-59.jpg", UriKind.Relative));
            images[3] = new BitmapImage(new Uri("photo_2021-03-28_20-31-22.jpg", UriKind.Relative));
            images[4] = new BitmapImage(new Uri("photo_2021-03-08_10-37-20.jpg", UriKind.Relative));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            num = (num + 4) % 5;
            me.Source = images[num];
            PhotoNum.Content = $"{num + 1}/5";
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            num = (num + 1) % 5;
            me.Source = images[num];
            PhotoNum.Content = $"{num + 1}/5";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }
    }
}
