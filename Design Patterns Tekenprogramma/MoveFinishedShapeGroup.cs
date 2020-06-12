using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveFinishedShapeGroup : Task
    {
        private ShapeGroup shapeGroup;

        public MoveFinishedShapeGroup(ShapeComponent shapeGroup)
        {
            this.shapeGroup = shapeGroup as ShapeGroup;
        }

        public void Execute()
        {
            shapeGroup.MoveFinished();
        }

        public void Undo()
        {
            shapeGroup.UndoMove();
        }

    }
}