using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    public class MyShape
    {
        static bool isCalled = false;
        static Point startPoint;
        static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        static Shape currentShape;
        Point newPos;

        static List<Shape> shapes = new List<Shape>();
        static List<Point> points = new List<Point>();
        static int counter = 0;

        //static bool undo = false;

        public void Move()
        {

            while (shapes.Count > counter)
            {
                shapes.Remove(shapes.Last());
            }

            while (points.Count > counter)
            {
                points.Remove(points.Last());
            }

            newPos = Mouse.GetPosition(myWin.canvas);
            currentShape = myWin.GetShape();

            if (!isCalled)
            {
                startPoint = myWin.GetStartPoint();
                isCalled = true;

            }

            double x = Canvas.GetLeft(currentShape);
            double y = Canvas.GetTop(currentShape);

            Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
            Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

            startPoint = newPos;

            shapes.Add(currentShape);
            Point point = new Point(x, y);
            points.Add(point);

            counter++;


            Console.WriteLine("step: " + counter);

            Console.WriteLine("shapescnt: " + shapes.Count);

            Console.WriteLine("pointscnt: " + points.Count);
        }

        public void UndoMove()
        {
            if (counter > 0)
            {

                int step = 10;
                counter = counter - step;
                if (counter < 0)
                {
                    counter = 0;
                }
                Shape shape = shapes[counter];
                Canvas.SetLeft(shape, points[counter].X);
                Canvas.SetTop(shape, points[counter].Y);

                Console.WriteLine(counter);
            }

        }

        public void RedoMove()
        {
            if (counter < shapes.Count - 1)
            {

                int step = 10;
                counter = counter + step;
                if (counter > shapes.Count - 1)
                {
                    counter = shapes.Count - 1;
                }
                Shape shape = shapes[counter];
                Canvas.SetLeft(shape, points[counter].X);
                Canvas.SetTop(shape, points[counter].Y);

                Console.WriteLine(counter);
            }
        }

        public void draw()
        {
            startPoint = Mouse.GetPosition(myWin.canvas);



            if (myWin.GetMode() == "rect")
            {
                //create shape
                currentShape = new Rectangle()
                {

                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)


                };
                //add to list
                myWin.AddShape(currentShape);
                //add functions
                myWin.AddMethods(currentShape);
                //set pos
                Canvas.SetLeft(currentShape, startPoint.X);
                Canvas.SetTop(currentShape, startPoint.Y);
                //add to canvas
                myWin.canvas.Children.Add(currentShape);
                myWin.SetShape(currentShape);


            }
            if (myWin.GetMode() == "ellipse")
            {
                currentShape = new Ellipse()
                {
                    Name = "ellipse",
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)
                };
                myWin.AddShape(currentShape);
                myWin.AddMethods(currentShape);
                Canvas.SetLeft(currentShape, startPoint.X);
                Canvas.SetTop(currentShape, startPoint.Y);
                myWin.canvas.Children.Add(currentShape);
                myWin.SetShape(currentShape);

            }
        }

        public void drawHold()
        {
            var pos = Mouse.GetPosition(myWin.canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            currentShape.Width = w;
            currentShape.Height = h;

            Canvas.SetLeft(currentShape, x);
            Canvas.SetTop(currentShape, y);
        }
        public static void SetIsCalled()
        {
            isCalled = false;
        }


    }
}
