using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public abstract class ShapeDecorator : MyShape
    {
        private MyShape decoratedMyShape;

        public ShapeDecorator(MyShape decoratedMyShape)
        {
            this.decoratedMyShape = decoratedMyShape;
        }

        public new void Draw()
        {
            decoratedMyShape.Draw();
        }
    }  
}
