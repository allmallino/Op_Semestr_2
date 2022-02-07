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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private Button[,] buttons = new Button[5, 5];
        private void WinOrNot ()//не работает
        {
            int EmptySlots = 0;
            for(int i =0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (!buttons[i, j].IsEnabled)
                    {
                        for(int n = 0; n < 3; n++)
                        {
                            for(int k = 0; k < 3; k++)
                            {
                                try
                                {
                                    for(int m = 1; m < 4; m++)
                                    {
                                        if (buttons[i, j].Content != buttons[i + m*(n - 1), j + m*(k - 1)].Content)
                                        {
                                            buttons[10,10] = new Button();
                                        }else if (n == 1 && k == 1)
                                        {
                                            buttons[10, 10] = new Button();
                                        }
                                    }
                                    gameText.Content = "Выиграл " + buttons[i, j].Content;
                                    foreach(Button b in GameField.Children.OfType<Button>())
                                    {
                                        b.IsEnabled = false;
                                    }
                                    NewGame.Visibility = Visibility.Visible;
                                    return;
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        EmptySlots++;
                    }
                }
            }
            if (EmptySlots == 0)
            {
                gameText.Content = "Ничья";
                NewGame.Visibility = Visibility.Visible;
            }
        }
        private Boolean xState = true;
        public Window2()
        {
            InitializeComponent();
            buttons[0, 0] = Btn11;
            buttons[0, 1] = Btn12;
            buttons[0, 2] = Btn13;
            buttons[0, 3] = Btn14;
            buttons[0, 4] = Btn15;
            buttons[1, 0] = Btn21;
            buttons[1, 1] = Btn22;
            buttons[1, 2] = Btn23;
            buttons[1, 3] = Btn24;
            buttons[1, 4] = Btn25;
            buttons[2, 0] = Btn31;
            buttons[2, 1] = Btn32;
            buttons[2, 2] = Btn33;
            buttons[2, 3] = Btn34;
            buttons[2, 4] = Btn35;
            buttons[3, 0] = Btn41;
            buttons[3, 1] = Btn42;
            buttons[3, 2] = Btn43;
            buttons[3, 3] = Btn44;
            buttons[3, 4] = Btn45;
            buttons[4, 0] = Btn51;
            buttons[4, 1] = Btn52;
            buttons[4, 2] = Btn53;
            buttons[4, 3] = Btn54;
            buttons[4, 4] = Btn55;
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
            if (xState)
            {
                btn.Content = "X";
                gameText.Content = "Сейчас ходит O";
            }
            else
            {
                btn.Content = "O";
                gameText.Content = "Сейчас ходит X";
            }
            
            btn.IsEnabled = false;
            xState = !xState;
            WinOrNot();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame.Visibility = Visibility.Hidden;
            foreach(Button b in GameField.Children.OfType<Button>())
            {
                b.IsEnabled = true;
                b.Content = null;
            }
            xState = true;
        }
    }
}
