using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveFinishedShapeGroup : ITask
    {
        private ShapeGroup shapeGroup;

        public MoveFinishedShapeGroup(ShapeComponent shapeGroup)
        {
            this.shapeGroup = shapeGroup as ShapeGroup;
        }

        public void Execute()
        {
            //shapeGroup.MoveFinished();
            MoveFinishedShapeVisitor sv = new MoveFinishedShapeVisitor();
            shapeGroup.Accept(sv);
        }

        public void Undo()
        {
            shapeGroup.UndoMove();
        }

    }
}