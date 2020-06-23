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
    public class MyShape : ShapeComponent, IVisitable
    {
        static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        Point startPoint = Mouse.GetPosition(myWin.canvas);
        Point newPos;
        Shape currentShape;
        double x;
        double y;
        double oldX;
        double oldY;
        string groupName;
        public List<TextBlock> ornaments = new List<TextBlock>();

        public IDrawStrategy drawStrategy;
        public MyShape()
        {

        }
        public MyShape(Shape shape)
        {
            currentShape = shape;

            oldX = Canvas.GetLeft(shape);
            oldY = Canvas.GetTop(shape);

            x = Canvas.GetLeft(currentShape);
            y = Canvas.GetTop(currentShape);

            
        }

        public override void MoveHold()
        {
            newPos = Mouse.GetPosition(myWin.canvas);

            x = Canvas.GetLeft(currentShape);
            y = Canvas.GetTop(currentShape);

            Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
            Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

            startPoint = newPos;

            Console.WriteLine("orn count: " + ornaments.Count);
            foreach (TextBlock ornament in ornaments)
            {
                Console.WriteLine("Im HETE");
                Canvas.SetLeft(ornament, x);
                Canvas.SetTop(ornament, y);
            }
        }

        public override void MoveFinished()
        {

            Canvas.SetLeft(currentShape, x);
            Canvas.SetTop(currentShape, y);
        }

        public void UndoMove()
        {

            Canvas.SetLeft(currentShape, oldX);
            Canvas.SetTop(currentShape, oldY);

        }

        public void SetDrawStrategy(IDrawStrategy newDrawStrategy)
        {
            drawStrategy = newDrawStrategy;
        }
        public void Draw()
        {
            Shape shapeToDraw = drawStrategy.GetDrawStrategy();
            currentShape = shapeToDraw;
            //currentShape = new Rectangle()
            //{
            //    Stroke = Brushes.LightBlue,
            //    StrokeThickness = 2,
            //    Fill = new SolidColorBrush(Colors.AliceBlue)
            //};


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

        public void DrawFinished()
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

        //public override void Enlarge()
        //{
        //    currentShape.Width *= 1.01;
        //    currentShape.Height *= 1.01;
        //}

        public void UndoEnlarge()
        {
            currentShape.Width *= 0.99;
            currentShape.Height *= 0.99;
        }

        //public void Shrink()
        //{
        //    currentShape.Width *= 0.99;
        //    currentShape.Height *= 0.99;
        //}
        
        public void UndoShrink()
        {
            currentShape.Width *= 1.01;
            currentShape.Height *= 1.01;
        }

        public override void SetStartPoint()
        {
            startPoint = myWin.GetStartPoint();
        }
        public Point GetStartPoint()
        {
            return startPoint;
        }

        public override void DisplayShapeInfo()
        {
            Console.WriteLine(currentShape.Name + " inGroup: " + groupName);
        }

        public override void SetGroupName(string groupName)
        {
            this.groupName = groupName;
            
        }
        public void AddOrnament(TextBlock ornament)
        {
            ornaments.Add(ornament);
        }

        public override Shape GetShape()
        {
            return currentShape;
        }

        public void Accept(IVisitor visitor)
        {
            Console.WriteLine("x: "+x+" y: "+y);
            visitor.Visit(this);
        }

        public void SetXY(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Point GetXY()
        {
            return new Point(x, y);
        }
    }
}
