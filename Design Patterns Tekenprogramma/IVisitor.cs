using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public abstract class IVisitor
    {
        public virtual void Visit(MyShape myShape)
        {
            throw new NotSupportedException();
        }
        public virtual void Visit(ShapeGroup shapeGroup)
        {
            throw new NotSupportedException();
        }
        public virtual void Visit(MainWindow mainWindow)
        {
            throw new NotSupportedException();
        }
    }
}
