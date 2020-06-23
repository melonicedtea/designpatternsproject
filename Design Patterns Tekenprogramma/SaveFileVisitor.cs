using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    public class SaveFileVisitor : IVisitor
    {

        public override void Visit(MainWindow mainWindow)
        {


            //List<string> shapesListStrings = new List<string>();
            //foreach (Shape shape in myWin.canvas.Children)
            //{
            //    string line =
            //        shape.Name + " " +
            //        (int)Canvas.GetLeft(shape) + " " +
            //        (int)Canvas.GetTop(shape) + " " +
            //        (int)shape.Width + " " +
            //        (int)shape.Height;
            //    Console.WriteLine(line);
            //    shapesListStrings.Add(line);

            //}
            //File.WriteAllLines("Mytxt.txt", shapesListStrings.ToArray());
            //shapesListStrings.Clear();
        }
    }
}
