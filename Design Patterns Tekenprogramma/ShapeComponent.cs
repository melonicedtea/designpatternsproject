using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public abstract class ShapeComponent
    {
        public virtual void Add(ShapeComponent shapeComponent)
        {
           

        }

        public void Remove(ShapeComponent shapeComponent)
        {
            throw new NotSupportedException();

        }
        public ShapeComponent GetComponent(int componentIndex)
        {
            throw new NotSupportedException();

        }

        public string GetShapeName()
        {
            throw new NotSupportedException();

        }

        public virtual void DisplayShapeInfo()
        {
            
        }
    }
}
