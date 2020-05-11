﻿using System;
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

        private bool drawMode = false;
        private Point startPoint;
        private Point newPos;
        private bool drag = false;
        private Shape shape;
        private List<Shape> shapes = new List<Shape>();
        private string mode;
        private bool shapeClicked;
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

            startPoint = e.GetPosition(canvas);

            if (mode == "select" && shapeClicked == false)
            {
                if (shapes != null)
                    foreach (Shape shape in shapes)
                        shape.Stroke = Brushes.LightBlue;


            }

            if (mode == "rect")
            {
                //create shape
                shape = new Rectangle()
                {

                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)


                };
                //add to list
                shapes.Add(shape);
                //add functions
                shape.MouseDown += Shape_MouseDown;
                shape.MouseMove += Shape_MouseMove;
                shape.MouseUp += Shape_MouseUp;
                //set pos
                Canvas.SetLeft(shape, startPoint.X);
                Canvas.SetTop(shape, startPoint.Y);
                //add to canvas
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
                shapes.Add(shape);
                shape.MouseDown += Shape_MouseDown;
                shape.MouseMove += Shape_MouseMove;
                shape.MouseUp += Shape_MouseUp;
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
            drawMode = false;

        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            shapeClicked = true;

            shape = sender as Shape;
            startPoint = e.GetPosition(canvas);
            drag = true;

            if (mode == "select")
                shape.Stroke = Brushes.Red;

        }
        private void Shape_MouseMove(object sender, MouseEventArgs e)
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

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shape = null;
            drag = false;
            shapeClicked = false;
        }
    }
}
