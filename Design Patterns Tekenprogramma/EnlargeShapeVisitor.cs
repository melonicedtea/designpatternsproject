using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class EnlargeShapeVisitor : IVisitor
    {
        public void Visit(MyShape myShape)
        {
            System.Windows.Shapes.Shape currentShape = myShape.GetShape();

            currentShape.Width *= 1.01;
            currentShape.Height *= 1.01;
        }

        public void Visit(ShapeGroup shapeGroup)
        {
            List<ShapeComponent> currentShapes = shapeGroup.GetComponents();
            Console.WriteLine(currentShapes.Count);
            foreach (ShapeComponent shapeComponent in currentShapes)
            {
                System.Windows.Shapes.Shape currentShape = shapeComponent.GetShape();

                currentShape.Width *= 1.01;
                currentShape.Height *= 1.01;
            }
        }
    }
}
