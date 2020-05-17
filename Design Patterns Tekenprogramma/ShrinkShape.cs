using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    class ShrinkShape : Task
    {
        private MyShape shape;

        public ShrinkShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void Execute()
        {
            shape.Shrink();
        }

        public void Undo()
        {
            shape.UndoShrink();
        }
    }
}

