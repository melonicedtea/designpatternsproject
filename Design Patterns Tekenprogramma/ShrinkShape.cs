using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    class ShrinkShape : ITask
    {
        private MyShape myShape;

        public ShrinkShape(ShapeComponent shapeComponent)
        {
            myShape = shapeComponent as MyShape;
        }

        public void Execute()
        {
            //myShape.Shrink();
            ShrinkShapeVisitor shrinkShapeVisitor = new ShrinkShapeVisitor();
            myShape.Accept(shrinkShapeVisitor);
        }

        public void Undo()
        {
            myShape.UndoShrink();
        }
    }
}

