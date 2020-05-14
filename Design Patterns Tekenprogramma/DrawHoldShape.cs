
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class DrawHoldShape : Task
    {
        private MyShape shape;

        public DrawHoldShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void Execute()
        {
            shape.drawHold();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}