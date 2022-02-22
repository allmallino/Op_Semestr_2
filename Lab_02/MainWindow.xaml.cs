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

namespace Лаба_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToWindow1_Click(object sender, RoutedEventArgs e)
        {
            Students wnd = new Students();
            Close();
        }

        private void ToWindow2_Click(object sender, RoutedEventArgs e)
        {
            TicTacToe wnd = new TicTacToe();
            Close();
        }

        private void ToWindow3_Click(object sender, RoutedEventArgs e)
        {
            Calculator wnd = new Calculator();
            Close();
        }

        private void ToInformation_Click(object sender, RoutedEventArgs e)
        {
            Author wnd = new Author();
            Close();
        }
    }
}
