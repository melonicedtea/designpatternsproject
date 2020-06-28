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
    public class MyShape : MyShapeComponent, IVisitable
    {
        static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        Point startPoint = Mouse.GetPosition(myWin.canvas);
        Point newPos;
        Shape currentShape;
        double x;
        double y;
        public int groupNumber;

        public double h = 0;
        public double w = 0;

        public List<OrnamentShapeDecorator> decorators = new List<OrnamentShapeDecorator>();

        public List<Point> points = new List<Point>();

        public int moves = 0;

        public IDrawStrategy drawStrategy;
        public MyShape()
        {

        }
        public MyShape(Shape shape)
        {
            currentShape = shape;
            x = Canvas.GetLeft(currentShape);
            y = Canvas.GetTop(currentShape);          
        }

        public override void MoveHold()
        {
            if (currentShape != null)
            {
                newPos = Mouse.GetPosition(myWin.canvas);

                x = Canvas.GetLeft(currentShape);
                y = Canvas.GetTop(currentShape);

                Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
                Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

                startPoint = newPos;
            }

            for(int i = 0; i < decorators.Count; i++)
            {
                decorators[i].MoveOrnament();
            }
                

           
        }

        //public override void MoveFinished()
        //{

        //    Canvas.SetLeft(currentShape, x);
        //    Canvas.SetTop(currentShape, y);
        //}

        public override void UndoMove()
        {
            moves--;
            if (moves < 0)
            {
                moves = 0;
            }
            Canvas.SetLeft(currentShape, points[moves-1].X);
            Canvas.SetTop(currentShape, points[moves-1].Y);

            Console.WriteLine("moves: " + moves);

            for (int i = 0; i < decorators.Count; i++)
            {
                decorators[i].UndoOrnament();
            }
        }

        public void RedoMove()
        {
            moves++;
            Canvas.SetLeft(currentShape, points[moves-1].X);
            Canvas.SetTop(currentShape, points[moves-1].Y);

            for (int i = 0; i < decorators.Count; i++)
            {
                decorators[i].MoveOrnament();
            }
        }

        public void SetDrawStrategy(IDrawStrategy newDrawStrategy)
        {
            drawStrategy = newDrawStrategy;
        }
        public void Draw()
        {
            Shape shapeToDraw = drawStrategy.GetDrawStrategy();
            currentShape = shapeToDraw;

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

            w = Math.Max(pos.X, startPoint.X) - x;
            h = Math.Max(pos.Y, startPoint.Y) - y;

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

        public override void UndoEnlarge()
        {
            currentShape.Width *= 0.99;
            currentShape.Height *= 0.99;
            w *= 0.99;
            h *= 0.99;
            for (int i = 0; i < decorators.Count; i++)
            {
                decorators[i].UndoOrnament();
            }
        }
        
        public override void UndoShrink()
        {
            currentShape.Width *= 1.01;
            currentShape.Height *= 1.01;
            h *= 1.01;
            w *= 1.01;
            for (int i = 0; i < decorators.Count; i++)
            {
                decorators[i].UndoOrnament();
            }
        }

        public override void SetStartPoint()
        {
            startPoint = myWin.GetStartPoint();
        }


        public override void SetStrokeColor(SolidColorBrush color)
        {
            currentShape.Stroke = color;
        }

        public void AddDecorator(OrnamentShapeDecorator d)
        {
            decorators.Add(d);
        }
        public void RemoveDecorator(OrnamentShapeDecorator d)
        {
            decorators.Remove(d);
        }

        public override void DisplayShapeInfo()
        {
            Console.WriteLine(ToString());
        }

        public void SetGroupNumber(int groupNumber)
        {
            this.groupNumber = groupNumber;
            
        }

        public override Shape GetShape()
        {
            return currentShape;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }

        //public override void SetXY(double x, double y)
        //{
        //    this.x = x;
        //    this.y = y;
        //}
        public override Point GetXY()
        {
            return new Point(x, y);
        }

        public override string ToString()
        {
                return currentShape.Name + " " + Convert.ToInt32(x).ToString() + " " + Convert.ToInt32(y).ToString() + " " + Convert.ToInt32(w).ToString() + " " + Convert.ToInt32(h).ToString();
        }

        public override List<string> GetStrings()
        {
            List<string> strings = new List<string>();
            strings.Add(ToString());
            return strings;
        }
    }
}
