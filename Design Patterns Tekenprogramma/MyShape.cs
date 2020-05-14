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
 class MyShape
    {
        static bool isCalled = false;
        static Point startPoint;
        static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        Shape currentShape;
        static Point newPos;
        static int oldcnt = myWin.scc;


        public void move()
        {
            int newcnt = myWin.scc;
           
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

            
        }

        public void draw()
        {
            myWin.scc++;
            Console.WriteLine(myWin.scc);
            startPoint = Mouse.GetPosition(myWin.canvas);



            if (myWin.mode == "rect")
            {
                //create shape
                currentShape = new Rectangle()
                {

                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)


                };
                //add to list
                myWin.shapes.Add(currentShape);
                //add functions
                myWin.addMethods(currentShape);
                //set pos
                Canvas.SetLeft(currentShape, startPoint.X);
                Canvas.SetTop(currentShape, startPoint.Y);
                //add to canvas
                myWin.canvas.Children.Add(currentShape);
                myWin.setShape(currentShape);


            }
            if (myWin.mode == "ellipse")
            {
                currentShape = new Ellipse()
                {
                    Name = "ellipse",
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue)
                };
                myWin.shapes.Add(currentShape);
                myWin.addMethods(currentShape);
                Canvas.SetLeft(currentShape, startPoint.X);
                Canvas.SetTop(currentShape, startPoint.Y);
                myWin.canvas.Children.Add(currentShape);
                myWin.setShape(currentShape);

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

        public static void setIsCalled()
        {
            isCalled = false;
        }
    }
}
