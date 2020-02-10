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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Boolean drawmode = false;
        private Point startPoint;
        private Point newPos;
        private Rectangle rect;


        private void AddRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            drawmode = true;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int test = 1;

            startPoint = e.GetPosition(canvas);

            if (drawmode == true)
            {
                rect = new Rectangle
                {
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    IsManipulationEnabled = true,


                };
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue);
                rect.MouseDown += Rectangle_MouseDown;
                rect.MouseMove += Rectangle_MouseMove;
                rect.MouseUp += Rectangle_MouseUp;
                Canvas.SetLeft(rect, startPoint.X);
                Canvas.SetTop(rect, startPoint.Y);
                canvas.Children.Add(rect);
            }
        }

        

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || rect == null || sender.GetType().Name == "Rectangle")
                return;


            var pos = e.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            rect.Width = w;
            rect.Height = h;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
            drawmode = false;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rect = (Rectangle)sender;
            startPoint = e.GetPosition(canvas);

            
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.Y);
        }
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Released || rect == null)
                return;
            
            newPos = e.GetPosition(canvas);
            double x = Canvas.GetLeft(rect);
            double y = Canvas.GetRight(rect);

            Canvas.SetLeft(rect, x + (newPos.X - startPoint.X));
            Canvas.SetTop(rect, y + (newPos.Y - startPoint.Y));
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }
    }
}
