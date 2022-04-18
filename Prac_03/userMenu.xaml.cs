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

namespace Prac03
{
    /// <summary>
    /// Логика взаимодействия для userMenu.xaml
    /// </summary>
    public partial class userMenu : Window
    {
        SqlConnection connect=null;
        SqlCommand command = null;
        SqlDataAdapter adapter;
        DataTable table = null;
        string Login;
        public userMenu(string Login, SqlConnection connection)
        {
            this.Login = Login;
            connect=connection;
            InitializeComponent();
            table=new DataTable();
            UpdateTable();
            nameTb.Text = table.Rows[0][0].ToString();
            surnameTb.Text = table.Rows[0][1].ToString();
            secretLabel.Content = welcomeLabel.Content = "Welcome, " + Login;
        }
        void UpdateTable()
        {
            command = new SqlCommand("select [Name], [Surname], [Password], [IsAdmin]  from [User] where [Nickname]='" + Login + "'", connect);
            adapter = new SqlDataAdapter(command);
            table.Clear();
            adapter.Fill(table);
        }
        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if(newPasswordGrid.Visibility == Visibility.Visible)
            {
                newPasswordGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                connect.Close();
                if (Convert.ToBoolean(table.Rows[0][3]))
                {
                    adminMenu wind = new adminMenu(Login, connect);
                    wind.Show();
                }
                else
                {
                    MainWindow wind = new MainWindow();
                    wind.Show();
                }
                Close();
            }
            
        }
        bool restriction(string pass)
        {
            DataTable restrictions = new DataTable();
            command = new SqlCommand("select [Restriction] from [User] where [Nickname]= 'admin'", connect);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(restrictions);
            if (Convert.ToBoolean(restrictions.Rows[0][0]))
            {
                if (pass.Length >= 4 && pass.Length <= 10 && pass.Contains("$"))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return true;
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            newPasswordGrid.Visibility=Visibility.Visible;
        }
        
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            command = new SqlCommand("update [User] set [Name] ='" + nameTb.Text + "', [Surname]='"+surnameTb.Text+"' where [Nickname] = '" + Login + "'", connect);
            command.ExecuteNonQuery();
            UpdateTable();
        }

        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if(oldPasswordTb.Password == table.Rows[0][2].ToString())
            {
                if(newPasswordTb.Password == repeatNewPasswordTb.Password)
                {
                    if (restriction(newPasswordTb.Password))
                    {
                        command = new SqlCommand("update [User] set [Password] ='" + newPasswordTb.Password + "' where [Nickname] = '" + Login + "'", connect);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Password changed successfully");
                        newPasswordGrid.Visibility = Visibility.Hidden;
                        oldPasswordTb.Password = newPasswordTb.Password = repeatNewPasswordTb.Password = null;
                        UpdateTable();
                    }
                    else
                        MessageBox.Show("Restricitions to the password is on.\nYour password must be bigger than 3, smaller than 11 and must contain '$'");
                }
                else
                    MessageBox.Show("Different new passwords");
            }
            else
                MessageBox.Show("Incorrect password");
        }
    }
}
