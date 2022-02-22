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
    internal class Students
    {
        private TextBox[] textboxes = new TextBox[5];
        private Grid[] grids = new Grid[4];
        private Label CapLable = new Label();
        private Window window;
        struct Student
        {
            public Student(string PIP, string NumZalik, string Group)
            {
                this.PIP = PIP;
                this.NumZalik = NumZalik;
                this.Group = Group;
            }
            private string PIP;
            private string NumZalik;
            private string Group;

            public string GetPIP() => PIP;
            public string GetNumZalik() => NumZalik;
            public string GetGroup() => Group;

        }

        private List<Student> readlist()
        {
            List<Student> students = new List<Student>();
            StreamReader rdr = new StreamReader(@"Student.txt");
            string[] m = rdr.ReadToEnd().Split('\n');
            string[] s;
            for (int i = 0; i < m.Length; i++)
            {
                s = m[i].Split('|');
                try
                {
                    students.Add(new Student(s[0], s[1], s[2]));
                }
                catch { }

            }
            rdr.Close();
            return students;
        }

        private void writelist()
        {
            StreamWriter wrt = new StreamWriter(@"Student.txt");
            foreach (Student s in students)
            {
                wrt.Write(s.GetPIP() + "|" + s.GetNumZalik() + "|" + s.GetGroup() + "\n");
            }
            wrt.Close();
        }

        private List<Student> students = new List<Student>();

        public Students()
        {
            students = readlist();
            Create_Components();
        }
        private void Create_Components()
        {
            window = new Window();
            window.Show();
            window.Title = "Students Database";
            window.ResizeMode = ResizeMode.NoResize;
            window.Height = 395;
            window.Width = 830;
            window.Background = new SolidColorBrush(Color.FromRgb(247, 230, 198));
            Grid BaseGrid = new Grid();
            Button exitbtn = new Button();
            exitbtn.Content = "Хаб";
            exitbtn.Height = 40; exitbtn.Width = 60;
            exitbtn.Click += this.Button_Click;
            exitbtn.FontFamily = new FontFamily("Gabriola"); exitbtn.FontSize = 24;
            exitbtn.Margin = new Thickness(715, 280, 0, 0);
            exitbtn.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            exitbtn.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            exitbtn.BorderThickness = new Thickness(3);
            exitbtn.HorizontalAlignment = HorizontalAlignment.Left;
            exitbtn.VerticalAlignment= VerticalAlignment.Top;
            BaseGrid.Children.Add(exitbtn);
            CapLable.Name = "CapLable";
            CapLable.Content = "Меню";
            CapLable.Height = 87; CapLable.Width = 570;
            CapLable.Margin = new Thickness(-250, -290, 0, 0);
            CapLable.FontSize = 48; CapLable.FontFamily = new FontFamily("Gabriola");
            BaseGrid.Children.Add(CapLable);

            Grid DefaultElements = new Grid();
            DefaultElements.Name = "DefaultElements";
            Button BtnAddStud = new Button();
            BtnAddStud.Content = "Добавить";
            BtnAddStud.Height = 132; BtnAddStud.Width = 255;
            BtnAddStud.Click += AddBtn;
            BtnAddStud.FontFamily = new FontFamily("Gabriola"); BtnAddStud.FontSize = 36;
            BtnAddStud.Margin = new Thickness(-550, 0, 0, 0);
            BtnAddStud.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            BtnAddStud.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            BtnAddStud.BorderThickness = new Thickness(3);
            DefaultElements.Children.Add(BtnAddStud);
            Button BtnShowStud = new Button();
            BtnShowStud.Content = "Показать";
            BtnShowStud.Height = 132; BtnShowStud.Width = 255;
            BtnShowStud.Click += this.ShowBtn;
            BtnShowStud.FontFamily = new FontFamily("Gabriola"); BtnShowStud.FontSize = 36;
            BtnShowStud.Margin = new Thickness(-20, 0, 0, 0);
            BtnShowStud.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            BtnShowStud.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            BtnShowStud.BorderThickness = new Thickness(3);
            DefaultElements.Children.Add(BtnShowStud);
            Button BtnDelStud = new Button();
            BtnDelStud.Content = "Удалить";
            BtnDelStud.Height = 132; BtnDelStud.Width = 255;
            BtnDelStud.Click += this.DelBtn;
            BtnDelStud.FontFamily = new FontFamily("Gabriola"); BtnDelStud.FontSize = 36;
            BtnDelStud.Margin = new Thickness(520, 0, 0, 0);
            BtnDelStud.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            BtnDelStud.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            BtnDelStud.BorderThickness = new Thickness(3);
            DefaultElements.Children.Add(BtnDelStud);
            BaseGrid.Children.Add(DefaultElements);
            grids[0] = DefaultElements;

            Grid AddStudents = new Grid();
            AddStudents.Name = "AddElements";AddStudents.Visibility = Visibility.Hidden;
            Button add = new Button();
            add.Content = "Добавить";
            add.Name = "Add";
            add.Height = 53; add.Margin = new Thickness(553, 60, 0, 0);
            add.Width = 155; add.Click += Button_Click_Add;
            add.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            add.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            add.BorderThickness = new Thickness(3);
            AddStudents.Children.Add(add);
            string[] lables = new string[]{ "Имя", "Фамилия", "Отчество", "Номер зачетной книги", "Группа" };
            string[] lab = new string[] { "AddName", "AddSurname", "AddLastName", "AddNum", "AddGroup" };
            Thickness[] thicknesses = new Thickness[] {new Thickness(-572, -115, 0, 0), new Thickness(-104, -115, 0, 0), new Thickness(360, -115, 0, 0), new Thickness(-345, 32, 0, 0), new Thickness(100, 32, 0, 0) };
            for(int i = 0; i < lables.Length; i++)
            {
                Label label = new Label();
                label.Content = lables[i];
                label.Margin = thicknesses[i];
                label.Height = 30; label.Width = 200;
                label.FontSize = 18;
                label.FontFamily = new FontFamily("Times New Roman");
                AddStudents.Children.Add(label);
                TextBox textBox = new TextBox();
                textBox.Name = lab[i];
                textBox.FontSize = 20;
                textBox.Height=30;
                textBox.Width=200;
                textBox.Background = new SolidColorBrush(Colors.OldLace);
                textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
                textBox.BorderThickness = new Thickness(3);
                textBox.Margin=new Thickness(thicknesses[i].Left,thicknesses[i].Top+60, 0, 0);
                AddStudents.Children.Add(textBox);
                textboxes[i] = textBox;
            }
            BaseGrid.Children.Add(AddStudents);
            grids[1] = AddStudents;
            Button BackBtn = new Button();
            BackBtn.Name = "BackBtn";
            BackBtn.Width = 100;
            BackBtn.Height = 40;
            BackBtn.Margin = new Thickness(675, -300, 0, 0);
            BackBtn.Click += Button_Back;
            BackBtn.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            BackBtn.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            BackBtn.BorderThickness = new Thickness(3);
            BackBtn.FontFamily = new FontFamily("Gabriola");
            BackBtn.FontSize = 24;
            BackBtn.Visibility = Visibility.Hidden;
            BackBtn.Content = "Обратно";
            BaseGrid.Children.Add(BackBtn);

            Grid ShowElements = new Grid();
            ShowElements.Visibility = Visibility.Hidden;
            ListBox StudList = new ListBox();
            StudList.Height = 230;
            StudList.Margin = new Thickness(30, 71, 40, 0);
            StudList.FontSize = 22;
            StudList.FontFamily = new FontFamily("Georgia");
            StudList.Background = new SolidColorBrush(Color.FromRgb(250, 244, 233));
            StudList.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            StudList.BorderThickness = new Thickness(3);
            ShowElements.Children.Add(StudList);
            grids[2] = ShowElements;
            BaseGrid.Children.Add(ShowElements);

            Grid DeleteElements = new Grid();
            DeleteElements.Visibility = Visibility.Hidden;
            Button ButtonDelete = new Button();
            ButtonDelete.Name = "DeleteConfirm";
            ButtonDelete.Content = "Удалить";
            ButtonDelete.Height = 50;
            ButtonDelete.Margin = new Thickness(453, 100, 0, 0);
            ButtonDelete.Width = 241;
            ButtonDelete.Click += DeleteConfirm_Click;
            ButtonDelete.Background = new SolidColorBrush(Color.FromRgb(253, 221, 160));
            ButtonDelete.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            ButtonDelete.BorderThickness = new Thickness(3);
            ButtonDelete.FontSize = 24;
            ButtonDelete.FontFamily = new FontFamily("Gabriola");
            DeleteElements.Children.Add(ButtonDelete);
            Label label1 = new Label();
            label1.Content = "Номер книги";
            label1.Height = 31;
            label1.Margin = new Thickness(453, -80, 0, 0);
            label1.Width = 241;
            label1.FontFamily = new FontFamily("Times New Roman");
            label1.FontSize = 22;
            Label label2 = new Label();
            label2.Content = "ФИО";
            label2.Height = 31;
            label2.Margin = new Thickness(-535, -80, 0, 0);
            label2.Width = 241;
            label2.FontFamily = new FontFamily("Times New Roman");
            label2.FontSize = 22;
            DeleteElements.Children.Add(label2);
            DeleteElements.Children.Add(label1);
            ComboBox PIPDelete = new ComboBox();
            PIPDelete.Name = "PIPDelete";
            PIPDelete.Height = 50;
            PIPDelete.Margin = new Thickness(-335,0,0,0);
            PIPDelete.Width = 437;
            PIPDelete.FontSize = 24;
            PIPDelete.SelectionChanged += PIPDelete_SelectionChanged;
            PIPDelete.FontFamily = new FontFamily("Times New Roman");
            PIPDelete.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            PIPDelete.BorderThickness = new Thickness(2);
            ComboBox NumDelete = new ComboBox();
            NumDelete.Name = "NumDelete";
            NumDelete.Height = 50;
            NumDelete.Margin = new Thickness(453, 0, 0, 0);
            NumDelete.Width = 240;
            NumDelete.FontSize = 24;
            NumDelete.SelectionChanged += NumDelete_SelectionChanged;
            NumDelete.FontFamily = new FontFamily("Times New Roman");
            NumDelete.BorderBrush = new SolidColorBrush(Color.FromRgb(243, 192, 14));
            NumDelete.BorderThickness = new Thickness(2);
            DeleteElements.Children.Add(NumDelete);
            DeleteElements.Children.Add(PIPDelete);
            grids[3] = DeleteElements;
            BaseGrid.Children.Add(DeleteElements);
            window.Content = BaseGrid;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            window.Close();
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            foreach (Grid g in ((Grid)window.Content).Children.OfType<Grid>())
            {
                if (g.IsVisible)
                {
                    g.Visibility = Visibility.Hidden;
                    break;
                }
            }
            ((Button)e.Source).Visibility = Visibility.Hidden;
            Grid DefaultElements=grids[0];
            DefaultElements.Visibility = Visibility.Visible;
            CapLable.Content = "Меню";
        }
        private void AddBtn(object sender, RoutedEventArgs e)
        {
            grids[0].Visibility = Visibility.Hidden;
            foreach(Button BackBtn in ((Grid)window.Content).Children.OfType<Button>())
            {
                if(BackBtn.Name == "BackBtn")
                {
                    BackBtn.Visibility = Visibility.Visible;
                    break;
                }
            }
            CapLable.Content = "Добавить студента";
            grids[1].Visibility = Visibility.Visible;
        }

        private void ShowBtn(object sender, RoutedEventArgs e)
        {

            grids[0].Visibility = Visibility.Hidden;
            foreach (Button BackBtn in ((Grid)window.Content).Children.OfType<Button>())
            {
                if (BackBtn.Name == "BackBtn")
                {
                    BackBtn.Visibility = Visibility.Visible;
                    break;
                }
            }
            CapLable.Content = "Список студентов";
            grids[2].Visibility = Visibility.Visible;
            ListBox StudList = new ListBox();
            foreach (ListBox ls in grids[2].Children.OfType<ListBox>())
            {
                StudList = ls;
            }
            StudList.Items.Clear();
            int i = 0;
            foreach (Student s in students)
            {
                i++;
                StudList.Items.Add(i + "] " + s.GetPIP() + "\t" + s.GetNumZalik() + "\t" + s.GetGroup());
            }
            if (i == 0)
                StudList.Items.Add("В базу данных студентов занесено не было");

        }
        private void DelBtn(object sender, RoutedEventArgs e)
        {
            grids[0].Visibility = Visibility.Hidden;
            foreach (Button BackBtn in ((Grid)window.Content).Children.OfType<Button>())
            {
                if (BackBtn.Name == "BackBtn")
                {
                    BackBtn.Visibility = Visibility.Visible;
                    break;
                }
            }
            CapLable.Content = "Удалить студента";
            grids[3].Visibility = Visibility.Visible;
            ComboBox PIPDelete=null, NumDelete=null;
            foreach(ComboBox cb in grids[3].Children.OfType<ComboBox>())
            {
                if(cb.Name == "PIPDelete")
                {
                    PIPDelete = cb;
                }else if(cb.Name == "NumDelete")
                {
                    NumDelete = cb;
                }
            }
            PIPDelete.Items.Clear();
            NumDelete.Items.Clear();
            foreach (Student s in students)
            {
                PIPDelete.Items.Add(s.GetPIP());
                NumDelete.Items.Add(s.GetNumZalik());
            }

        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (textboxes[0].Text.Length != 0 && textboxes[1].Text.Length != 0 && textboxes[2].Text.Length != 0 && textboxes[3].Text.Length != 0 && textboxes[4].Text.Length != 0)
            {
                foreach (Student s in students)
                {
                    if (s.GetNumZalik() == textboxes[3].Text)
                    {
                        MessageBox.Show("Номер зачетных книг не может повторяться");
                        return;
                    }
                }
                students.Add(new Student(textboxes[1].Text + " " + textboxes[0].Text + " " + textboxes[2].Text, textboxes[3].Text, textboxes[4].Text));
                writelist();
                MessageBox.Show("Вы успешно добавили студента");
                foreach (TextBox tt in grids[1].Children.OfType<TextBox>())
                {
                    tt.Text = null;
                }
            }
            else
            {
                MessageBox.Show("Вы заполнили не все поля");
            }
        }
        private void DeleteConfirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBox PIPDelete = null, NumDelete = null;
            foreach (ComboBox cb in grids[3].Children.OfType<ComboBox>())
            {
                if (cb.Name == "PIPDelete")
                {
                    PIPDelete = cb;
                }
                else if (cb.Name == "NumDelete")
                {
                    NumDelete = cb;
                }
            }
            if (PIPDelete.SelectedItem != null)
            {
                foreach (Student s in students)
                {
                    if (s.GetPIP() == (string)PIPDelete.SelectedItem)
                    {
                        students.Remove(s);
                        break;
                    }
                }
                MessageBox.Show("Студент " + (string)PIPDelete.SelectedItem + " был успешно удален");
                PIPDelete.SelectedIndex = -1;
                NumDelete.SelectedIndex = -1;
                PIPDelete.Items.Clear();
                NumDelete.Items.Clear();
                foreach (Student s in students)
                {
                    PIPDelete.Items.Add(s.GetPIP());
                    NumDelete.Items.Add(s.GetNumZalik());
                }
                writelist();
            }
            else
            {
                MessageBox.Show("Выберите студента");
            }
        }

        private void PIPDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox PIPDelete = null, NumDelete = null;
            foreach (ComboBox cb in grids[3].Children.OfType<ComboBox>())
            {
                if (cb.Name == "PIPDelete")
                {
                    PIPDelete = cb;
                }
                else if (cb.Name == "NumDelete")
                {
                    NumDelete = cb;
                }
            }
            NumDelete.SelectedIndex = PIPDelete.SelectedIndex;
        }

        private void NumDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox PIPDelete = null, NumDelete = null;
            foreach (ComboBox cb in grids[3].Children.OfType<ComboBox>())
            {
                if (cb.Name == "PIPDelete")
                {
                    PIPDelete = cb;
                }
                else if (cb.Name == "NumDelete")
                {
                    NumDelete = cb;
                }
            }
            PIPDelete.SelectedIndex = NumDelete.SelectedIndex;
        }
    }
}
