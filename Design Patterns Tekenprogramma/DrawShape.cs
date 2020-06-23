using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class DrawShape : ITask
    {
        private MyShape shape;

        public DrawShape(ShapeComponent shape)
        {
            this.shape = shape as MyShape;
        }

        public void Execute()
        {
            shape.Draw();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
