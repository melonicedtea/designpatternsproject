using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Design_Patterns_Tekenprogramma
{
    public class MyShapeGroup : MyShapeComponent, IVisitable
    {
        List<MyShapeComponent> shapeComponents = new List<MyShapeComponent>();

        string groupName;
        public int groupNumber;

        public MyShapeGroup(int groupNumber)
        {
            groupName = "group " + groupNumber;
            this.groupNumber = groupNumber;

        }

        public string GetGroupName() { return groupName; }

        public override void Add(MyShapeComponent shapeComponent)
        {
            shapeComponents.Add(shapeComponent);
        }

        public override List<MyShapeComponent> GetComponents()
        {
            return shapeComponents;
        }

        public override void DisplayShapeInfo()
        {
            Console.WriteLine("group " + shapeComponents.Count);

            foreach(MyShapeComponent sc in shapeComponents)
            {
                sc.DisplayShapeInfo();
            }
        }

        public override List<string> GetStrings()
        {
            List<string> groupString = new List<string>();
            groupString.Add("group " + shapeComponents.Count);
            foreach (MyShapeComponent sc in shapeComponents)
            {
                groupString.AddRange(sc.GetStrings());
            }

            return groupString;
        }

        public override void UndoEnlarge()
        {
            foreach (MyShapeComponent sc in shapeComponents)
            {
                sc.UndoEnlarge();
            }
        }

        public override void MoveHold()
        {
            foreach (MyShapeComponent sc in shapeComponents)
            {
                sc.MoveHold();
            }
        }

        public override void MoveFinished()
        {
            foreach (MyShapeComponent sc in shapeComponents)
            {
                MoveShapeVisitor sv = new MoveShapeVisitor();
                sc.Accept(sv);
            }
        }
        public override void UndoMove()
        {
            foreach (MyShapeComponent sc in shapeComponents)
            {
                sc.UndoMove();
            }
        }

        //public override void SetGroupName(string groupName)
        //{
        //    foreach (MyShapeComponent sc in shapeComponents)
        //    {
        //        sc.SetGroupName(groupName);
        //    }
        //}
        public override void SetStartPoint()
        {
            foreach (MyShapeComponent sc in shapeComponents)
            {
                sc.SetStartPoint();
            }
        }

        public override void SetStrokeColor(SolidColorBrush color)
        {
            foreach (MyShapeComponent sc in shapeComponents)
            {
                sc.SetStrokeColor(color);
            }
        }


        public override void Accept(Visitor visitor)
        { 
            visitor.Visit(this);
        }

    }
}
