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

namespace Lab_04
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        SqlCommand cmd;
        int index;
        SqlDataAdapter adapter;
        string[,] myCommands = new string[3,3];
        string spCmd;
        public MainWindow()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conn.Open();
            InitializeComponent();
            myCommands[0, 0] = "select |dbo.[Group].Name| AS [Group], |dbo.Music.Name| AS [Music name], dbo.Music.RecCondition As [Recorded at] FROM dbo.[Group] INNER JOIN dbo.MusicGroup ON dbo.[Group].IDGroup = dbo.MusicGroup.IDGroup INNER JOIN dbo.Music ON dbo.MusicGroup.IDMusic = dbo.Music.IDMusic GROUP BY dbo.[Group].Name, dbo.Music.Name,dbo.Music.RecCondition";
            myCommands[0, 1] = "select |dbo.Music.Name| AS [Music name], |dbo.Cover.Name| AS [Cover], dbo.Cover.N_All_Time AS [Sells] FROM dbo.Music INNER JOIN  dbo.Music_Cover ON dbo.Music.IDMusic = dbo.Music_Cover.IDMusic INNER JOIN dbo.Cover ON dbo.Music_Cover.IDCover = dbo.Cover.IDCover GROUP BY dbo.Cover.Name, dbo.Music.Name, dbo.Cover.N_All_Time";
            myCommands[0, 2] = "select |dbo.Music.Name| AS [Music name], |dbo.Genre.Name| AS [Genre] FROM dbo.Music INNER JOIN dbo.MusicGenre ON dbo.Music.IDMusic = dbo.MusicGenre.IDMusic INNER JOIN dbo.Genre ON dbo.MusicGenre.IDGenre = dbo.Genre.IDGenre GROUP BY dbo.Genre.Name, dbo.Music.Name";
            myCommands[1, 0] = "select dbo.Musician.Name As [Name], |dbo.Musician.Surname| As [Surname], |dbo.[Group].Name| AS [Group] FROM dbo.Musician INNER JOIN dbo.[Group] ON dbo.[Group].IDGroup = dbo.Musician.IDGroup GROUP BY dbo.[Group].Name, dbo.Musician.Name, dbo.Musician.Surname";
            myCommands[1, 1] = "select dbo.Musician.Name As [Name], |dbo.Musician.Surname| As [Surname], |dbo.Instrument.Name| AS [Instrument] FROM dbo.Musician INNER JOIN dbo.MusicianInstrument ON dbo.Musician.IDMusician = dbo.MusicianInstrument.IDMusician INNER JOIN dbo.Instrument ON dbo.MusicianInstrument.IDInstrument = dbo.Instrument.IDInstrument GROUP BY dbo.Instrument.Name, dbo.Musician.Name, dbo.Musician.Surname";
            myCommands[1, 2] = "select dbo.Musician.Name As [Name], |dbo.Musician.Surname| As [Surname], |dbo.Occupation.Name| AS [Occupation] FROM dbo.Musician INNER JOIN dbo.MusicianOccupation ON dbo.Musician.IDMusician = dbo.MusicianOccupation.IDMusician INNER JOIN dbo.Occupation ON dbo.MusicianOccupation.IDOccupation = dbo.Occupation.IDOccupation GROUP BY dbo.Occupation.Name, dbo.Musician.Name, dbo.Musician.Surname";
            myCommands[2, 0] = "select |dbo.Cover.Name| AS [Cover], dbo.Music.Name As [Music], |dbo.[Group].Name| As [Group] From dbo.Music INNER JOIN dbo.Music_Cover ON dbo.Music.IDMusic = dbo.Music_Cover.IDMusic INNER JOIN dbo.Cover ON dbo.Cover.IDCover=dbo.Music_Cover.IDCover INNER JOIN dbo.MusicGroup on dbo.MusicGroup.IDMusic = dbo.Music.IDMusic INNER JOIN dbo.[Group] on dbo.[Group].IDGroup = dbo.MusicGroup.IDGroup Group by dbo.Cover.Name, dbo.Music.Name, dbo.[Group].Name";
            myCommands[2, 1] = "select |dbo.Cover.Name| as [Cover], dbo.Plate.Release_Time As [Was released], dbo.Plate.N_In_Stock as [In stock], |dbo.Company.Name| as [Company] from dbo.Cover INNER JOIN dbo.Plate on dbo.Plate.IDCover = dbo.Cover.IDCover Inner Join dbo.Company on dbo.Company.IDCompany = dbo.Plate.IDCompany_Creator Group by dbo.Cover.Name, dbo.Plate.Release_Time, dbo.Plate.N_In_Stock, dbo.Company.Name";
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tableWindow.Visibility = Visibility.Hidden;
            tableTypeList.SelectionChanged-=tableTypeList_SelectionChanged;
            tableTypeList.Items.Clear();
            tableFilterTypeList.Items.Clear();
        }

        private void tableTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable table = new DataTable();
            string[] entity = myCommands[index, tableTypeList.SelectedIndex].Split('|');
            spCmd = "";
            for(int i = 0; i < entity.Length; i++)
            {
                spCmd+=entity[i];
            }
            cmd = new SqlCommand(spCmd,conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            tableFilterTypeList.Items.Clear();
            tableFilterTypeList.Items.Add(entity[1]);
            tableFilterTypeList.Items.Add(entity[3]);
            tableFilterTypeList.SelectedIndex = 0;
            nameFilter.Text = "";
            mainDataGrid.ItemsSource = table.DefaultView;
            MessageBox.Show(table.Columns[0].ColumnName);
        }

        private void musicShowBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tableWindow.Visibility = Visibility.Visible;
            index = 0;
            tableTypeList.Items.Add("Music-Group");
            tableTypeList.Items.Add("Music-Cover");
            tableTypeList.Items.Add("Music-Genre");
            tableTypeList.SelectedIndex = 0;
            DataTable table = new DataTable();
            string[] entity = myCommands[index, tableTypeList.SelectedIndex].Split('|');
            spCmd = "";
            for (int i = 0; i < entity.Length; i++)
            {
                spCmd += entity[i];
            }
            cmd = new SqlCommand(spCmd, conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            tableFilterTypeList.Items.Add(entity[1]);
            tableFilterTypeList.Items.Add(entity[3]);
            tableFilterTypeList.SelectedIndex = 0;
            nameFilter.Text = "";
            mainDataGrid.ItemsSource = table.DefaultView;
            tableTypeList.SelectionChanged += tableTypeList_SelectionChanged;
        }

        private void nameFilter_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable table = new DataTable();
            cmd = new SqlCommand(spCmd+ " Having ("+tableFilterTypeList.Text+" Like '"+ nameFilter.Text+"%')", conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            mainDataGrid.ItemsSource = table.DefaultView;
        }

        private void musicianShowBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tableWindow.Visibility = Visibility.Visible;
            index = 1;
            tableTypeList.Items.Add("Musician-Group");
            tableTypeList.Items.Add("Musician-Instrument");
            tableTypeList.Items.Add("Musician-Occupation");
            tableTypeList.SelectedIndex = 0;
            DataTable table = new DataTable();
            string[] entity = myCommands[index, tableTypeList.SelectedIndex].Split('|');
            spCmd = "";
            for (int i = 0; i < entity.Length; i++)
            {
                spCmd += entity[i];
            }
            cmd = new SqlCommand(spCmd, conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            tableFilterTypeList.Items.Add(entity[1]);
            tableFilterTypeList.Items.Add(entity[3]);
            tableFilterTypeList.SelectedIndex = 0;
            nameFilter.Text = "";
            mainDataGrid.ItemsSource = table.DefaultView;
            tableTypeList.SelectionChanged += tableTypeList_SelectionChanged;
        }

        private void plateShowBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tableWindow.Visibility = Visibility.Visible;
            index = 2;
            tableTypeList.Items.Add("Plate-Music");
            tableTypeList.Items.Add("Plate-Company");
            tableTypeList.SelectedIndex = 0;
            DataTable table = new DataTable();
            string[] entity = myCommands[index, tableTypeList.SelectedIndex].Split('|');
            spCmd = "";
            for (int i = 0; i < entity.Length; i++)
            {
                spCmd += entity[i];
            }
            cmd = new SqlCommand(spCmd, conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            tableFilterTypeList.Items.Add(entity[1]);
            tableFilterTypeList.Items.Add(entity[3]);
            tableFilterTypeList.SelectedIndex = 0;
            nameFilter.Text = "";
            mainDataGrid.ItemsSource = table.DefaultView;
            tableTypeList.SelectionChanged += tableTypeList_SelectionChanged;
        }
    }
}
