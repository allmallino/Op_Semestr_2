using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Prac_2
{
    /// <summary>
    /// Логика взаимодействия для GreedAlgo.xaml
    /// </summary>
    public partial class GreedAlgo : Window
    {
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();
        static int CurrentCity;
        static double MinRoad = double.MaxValue;
        static int MinCity;
        static int CityCount = 1;
        static List<int> CityList = new List<int>();
        static List<int> OtherCityList = new List<int>();
        public GreedAlgo()
        {
            Random rnd = new Random();
            dT = new DispatcherTimer();
            CurrentCity = rnd.Next(PointCount);
            InitializeComponent();
            InitPoints();
            InitPolygon();

            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            OtherCityList.Clear();
            for (int i = 0; i < PointCount; i++)
            {
                if (i != CurrentCity)
                {
                    OtherCityList.Add(i);
                }
            }
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * GreedAlgoWind.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * GreedAlgoWind.Height - 3 * Radius));
                pC.Add(p);
            }

            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }
        }

        private void InitPolygon()
        {
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }

        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }


        private void PlotWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();

            for (int i = 0; i < BestWayIndex.Length; i++)
                Points.Add(pC[BestWayIndex[i]]);

            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }

        private void VelCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
        }

        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                dT.Stop();
                NumElemCB.IsEnabled = true;
            }
            else
            {
                NumElemCB.IsEnabled = false;
                dT.Start();
            }
        }

        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Random rnd = new Random();
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            CurrentCity = rnd.Next(0, PointCount);
            MinRoad = double.MaxValue;
            MinCity = CurrentCity;
            OtherCityList.Clear();
            CityCount = 1;
            for (int i = 0; i < PointCount; i++)
            {
                if (i != CurrentCity)
                {
                    OtherCityList.Add(i);
                }
            }
            CityList.Clear();
            CityList.Add(CurrentCity);
            InitPoints();
            InitPolygon();
        }
        private void OneStep(object sender, EventArgs e)
        {
            if (CityList.Count != PointCount)
            {
                MyCanvas.Children.Clear();
                PlotPoints();
                PlotWay(GetBestWay());
            }
        }

        private int[] GetBestWay()
        {
            Random rnd = new Random();
            int[] way = new int[CityCount + 1];
            for (int i = 0; i < CityCount; i++)
                way[i] = CityList[i];
            if (OtherCityList.Count == 0)
            {
                way[way.Length - 1] = MinCity;
                CityList.Add(MinCity);
                MinRoad = double.MaxValue;
                CityCount++;
                for (int i = 0; i < PointCount; i++)
                {
                    if (!CityList.Contains(i))
                    {
                        OtherCityList.Add(i);
                    }
                }
            }
            else
            {
                way[way.Length - 1] = OtherCityList[0];
                OtherCityList.RemoveAt(0);
                double length = Math.Sqrt(Math.Pow(pC[way[way.Length - 1]].X - pC[way[way.Length - 2]].X, 2) + Math.Pow(pC[way[way.Length - 1]].Y - pC[way[way.Length - 2]].Y, 2));
                if (length < MinRoad)
                {
                    MinRoad = length;
                    MinCity = way[way.Length - 1];
                }
            }

            return way;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }
    }
}
