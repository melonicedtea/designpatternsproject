using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    public abstract class ShapeComponent
    {
        public bool inGroup = false;
        public virtual void Add(ShapeComponent shapeComponent)
        {

            throw new NotSupportedException();
        }

        public void Remove(ShapeComponent shapeComponent)
        {
            throw new NotSupportedException();

        }
        public virtual ShapeComponent GetComponent(int componentIndex)
        {
            throw new NotSupportedException();

        }

        public virtual List<ShapeComponent> GetComponents()
        {
            throw new NotSupportedException();
        }

        public string GetShapeName()
        {
            throw new NotSupportedException();

        }

        public virtual void DisplayShapeInfo()
        {
            throw new NotSupportedException();
        }

        public virtual void Enlarge()
        {
            throw new NotSupportedException();
        }

        public virtual void MoveHold()
        {
            throw new NotSupportedException();
        }

        public virtual void MoveFinished()
        {
            throw new NotSupportedException();
        }
        //public virtual void UndoMove()
        //{
        //    throw new NotSupportedException();
        //}

        public virtual void SetStartPoint()
        {
            throw new NotSupportedException();
        }

        public virtual void SetGroupName(String groupName)
        {
            throw new NotSupportedException();
        }
        public virtual Shape GetShape()
        {
            throw new NotSupportedException();
        }
    }
}
