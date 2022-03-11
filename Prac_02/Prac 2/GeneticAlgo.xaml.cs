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
    /// Логика взаимодействия для GeneticAlgo.xaml
    /// </summary>
    public partial class GeneticAlgo : Window
    {
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();
        static int PopulationCount = 5;
        static int[][] PopulationChromosomes = new int[2 * PopulationCount][];
        static double PersMutation = 0.7;
        struct Roads
        {
            private double Length;
            private int Index;
            public Roads(double Length, int Index)
            {
                this.Length = Length;
                this.Index = Index;
            }
            public double GetLength() => Length;
            public int GetIndex() => Index;
        }
        public GeneticAlgo()
        {
            Random rnd = new Random();
            dT = new DispatcherTimer();

            InitializeComponent();
            InitPoints();
            InitPolygon();
            for (int i = 0; i < PopulationCount; i++)
            {
                PopulationChromosomes[i] = new int[PointCount];
            }
            for (int i = 0; i < PopulationCount; i++)
            {
                for (int j = 0; j < PointCount; j++)
                {
                    PopulationChromosomes[i][j] = j;
                }
            }
            for (int i = 0; i < PopulationCount; i++)
            {
                for (int s = 0; s < 2 * PointCount; s++)
                {
                    int i1, i2, tmp;

                    i1 = rnd.Next(PointCount);
                    i2 = rnd.Next(PointCount);
                    tmp = PopulationChromosomes[i][i1];
                    PopulationChromosomes[i][i1] = PopulationChromosomes[i][i2];
                    PopulationChromosomes[i][i2] = tmp;
                }
            }
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height - 3 * Radius));
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
            PopulationChromosomes = new int[2 * PopulationCount][];

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
            for (int i = 0; i < PopulationCount; i++)
            {
                PopulationChromosomes[i] = new int[PointCount];
            }
            for (int i = 0; i < PopulationCount; i++)
            {
                for (int j = 0; j < PointCount; j++)
                {
                    PopulationChromosomes[i][j] = j;
                }
            }
            for (int i = 0; i < PopulationCount; i++)
            {
                for (int s = 0; s < 2 * PointCount; s++)
                {
                    int i1, i2, tmp;

                    i1 = rnd.Next(PointCount);
                    i2 = rnd.Next(PointCount);
                    tmp = PopulationChromosomes[i][i1];
                    PopulationChromosomes[i][i1] = PopulationChromosomes[i][i2];
                    PopulationChromosomes[i][i2] = tmp;
                }
            }
        }

        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();
            PlotPoints();
            PlotWay(GetBestWay());
        }

        private int[] GetBestWay()
        {
            Random rnd = new Random();
            List<Roads> roads = new List<Roads>();
            for (int iteration = 0; iteration < 5; iteration++)
            {
                for (int i = 0; i < PopulationCount; i++)
                {
                    int i1, i2, tmp;
                    i1 = rnd.Next(PopulationCount);
                    i2 = rnd.Next(PopulationCount);
                    tmp = rnd.Next(1, PointCount - 1);
                    PopulationChromosomes[i + PopulationCount] = Merge(i1, i2, tmp);
                    if (rnd.NextDouble() < PersMutation)
                    {
                        int DotOne = rnd.Next(PointCount);
                        int DotTwo = rnd.Next(PointCount);
                        PopulationChromosomes[i + PopulationCount] = Mutation(DotOne, DotTwo, PopulationChromosomes[i + PopulationCount]);
                    }
                }
                roads = new List<Roads>();
                for (int i = 0; i < PopulationCount * 2; i++)
                {
                    roads.Add(new Roads(CalcLength(PopulationChromosomes[i]), i));
                }
                roads.Sort(CompareByLength);
                int[][] NewPopulationChromosomes = new int[2 * PopulationCount][];
                for (int i = 0; i < PopulationCount; i++)
                {
                    NewPopulationChromosomes[i] = PopulationChromosomes[roads[i].GetIndex()];
                }
                PopulationChromosomes = NewPopulationChromosomes;
            }
            int[] way = PopulationChromosomes[0];
            return way;
        }
        private double CalcLength(int[] Road)
        {
            double length = 0;
            for (int i = 0; i < Road.Length - 1; i++)
            {
                length += Math.Sqrt(Math.Pow(pC[Road[i + 1]].X - pC[Road[i]].X, 2) + Math.Pow(pC[Road[i + 1]].Y - pC[Road[i]].Y, 2));
            }
            length += Math.Sqrt(Math.Pow(pC[Road[0]].X - pC[Road[Road.Length - 1]].X, 2) + Math.Pow(pC[Road[0]].Y - pC[Road[Road.Length - 1]].Y, 2));
            return length;
        }
        private int[] Merge(int BodyOne, int BodyTwo, int Point)
        {
            Random rnd = new Random();
            int[] NewBodyPartOne = new int[PointCount];
            int[] NewBodyPartTwo = new int[PointCount];
            for (int i = 0; i < PointCount; i++)
            {
                if (i <= Point)
                {
                    NewBodyPartOne[i] = PopulationChromosomes[BodyTwo][i];
                    NewBodyPartTwo[i] = PopulationChromosomes[BodyOne][i];
                }
                else
                {
                    NewBodyPartOne[i] = PopulationChromosomes[BodyOne][i];
                    NewBodyPartTwo[i] = PopulationChromosomes[BodyTwo][i];
                }
            }
            List<int> NewPoints = new List<int>();
            for (int i = 0; i < PointCount * 2; i++)
            {

                if (i < PointCount)
                {
                    if (!NewPoints.Contains(NewBodyPartOne[i]))
                    {
                        NewPoints.Add(NewBodyPartOne[i]);
                    }
                }
                else
                {
                    if (!NewPoints.Contains(NewBodyPartTwo[i % PointCount]))
                        NewPoints.Add(NewBodyPartTwo[i % PointCount]);
                }
            }
            int[] NewBodyOne = new int[PointCount];
            for (int i = 0; i < NewPoints.Count; i++)
            {
                NewBodyOne[i] = NewPoints[i];
            }
            NewPoints.Clear();
            for (int i = 0; i < PointCount * 2; i++)
            {

                if (i < PointCount)
                {
                    if (!NewPoints.Contains(NewBodyPartTwo[i]))
                    {
                        NewPoints.Add(NewBodyPartTwo[i]);
                    }
                }
                else
                {
                    if (!NewPoints.Contains(NewBodyPartOne[i % PointCount]))
                        NewPoints.Add(NewBodyPartOne[i % PointCount]);
                }
            }
            int[] NewBodyTwo = new int[PointCount];
            for (int i = 0; i < NewPoints.Count; i++)
            {
                NewBodyTwo[i] = NewPoints[i];
            }

            if (rnd.NextDouble() < 0.5)
            {
                return NewBodyOne;
            }
            else
            {
                return NewBodyTwo;
            }
        }
        private int[] Mutation(int DotOne, int DotTwo, int[] Body)
        {
            int[] NewBody = new int[Body.Length];
            int UpDot = Math.Max(DotTwo, DotOne);
            int DownDot = Math.Min(DotOne, DotTwo);
            for (int i = 0; i < DownDot; i++)
                NewBody[i] = Body[i];
            for (int i = 0; i <= UpDot - DownDot; i++)
                NewBody[DownDot + i] = Body[UpDot - i];
            for (int i = UpDot + 1; i < Body.Length; i++)
                NewBody[i] = Body[i];
            return NewBody;
        }
        private static int CompareByLength(Roads x, Roads y)
        {
            if (x.GetLength() > y.GetLength())
                return 1;
            else if (x.GetLength() == y.GetLength())
                return 0;
            else
                return -1;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }
    }
}

