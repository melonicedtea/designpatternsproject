using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public class ShapeGroup : ShapeComponent, IVisitable
    {
        List<ShapeComponent> shapeComponents = new List<ShapeComponent>();

        string groupName;

        public ShapeGroup(String groupName)
        {
            this.groupName = groupName;

        }

        public string GetGroupName() { return groupName; }

        public override void Add(ShapeComponent shapeComponent)
        {
            shapeComponents.Add(shapeComponent);
        }
        public void Remove(ShapeComponent shapeComponent)
        {
            shapeComponents.Remove(shapeComponent);
        }

        public override ShapeComponent GetComponent(int componentIndex)
        {
            return shapeComponents[componentIndex];
        }
        public override List<ShapeComponent> GetComponents()
        {
            return shapeComponents;
        }

        public override void DisplayShapeInfo()
        {
            Console.WriteLine(GetGroupName());

            foreach(ShapeComponent sc in shapeComponents)
            {
                sc.DisplayShapeInfo();
            }
        }

        public override void Enlarge()
        {

            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.Enlarge();
            }
        }

        public override void MoveHold()
        {
            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.MoveHold();
            }
        }

        public override void MoveFinished()
        {
            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.MoveFinished();
            }
        }
        public void UndoMove()
        {
            foreach (MyShape sc in shapeComponents)
            {
                sc.UndoMove();
            }
        }

        public override void SetGroupName(string groupName)
        {
            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.SetGroupName(groupName);
            }
        }
        public override void SetStartPoint()
        {
            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.SetStartPoint();
            }
        }
        public override void SetXY(double x, double y)
        {
            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.SetXY(x,y);
            }
        }

        public override void SetOldXY()
        {
            foreach (ShapeComponent sc in shapeComponents)
            {
                sc.SetOldXY();
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
