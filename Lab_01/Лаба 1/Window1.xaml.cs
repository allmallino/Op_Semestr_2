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

namespace Лаба_1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
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
            StreamReader rdr = new StreamReader("Student.txt");
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
            StreamWriter wrt = new StreamWriter("Student.txt");
            foreach (Student s in students)
            {
                wrt.Write(s.GetPIP() + "|" + s.GetNumZalik() + "|" + s.GetGroup()+"\n");
            }
            wrt.Close();
        }

        private List<Student> students = new List<Student>();

        public Window1()
        {
            InitializeComponent();
            students = readlist();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            foreach(Grid g in this.BaseGrid.Children.OfType<Grid>())
            {
                if (g.IsVisible)
                {
                    g.Visibility = Visibility.Hidden;
                    break;
                }
            }
            ((Button)e.Source).Visibility = Visibility.Hidden;
            DefaultElements.Visibility = Visibility.Visible;
            CapLable.Content = "Меню";
        }

        
        private void AddBtn(object sender, RoutedEventArgs e)
        {
            DefaultElements.Visibility = Visibility.Hidden;
            BackBtn.Visibility = Visibility.Visible;
            CapLable.Content = "Добавить студента";
            AddElements.Visibility = Visibility.Visible;
        }

        private void ShowBtn(object sender, RoutedEventArgs e)
        {
            
            DefaultElements.Visibility = Visibility.Hidden;
            BackBtn.Visibility = Visibility.Visible;
            CapLable.Content = "Список студентов";
            ShowElements.Visibility = Visibility.Visible;
            StudList.Items.Clear();
            int i = 0;
            foreach (Student s in students)
            {
                i++;
                StudList.Items.Add(i + "] " + s.GetPIP() + ",\t" + s.GetNumZalik() + ",\t" + s.GetGroup());
            }
            if (i == 0)
                StudList.Items.Add("В базу данных студентов занесено не было");
            
        }

        private void DelBtn(object sender, RoutedEventArgs e)
        {
            DefaultElements.Visibility = Visibility.Hidden;
            BackBtn.Visibility = Visibility.Visible;
            CapLable.Content = "Удалить студента";
            DeleteElements.Visibility = Visibility.Visible;
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
            if(AddNum.Text.Length!=0 && AddLastName.Text.Length != 0 && AddSurname.Text.Length != 0 && AddName.Text.Length != 0 && AddGroup.Text.Length != 0)
            {
                foreach(Student s in students)
                {
                    if(s.GetNumZalik() == AddNum.Text)
                    {
                        MessageBox.Show("Номер зачетных книг не может повторяться");
                        return;
                    }
                }
                students.Add(new Student(AddSurname.Text + " " + AddName.Text +  " " + AddLastName.Text, AddNum.Text,AddGroup.Text));
                writelist();
                MessageBox.Show("Вы успешно добавили студента "+ AddSurname.Text + " " + AddName.Text + " " + AddLastName.Text);
                foreach(TextBox tt in AddElements.Children.OfType<TextBox>())
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
            if (PIPDelete.SelectedItem != null)
            {
                foreach(Student s in students)
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
            NumDelete.SelectedIndex = PIPDelete.SelectedIndex;
        }

        private void NumDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PIPDelete.SelectedIndex = NumDelete.SelectedIndex;
        }
    }
}
