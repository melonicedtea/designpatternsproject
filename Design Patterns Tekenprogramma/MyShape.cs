using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
 class MyShape
    {
        static bool isCalled = false;
        static Point startPoint;
        static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        Shape currentShape;
        static Point newPos;
        static int oldcnt = myWin.scc;


        public void move()
        {
            int newcnt = myWin.scc;
           
            newPos = Mouse.GetPosition(myWin.canvas);
            currentShape = myWin.GetShape();

            if (!isCalled)
            {
                startPoint = myWin.GetStartPoint();
                isCalled = true;
                
            }

            double x = Canvas.GetLeft(currentShape);
            double y = Canvas.GetTop(currentShape);

            Canvas.SetLeft(currentShape, x + (newPos.X - startPoint.X));
            Canvas.SetTop(currentShape, y + (newPos.Y - startPoint.Y));

            startPoint = newPos;



            
        }
    }
}
