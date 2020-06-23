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
    public class MoveHoldShapeVisitor : IVisitor
    {
        MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        Point startPoint;

        public MoveHoldShapeVisitor() { }
        public void Visit(MyShape myShape)
        {
            Console.WriteLine("Shape Moved");
            myShape.MoveHold();
            //Shape currentShape = myShape.GetShape();
            //Point newPos = Mouse.GetPosition(myWin.canvas);

            //double x = Canvas.GetLeft(currentShape);
            //double y = Canvas.GetTop(currentShape);

            //myShape.SetXY(x, y);

            //Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
            //Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

            //Console.WriteLine("orn count: " + myShape.ornaments.Count);
            //foreach(TextBlock ornament in myShape.ornaments)
            //{
            //    Console.WriteLine("Im HETE");
            //    Canvas.SetLeft(ornament, x);
            //    Canvas.SetTop(ornament, y);
            //}

            //startPoint = newPos;
        }

        public void Visit(ShapeGroup shapeGroup)
        {
            Console.WriteLine("Shapegroup Moved");

            List<ShapeComponent> currentShapes = shapeGroup.GetComponents();
            Console.WriteLine(currentShapes.Count);
            foreach (ShapeComponent shapeComponent in currentShapes)
            {
                System.Windows.Shapes.Shape currentShape = shapeComponent.GetShape();
                Point newPos = Mouse.GetPosition(myWin.canvas);

                double x = Canvas.GetLeft(currentShape);
                double y = Canvas.GetTop(currentShape);
                Console.WriteLine(x.ToString());
                Console.WriteLine(y.ToString());

                Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
                Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

                startPoint = newPos;
            }
        }

        public void SetStartPoint()
        {
            myWin = (MainWindow)Application.Current.MainWindow;
            startPoint = myWin.GetStartPoint();
        }
    }
}
