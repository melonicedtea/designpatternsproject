using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    class EnlargeShape : ITask
    {
        private MyShape myShape;

        public EnlargeShape(ShapeComponent shapeComponent)
        {
            myShape = shapeComponent as MyShape;
        }

        public void Execute()
        {
            //myShape.Enlarge();
            EnlargeShapeVisitor enlargeShapeVisitor = new EnlargeShapeVisitor();
            myShape.Accept(enlargeShapeVisitor);
        }

        public void Undo()
        {
            myShape.UndoEnlarge();
        }
    }
}
