using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveFinishedShape : ITask
    {
        private MyShape myShape;

        public MoveFinishedShape(ShapeComponent shapeComponent)
        {
            myShape = shapeComponent as MyShape;
        }

        public void Execute()
        {
            Console.WriteLine("myshape coords:");
            Console.WriteLine(myShape.GetXY());
            //myShape.MoveFinished();
            MoveFinishedShapeVisitor sv = new MoveFinishedShapeVisitor();
            myShape.Accept(sv);
        }

        public void Undo()
        {
            myShape.UndoMove();
        }

    }
}