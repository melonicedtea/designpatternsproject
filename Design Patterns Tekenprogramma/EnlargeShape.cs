using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    class EnlargeShape : Task
    {
        private MyShape shape;

        public EnlargeShape(ShapeComponent shape)
        {
            this.shape = shape as MyShape;
        }

        public void Execute()
        {
            shape.Enlarge();
        }

        public void Undo()
        {
            shape.UndoEnlarge();
        }
    }
}
