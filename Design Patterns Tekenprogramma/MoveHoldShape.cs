using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveHoldShape : Task
    {
        private MyShape shape;

        public MoveHoldShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void Execute()
        {
            shape.MoveHold();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

    }
}
