using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{

    public abstract class MyShapeComponent : IVisitable
    {

        public virtual void Add(MyShapeComponent shapeComponent)
        {

            throw new NotSupportedException();
        }


        public virtual List<MyShapeComponent> GetComponents()
        {
            throw new NotSupportedException();
        }


        public virtual void DisplayShapeInfo()
        {
            throw new NotSupportedException();
        }


        public virtual void UndoEnlarge()
        {
            throw new NotSupportedException();
        }


        public virtual void UndoShrink()
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

        public virtual void UndoMove()
        {
            throw new NotSupportedException();
        }

        public virtual void SetStartPoint()
        {
            throw new NotSupportedException();
        }


        public virtual Shape GetShape()
        {
            throw new NotSupportedException();
        }

        public virtual void SetStrokeColor(SolidColorBrush color)
        {
            throw new NotSupportedException();
        }

        public virtual void Accept(Visitor visitor)
        {
            throw new NotSupportedException();
        }

        public virtual string ToString()
        {
            throw new NotSupportedException();
        }

        public virtual List<string> GetStrings()
        {

            throw new NotSupportedException();
        }

        public virtual void SetSelected(bool b)
        {
            throw new NotSupportedException();
        }
    }
}
