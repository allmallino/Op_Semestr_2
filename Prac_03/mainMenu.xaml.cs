using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Configuration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Threading;


namespace Prac03
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox userNickname;
        PasswordBox userPassword;
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter;
        Bitmap dogBitmap;
        BitmapSource _source;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void userBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userLoginWndw.Visibility = Visibility.Visible;
        }
        private void userLoginBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataTable dt = new DataTable();
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select [Password],[IsAdmin],[Block] from [User] where [Nickname]='" + userNickname.Text + "'", connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
                MessageBox.Show("We don't have any users with nikname like '"+userNickname.Text+"'");
            else
            {
                if (Convert.ToBoolean(dt.Rows[0][2]))
                    MessageBox.Show("USER PID BANOM");
                else
                {
                    if (userPassword.Password == dt.Rows[0][0].ToString())
                    {
                        if (Convert.ToBoolean(dt.Rows[0][1]))
                        {
                            MessageBox.Show("ADMIN MODE ON");
                            adminMenu menu = new adminMenu(userNickname.Text,connection);
                            menu.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Welcome '" + userNickname.Text + "'");
                            userMenu menu = new userMenu(userNickname.Text, connection);
                            menu.Show();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password is not correct");
                    }
                }
                
            }
            
        }
        private void userRegisterBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataTable dt = new DataTable();
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select * from [User] where [Nickname]='" + userNickname.Text + "'", connection);
            adapter = new SqlDataAdapter(command);
            connection.Close();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                if (restriction(userPassword.Password))
                {
                    if(userNickname.Text != "")
                    {
                        connection.Open();
                        command = new SqlCommand("insert into [User] values('" + userNickname.Text + "','" + userPassword.Password + "',NULL,NULL,0,0,0)", connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("User with nikname like '" + userNickname.Text + "' is succesfully added");
                        userMenu menu = new userMenu(userNickname.Text, connection);
                        menu.Show();
                        Close();
                    }

                }else
                        MessageBox.Show("Restricitions to the password is on.\nYour password must be bigger than 3, smaller than 11 and must contain '$'");

            }
            else
            {
                MessageBox.Show("We have already user with this nikname");
            }
            
        }
        bool restriction(string pass)
        {
            DataTable restrictions = new DataTable();
            command = new SqlCommand("select [Restriction] from [User] where [Nickname]= 'admin'", connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(restrictions);
            if (Convert.ToBoolean(restrictions.Rows[0][0]))
            {
                if(pass.Length>=4 && pass.Length<=10 && pass.Contains("$"))
                {
                    return true;
                }else
                    return false;
            }
            else
            {
                return true;
            }
        }
        private void borderOne_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userNickname = new TextBox();
            userPassword = new PasswordBox();
            Label userLoginText = new Label();
            userNickname.Name = "Nickname";
            userPassword.Name = "Password";
            userNickname.Background = null;
            userPassword.Background = null;
            userLoginText.Content = "Login";
            userPassword.FontSize = userNickname.FontSize = 28;
            borderOne.Width = borderOneShadow.Width = borderTwo.Width = borderTwoShadow.Width = 200;
            userNickname.Foreground = new SolidColorBrush(Colors.White);
            userPassword.Foreground = new SolidColorBrush(Colors.White);
            userNickname.TextAlignment = TextAlignment.Center;
            userPassword.VerticalAlignment = VerticalAlignment.Center;
            userLoginText.Foreground = new SolidColorBrush(Colors.White);
            borderOne.Child = userNickname;
            borderTwo.Child = userPassword;
            userLoginText.FontSize = 28;
            userLoginText.HorizontalAlignment = HorizontalAlignment.Center;
            userLoginBtn.Child = userLoginText;
            borderOne.MouseLeftButtonDown -= borderOne_MouseLeftButtonDown;
            borderTwo.MouseLeftButtonDown -= borderTwo_MouseLeftButtonDown;
            userLoginBtn.MouseLeftButtonDown += userLoginBtn_MouseLeftButtonDown;
            userLoginBtnShadow.Visibility = Visibility.Visible;
            userLoginBtn.Visibility = Visibility.Visible;
        }
        private void borderTwo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userNickname = new TextBox();
            userPassword = new PasswordBox();
            Label userLoginText = new Label();
            userLoginText.Content = "Register";
            userNickname.Name = "Nickname";
            userPassword.Name = "Password";
            userNickname.Background = null;
            userPassword.Background = null;
            userPassword.FontSize = userNickname.FontSize = 28;
            userNickname.Foreground = new SolidColorBrush(Colors.White);
            userPassword.Foreground = new SolidColorBrush(Colors.White);
            userNickname.TextAlignment = TextAlignment.Center;
            userPassword.VerticalAlignment = VerticalAlignment.Center;
            userLoginText.Foreground = new SolidColorBrush(Colors.White);
            borderOne.Child = userNickname;
            borderOne.Width = borderOneShadow.Width = borderTwo.Width = borderTwoShadow.Width = 200;
            borderTwo.Child = userPassword;
            userLoginText.FontSize = 28;
            userLoginText.HorizontalAlignment = HorizontalAlignment.Center;
            userLoginBtn.Child = userLoginText;
            borderOne.MouseLeftButtonDown -= borderOne_MouseLeftButtonDown;
            borderTwo.MouseLeftButtonDown -= borderTwo_MouseLeftButtonDown;
            userLoginBtn.MouseLeftButtonDown += userRegisterBtn_MouseLeftButtonDown;
            userLoginBtnShadow.Visibility = Visibility.Visible;
            userLoginBtn.Visibility = Visibility.Visible;
        }

        private void aboutBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            aboutWndw.Visibility = Visibility.Visible;
            _source = GetSource();
            dogGif.Source = _source;
            ImageAnimator.Animate(dogBitmap, OnFrameChanged);
        }

        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            aboutWndw.Visibility= Visibility.Hidden;
            ImageAnimator.StopAnimate(dogBitmap, OnFrameChanged);
        }
        private BitmapSource GetSource()
        {
            if (dogBitmap == null)
            {
                dogBitmap = new Bitmap("nose-fur.gif");
            }
            IntPtr handle = IntPtr.Zero;
            handle = dogBitmap.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(
                    handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        private void FrameUpdatedCallback()
        {
            ImageAnimator.UpdateFrames();
            if (_source != null)
                _source.Freeze();
            _source = GetSource();
            dogGif.Source = _source;
            InvalidateVisual();
        }
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                    new Action(FrameUpdatedCallback));
        }
        private void userBackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userLoginWndw.Visibility = Visibility.Hidden;
            borderOne.MouseLeftButtonDown += borderOne_MouseLeftButtonDown;
            borderTwo.MouseLeftButtonDown += borderTwo_MouseLeftButtonDown;
            userLoginBtn.MouseLeftButtonDown -= userLoginBtn_MouseLeftButtonDown;
            userLoginBtn.MouseLeftButtonDown -= userRegisterBtn_MouseLeftButtonDown;
            userLoginBtnShadow.Visibility = Visibility.Hidden;
            userLoginBtn.Visibility = Visibility.Hidden;
            Label login = new Label();
            Label register = new Label();
            login.HorizontalAlignment = HorizontalAlignment.Center;
            login.Foreground = new SolidColorBrush(Colors.White);
            login.Content = "Login";
            register.Content = "Register";
            register.FontSize = login.FontSize = 28;
            borderOne.Width = borderOneShadow.Width = borderTwo.Width = borderTwoShadow.Width = 150;
            register.HorizontalAlignment = HorizontalAlignment.Center;
            register.Foreground = new SolidColorBrush(Colors.White);
            borderOne.Child = login;
            borderTwo.Child = register;
            userLoginBtn.Child = null;
        }
    }
}
