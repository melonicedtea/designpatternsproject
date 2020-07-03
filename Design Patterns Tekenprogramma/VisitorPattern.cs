using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    public abstract class Visitor
    {
        public virtual void Visit(MyShape myShape)
        {
            throw new NotSupportedException();
        }
        public virtual void Visit(MyShapeGroup myShapeGroup)
        {
            throw new NotSupportedException();
        }
        public virtual void Visit(MainWindow mainWindow)
        {
            throw new NotSupportedException();
        }
    }
    interface IVisitable
    {
        void Accept(Visitor visitor);
    }

    class MoveShapeVisitor : Visitor
    {
        public override void Visit(MyShape myShape)
        {
                Shape currentShape = myShape.GetShape();
                //Canvas.SetLeft(currentShape, myShape.points[myShape.moves].X);
                //Canvas.SetTop(currentShape, myShape.points[myShape.moves].Y);

                while (myShape.points.Count > myShape.moves)
                {
                    myShape.points.RemoveAt(myShape.moves);
                }
                myShape.points.Add(new System.Windows.Point(Canvas.GetLeft(currentShape), Canvas.GetTop(currentShape)));
                myShape.moves++;
                Console.WriteLine("movesList: " + myShape.points.Count);
                Console.WriteLine("moves: " + myShape.moves);

           
        }

        public override void Visit(MyShapeGroup shapeGroup)
        {
            List<MyShapeComponent> currentShapes = shapeGroup.GetComponents();
            foreach (MyShapeComponent msc in currentShapes)
            {
                MyShape ms = msc as MyShape;
                if (ms != null)
                {
                    Visit(ms);
                }
                
            }
        }
    }
    class EnlargeShapeVisitor : Visitor
    {
        public override void Visit(MyShape myShape)
        {
            Shape currentShape = myShape.GetShape();

            currentShape.Width *= 1.01;
            currentShape.Height *= 1.01;

            myShape.w *= 1.01;
            myShape.h *= 1.01;

            for (int i = 0; i < myShape.decorators.Count; i++)
            {
                myShape.decorators[i].UndoOrnament();
            }

        }

        public override void Visit(MyShapeGroup shapeGroup)
        {
            List<MyShapeComponent> currentShapes = shapeGroup.GetComponents();
            foreach (MyShape myShape in currentShapes)
            {
                Visit(myShape);
            }
        }
    }

    class ShrinkShapeVisitor : Visitor
    {
        public override void Visit(MyShape myShape)
        {
            Shape currentShape = myShape.GetShape();
            currentShape.Width *= 0.99;
            currentShape.Height *= 0.99;

            myShape.w *= 0.99;
            myShape.h *= 0.99;

            for (int i = 0; i < myShape.decorators.Count; i++)
            {
                myShape.decorators[i].UndoOrnament();
            }
        }

        public override void Visit(MyShapeGroup shapeGroup)
        {
            List<MyShapeComponent> currentShapes = shapeGroup.GetComponents();
            Console.WriteLine(currentShapes.Count);
            foreach (MyShape myShape in currentShapes)
            {
                Shape currentShape = myShape.GetShape();
                currentShape.Width *= 0.99;
                currentShape.Height *= 0.99;

                myShape.w *= 0.99;
                myShape.h *= 0.99;

                for (int i = 0; i < myShape.decorators.Count; i++)
                {
                    myShape.decorators[i].UndoOrnament();
                }
            }
        }
    }
    public class SaveFileVisitor : Visitor
    {
        public static List<string> save = new List<string>();
        public override void Visit(MyShape myShape)
        {
            Shape shape = myShape.GetShape();
            string line =
                shape.Name + " " +
                (int)Canvas.GetLeft(shape) + " " +
                (int)Canvas.GetTop(shape) + " " +
                (int)shape.Width + " " +
                (int)shape.Height;
                //+Environment.NewLine;
            save.Add(line);

            Console.WriteLine(line);
            
        }
        public override void Visit(MainWindow mainWindow)
        {
            File.WriteAllLines("Mytxt.txt", save.ToArray());
        }

    }
}
