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
    internal class TicTacToe
    {
        private Button[,] buttons = new Button[5, 5];
        Grid GameField;
        private void WinOrNot()
        {
            int EmptySlots = 0;
            Label gameText = null;
            foreach (Label g in ((Grid)window.Content).Children.OfType<Label>())
            {
                if (g.Name == "gameText")
                {
                    gameText = g;
                    break;
                }
            }
            Button NewGame = new Button();
            foreach(Button b in ((Grid)window.Content).Children.OfType<Button>())
            {
                if(b.Name == "NewGame")
                {
                    NewGame = b;
                    break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!buttons[i, j].IsEnabled)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                try
                                {
                                    for (int m = 1; m < 4; m++)
                                    {
                                        if (buttons[i, j].Content != buttons[i + m * (n - 1), j + m * (k - 1)].Content)
                                        {
                                            buttons[10, 10] = new Button();
                                        }
                                        else if (n == 1 && k == 1)
                                        {
                                            buttons[10, 10] = new Button();
                                        }
                                    }
                                    gameText.Content = "Выиграл " + buttons[i, j].Content;
                                    foreach (Button b in GameField.Children.OfType<Button>())
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
        private Window window;
        public TicTacToe()
        {
            Create_Components();
        }
        private void Create_Components()
        {
            window = new Window();
            window.Show();
            window.Title = "Tic-Tac-Toe";
            window.ResizeMode = ResizeMode.NoResize;
            window.Height = 615;
            window.Width = 645;
            window.Background = new SolidColorBrush(Color.FromRgb(133, 241, 255));
            Grid BaseGrid = new Grid();
            GameField = new Grid();
            GameField.Width = 400;
            GameField.Height = 400;
            GameField.ShowGridLines = true;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Click += Btn_Click;
                    buttons[i, j].Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    buttons[i, j].BorderThickness = new Thickness(0);
                }
            RowDefinition[] rows = new RowDefinition[5];
            ColumnDefinition[] cols = new ColumnDefinition[5];
            for (int i = 0; i < 5; i++)
            {
                rows[i] = new RowDefinition();
                GameField.RowDefinitions.Add(rows[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                cols[i] = new ColumnDefinition();
                GameField.ColumnDefinitions.Add(cols[i]);
            }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);
                }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    GameField.Children.Add(buttons[i, j]);
                }
            BaseGrid.Children.Add(GameField);
            Button Back_Btn = new Button { Content = "Хаб", Height = 45, Margin = new Thickness(450, 500, 0, 0), Width =120, FontFamily = new FontFamily("Segoe UI Black"), FontSize = 14, BorderBrush = new SolidColorBrush(Color.FromRgb(68, 105, 177)), BorderThickness = new Thickness(3), Background = new SolidColorBrush(Color.FromArgb(0,0,0,0)), Foreground = new SolidColorBrush(Color.FromRgb(68, 105, 177)), };
            Back_Btn.Click += Button_Click;
            BaseGrid.Children.Add(Back_Btn);
            Label gameText = new Label();
            gameText.Name = "gameText";
            gameText.Content = "Сейчас ходит X";
            gameText.Height = 40;
            gameText.Margin = new Thickness(-250, 500, 0, 0);
            gameText.Width = 361;
            gameText.FontSize = 22;
            gameText.FontFamily = new FontFamily("Segoe UI Black");
            gameText.Foreground = new SolidColorBrush(Color.FromRgb(68, 105, 177));
            BaseGrid.Children.Add(gameText);
            Button NewGame = new Button { Name = "NewGame", Content = "Новая игра", Height = 45, Margin = new Thickness(150, 500, 0, 0), Width = 130, FontFamily = new FontFamily("Segoe UI Black"), FontSize = 14, BorderBrush = new SolidColorBrush(Color.FromRgb(68, 105, 177)), BorderThickness = new Thickness(3), Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), Foreground = new SolidColorBrush(Color.FromRgb(68, 105, 177)), };
            NewGame.Click += NewGame_Click;
            NewGame.Visibility = Visibility.Hidden;
            BaseGrid.Children.Add(NewGame);
            Label TextUp = new Label();
            TextUp.Content = "Крестики-Нолики";
            TextUp.Height = 69;
            TextUp.Width = 640;
            TextUp.FontFamily = new FontFamily("Times New Roman");
            TextUp.FontSize = 48;
            TextUp.Margin = new Thickness(0, -525, 0, 0);
            TextUp.Foreground = new SolidColorBrush(Color.FromRgb(68, 105, 177));
            BaseGrid.Children.Add(TextUp);
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
            Label gameText = null;
            foreach (Label g in ((Grid)window.Content).Children.OfType<Label>())
            {
                if (g.Name == "gameText")
                {
                    gameText = g;
                    break;
                }
            }
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

            ((Button)e.Source).Visibility = Visibility.Hidden;
            for(int i =0;i<5;i++)
                for(int j = 0; j < 5; j++)
                {
                    buttons[i, j].Content = null;
                    buttons[i, j].IsEnabled = true;
                }
            xState = true;
        }
    }
    
}
