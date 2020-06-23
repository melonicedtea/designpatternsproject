using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveFinishedShapeVisitor : IVisitor
    {
        public void Visit(MyShape myShape)
        {
            Shape currentShape = myShape.GetShape();
            Canvas.SetLeft(currentShape, myShape.GetXY().X);
            Canvas.SetTop(currentShape, myShape.GetXY().Y);
            //myShape.MoveFinished();
        }

        public void Visit(ShapeGroup shapeGroup)
        {
            List<ShapeComponent> currentShapes = shapeGroup.GetComponents();
            foreach (ShapeComponent shapeComponent in currentShapes)
            {
                Canvas.SetLeft(shapeComponent.GetShape(), Canvas.GetLeft(shapeComponent.GetShape()));
                Canvas.SetTop(shapeComponent.GetShape(), Canvas.GetTop(shapeComponent.GetShape()));
            }
        }
    }
}
