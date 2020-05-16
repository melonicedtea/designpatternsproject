using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveFinishedShape : Task
    {
        private MyShape shape;

        public MoveFinishedShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void Execute()
        {
            shape.MoveFinished();
        }

        public void Undo()
        {
            shape.UndoMove();
        }

    }
}