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

        public DrawFinishedShape(ShapeComponent shape)
        {
            this.shape = shape as MyShape;
        }

        public void Execute()
        {
            shape.DrawFinished();
        }

        public void Undo()
        {
            shape.UndoDraw();
        }
    }
}
