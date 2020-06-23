using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveHoldShape : ITask
    {
        private MyShape shape;

        public MoveHoldShape(ShapeComponent shape)
        {
            this.shape = shape as MyShape;
        }

        public void Execute()
        {
            MoveHoldShapeVisitor sv = new MoveHoldShapeVisitor();
            shape.Accept(sv);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

    }
}
