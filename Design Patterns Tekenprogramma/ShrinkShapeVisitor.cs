using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class ShrinkShapeVisitor : IVisitor
    {
        public void Visit(MyShape myShape)
        {
            Shape currentShape = myShape.GetShape();
            currentShape.Width *= 0.99;
            currentShape.Height *= 0.99;
        }

        public void Visit(ShapeGroup shapeGroup)
        {
            List<ShapeComponent> currentShapes = shapeGroup.GetComponents();
            Console.WriteLine(currentShapes.Count);
            foreach (ShapeComponent shapeComponent in currentShapes)
            {
                Shape currentShape = shapeComponent.GetShape();
                currentShape.Width *= 0.99;
                currentShape.Height *= 0.99;
            }
        }
    }
}
