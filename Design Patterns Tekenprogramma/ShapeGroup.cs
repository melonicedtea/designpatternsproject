using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    public class ShapeGroup : ShapeComponent
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

        public ShapeComponent GetComponent(int componentIndex)
        {
            return shapeComponents[componentIndex];
        }

        public override void DisplayShapeInfo()
        {
            Console.WriteLine(GetGroupName());

            foreach(ShapeComponent sc in shapeComponents)
            {
                sc.DisplayShapeInfo();
            }
        }
    }
}
