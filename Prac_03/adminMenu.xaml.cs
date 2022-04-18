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
    /// Логика взаимодействия для adminMenu.xaml
    /// </summary>
    public partial class adminMenu : Window
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter;
        DataTable table = null;
        string Login;
        int eNum = 0;
        DataTable restrictions = null;
        public adminMenu(string Login,SqlConnection connect)
        {
            this.Login = Login;
            connection = connect;
            table = new DataTable();
            restrictions = new DataTable();
            connection.Open();
            TableUpdate();
            InitializeComponent();
            secretLabel.Content=welcomeLabel.Content = "Welcome, "+Login;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                userLogins.Items.Add(table.Rows[i][0]);
            }
            if(table.Rows.Count > 0)
            {
                userLoginsLabel.Content = "Login: " + table.Rows[eNum][0];
                checkAdmin.IsChecked = Convert.ToBoolean(table.Rows[eNum][1]);
                checkBlocked.IsChecked = Convert.ToBoolean(table.Rows[eNum][2]);
                userLogins.SelectionChanged +=userLogins_SelectionChanged;
                userLogins.SelectedIndex = eNum;
                checkAdmin.Click += checkAdmin_Click;
                checkBlocked.Click += checkBlocked_Click;
                checkRestriction.Click+=checkRestriction_Click;
            }
            else
            {
                userLoginsLabel.Content = "There is no users :(";
            }
            checkRestriction.IsChecked = Convert.ToBoolean(restrictions.Rows[0][0]);
        }


        private void ConfigureThis_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            userMenu menu = new userMenu(Login, connection);
            menu.Show();
            Close();
        }

        private void configureOther_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            otherMenu.Visibility = Visibility.Visible;
            adminControlMenu.Visibility = Visibility.Hidden;
            adminControlMenuShadow.Visibility = Visibility.Hidden;
        }

        private void userLogins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            eNum = userLogins.SelectedIndex;
            userLoginsLabel.Content = "Login: " + table.Rows[eNum][0];
            checkAdmin.IsChecked = Convert.ToBoolean(table.Rows[eNum][1]);
            checkBlocked.IsChecked = Convert.ToBoolean(table.Rows[eNum][2]);
            checkRestriction.IsChecked = Convert.ToBoolean(table.Rows[eNum][3]);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            eNum=(eNum+userLogins.Items.Count-1)%userLogins.Items.Count;
            userLogins.SelectedIndex = eNum;
            userLoginsLabel.Content = "Login: " + table.Rows[eNum][0];
            checkAdmin.IsChecked = Convert.ToBoolean(table.Rows[eNum][1]);
            checkBlocked.IsChecked = Convert.ToBoolean(table.Rows[eNum][2]);
            checkRestriction.IsChecked = Convert.ToBoolean(table.Rows[eNum][3]);
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            eNum = (eNum + 1) % userLogins.Items.Count;
            userLogins.SelectedIndex = eNum;
            userLoginsLabel.Content = "Login: " + table.Rows[eNum][0];
            checkAdmin.IsChecked = Convert.ToBoolean(table.Rows[eNum][1]);
            checkBlocked.IsChecked = Convert.ToBoolean(table.Rows[eNum][2]);
            checkRestriction.IsChecked = Convert.ToBoolean(restrictions.Rows[0][0]);
        }
        void TableUpdate()
        {
            table.Rows.Clear();
            command = new SqlCommand("select [Nickname],[IsAdmin],[Block],[Restriction] from [User] where [Nickname]!= '" + Login + "' and [Nickname]!= 'admin'", connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            restrictions.Clear();
            command = new SqlCommand("select [Restriction] from [User] where [Nickname]= 'admin'", connection);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(restrictions);
        }
        private void checkAdmin_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand("update [User] set [IsAdmin] ='"+(Convert.ToInt32(table.Rows[eNum][1])+1)%2 +"' where [Nickname] = '" + table.Rows[eNum][0] + "'", connection);
            command.ExecuteNonQuery();
            TableUpdate();
        }

        private void checkBlocked_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand("update [User] set [Block] =" + (Convert.ToInt32(table.Rows[eNum][2]) + 1) % 2 + "  where [Nickname] = '" + table.Rows[eNum][0] + "'", connection);
            command.ExecuteNonQuery();
            TableUpdate();
        }

        private void checkRestriction_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand("update [User] set [Restriction] =" + (Convert.ToInt32(restrictions.Rows[0][0]) + 1) % 2 + " where [Nickname] = 'admin'", connection);
            command.ExecuteNonQuery();
            TableUpdate();
        }

        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if(adminControlMenu.Visibility== Visibility.Visible)
            {
                MainWindow wind = new MainWindow();
                wind.Show();
                Close();
            }
            else
            {
                otherMenu.Visibility = Visibility.Hidden;
                adminControlMenu.Visibility = Visibility.Visible;
                adminControlMenuShadow.Visibility = Visibility.Visible;
            }
            
        }
    }
}
