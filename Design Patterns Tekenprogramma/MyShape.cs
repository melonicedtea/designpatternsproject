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
        Point startPoint;
        MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        Shape currentShape;
        Point newPos;
        double x;
        double y;
        double oldX;
        double oldY;

        public MyShape()
        {

        }
        public MyShape(Shape shape)
        {
            currentShape = shape;

            oldX = Canvas.GetLeft(shape);
            oldY = Canvas.GetTop(shape);


        }

        public void MoveHold()
        {
            newPos = Mouse.GetPosition(myWin.canvas);

            x = Canvas.GetLeft(currentShape);
            y = Canvas.GetTop(currentShape);

            Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
            Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

            startPoint = newPos;
        }

        public void MoveFinished()
        {
            Canvas.SetLeft(currentShape, x);
            Canvas.SetTop(currentShape, y);
        }

        public void UndoMove()
        {

            Canvas.SetLeft(currentShape, oldX);
            Canvas.SetTop(currentShape, oldY);

        }

        public void Draw()
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

            x = Math.Min(pos.X, startPoint.X);
            y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            currentShape.Width = w;
            currentShape.Height = h;

            Canvas.SetLeft(currentShape, x);
            Canvas.SetTop(currentShape, y);
        }

        public void drawFinished()
        {
                if (!myWin.canvas.Children.Contains(currentShape))
                {
                    myWin.canvas.Children.Add(currentShape);
                }
                        
        }

        public void UndoDraw()
        {
            if (myWin.canvas.Children.Contains(currentShape))
            {
                myWin.canvas.Children.Remove(currentShape);
            }

        }
        public void SetStartPoint()
        {
            startPoint = myWin.GetStartPoint();
        }


    }
}
