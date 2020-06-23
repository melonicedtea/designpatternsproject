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
        public override void Visit(MyShape myShape)
        {
            Shape currentShape = myShape.GetShape();
            Canvas.SetLeft(currentShape, myShape.GetXY().X);
            Canvas.SetTop(currentShape, myShape.GetXY().Y);
            //myShape.MoveFinished();
        }

        public override void Visit(ShapeGroup shapeGroup)
        {
            List<ShapeComponent> currentShapes = shapeGroup.GetComponents();
            foreach (ShapeComponent shapeComponent in currentShapes)
            {
                Canvas.SetLeft(shapeComponent.GetShape(), shapeComponent.GetXY().X);
                Canvas.SetTop(shapeComponent.GetShape(), shapeComponent.GetXY().Y);
                Console.WriteLine("XY" + shapeComponent.GetXY().X +", " +shapeComponent.GetXY().Y);
                Console.WriteLine("OLDXY" + shapeComponent.GetOldXY().X + ", " + shapeComponent.GetOldXY().Y);
            }
            //shapeGroup.MoveFinished();
        }
    }
}
