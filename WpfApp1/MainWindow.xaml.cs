using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfApp1
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


        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            shapeString = Convert.ToString(radioButton.Content.ToString());
            String shapeName = Convert.ToString(radioButton.Content.ToString());
            //MessageBox.Show(shapeName.ToString(CultureInfo.InvariantCulture));
        }

        private Point startPoint;
        //private Ellipse ellipse = new Ellipse() {
        //    Stroke = Brushes.LightBlue,
        //    StrokeThickness = 2
        //};
        //private Rectangle rect = new Rectangle()
        //{

        //    Stroke = Brushes.LightBlue,
        //        StrokeThickness = 2

        //};
        private Shape shape;
        private string shapeString;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            startPoint = e.GetPosition(canvas);
            shape = new Rectangle()
            {

                Stroke = Brushes.LightBlue,
                StrokeThickness = 2

            }; ;
            if (shapeString == "rect")
            { shape = new Rectangle()
               {

                Stroke = Brushes.LightBlue,
                StrokeThickness = 2

               };
            }
            if (shapeString == "ellipse")
            { shape = new Ellipse()
            {
                Name = "ellipse",
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2
            };


            
            //{
            //    Stroke = Brushes.LightBlue,
            //    StrokeThickness = 2
            //};
            Canvas.SetLeft(shape, startPoint.X);
            Canvas.SetTop(shape, startPoint.Y);
            canvas.Children.Add(shape);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || shape == null)
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
        }
    }
}
