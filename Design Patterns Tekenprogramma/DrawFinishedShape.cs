using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class DrawFinishedShape : Task
    {
        private MyShape shape;

        public DrawFinishedShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void Execute()
        {
            shape.drawFinished();
        }

        public void Undo()
        {
            shape.UndoDraw();
        }
    }
}
