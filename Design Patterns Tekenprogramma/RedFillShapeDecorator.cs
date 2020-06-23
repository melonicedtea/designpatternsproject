using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    public class RedFillShapeDecorator
    {
        MyShape decoratedMyShape;
        public RedFillShapeDecorator(MyShape decoratedMyShape)
        {
            this.decoratedMyShape = decoratedMyShape;
            SetRedFill();
        }

        private void SetRedFill()
        {
            decoratedMyShape.GetShape().Fill = Brushes.Red;
        }
 
    }
}
