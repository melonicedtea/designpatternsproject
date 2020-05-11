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
        private bool drag = false;
        private Shape shape;
        private List<Shape> shapes = new List<Shape>();
        private Shape box;
        private string mode;
        private string level;
        private string level1;

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {

            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            mode = Convert.ToString(radioButton.Content.ToString());
            String shapeName = Convert.ToString(radioButton.Content.ToString());
            //MessageBox.Show(shapeName.ToString(CultureInfo.InvariantCulture));
        }

        //private void AddRectangleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    drawmode = true;
        //}

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            level1 = sender.GetType().Name;
           // Console.WriteLine(level1);


            startPoint = e.GetPosition(canvas);

            if (mode == "select" && level1 == "Canvas" && level == null)
            {
                if (shapes != null)
                    foreach (Shape shape in shapes)
                        shape.Stroke = Brushes.LightBlue;


            }

            if (mode == "rect")
            {
                shape = new Rectangle()
                {

                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)


                };
                shapes.Add(shape);
                shape.MouseDown += Rectangle_MouseDown;
                shape.MouseMove += Rectangle_MouseMove;
                shape.MouseUp += Rectangle_MouseUp;
                Canvas.SetLeft(shape, startPoint.X);
                Canvas.SetTop(shape, startPoint.Y);
                canvas.Children.Add(shape);
                
            }
            if (mode == "ellipse")
            {
                shape = new Ellipse()
                {
                    Name = "ellipse",
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)
                };
                shape.MouseDown += Rectangle_MouseDown;
                shape.MouseMove += Rectangle_MouseMove;
                shape.MouseUp += Rectangle_MouseUp;
                Canvas.SetLeft(shape, startPoint.X);
                Canvas.SetTop(shape, startPoint.Y);
                canvas.Children.Add(shape);
            }

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || shape == null || sender.GetType().Name == "Shape" || mode == "select")
                return;

                var pos = e.GetPosition(canvas);

                var x = Math.Min(pos.X, startPoint.X);
                var y = Math.Min(pos.Y, startPoint.Y);

                var w = Math.Max(pos.X, startPoint.X) - x;
                var h = Math.Max(pos.Y, startPoint.Y) - y;

                shape.Width = w;
                shape.Height = h;

                Canvas.SetLeft(shape, x);
                Canvas.SetTop(shape, y);
            
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
            shape = null;
            drawmode = false;
            level = null;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            level = sender.GetType().Name;
            Console.WriteLine(level);

            shape = sender as Shape;
            startPoint = e.GetPosition(canvas);
            drag = true;

            if (mode == "select" && level == "Rectangle")
                shape.Stroke = Brushes.Red;

        }
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Released || shape == null)
                return;

            if (drag)
            {
                newPos = Mouse.GetPosition(canvas);
                double x = Canvas.GetLeft(shape);
                double y = Canvas.GetTop(shape);

                Canvas.SetLeft(shape, x + (newPos.X - startPoint.X));
                Canvas.SetTop(shape, y + (newPos.Y - startPoint.Y));

                startPoint = newPos;
            }
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shape = null;
            drag = false;
        }
    }
}
