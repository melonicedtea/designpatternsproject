using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public interface IVisitor
    {
        void Visit(MyShape myShape);
        void Visit(ShapeGroup shapeGroup);
    }
}
