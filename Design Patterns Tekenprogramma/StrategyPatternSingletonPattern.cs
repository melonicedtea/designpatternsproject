using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    public interface IDrawStrategy
    {
        Shape GetDrawStrategy();
    }
    public class DrawRectangleStrategy : IDrawStrategy
    {
        private static DrawRectangleStrategy instance;

        private DrawRectangleStrategy() { }

        public static DrawRectangleStrategy GetInstance()
        {
            if (instance == null)
            {
                instance = new DrawRectangleStrategy();
            }
            return instance;
        }

        public Shape GetDrawStrategy()
        {
            Shape rectangle = new Rectangle()
            {
                Name = "rectangle",
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.AliceBlue)
            };
            return rectangle; 
        }

        public void showMessage()
        {
            Console.WriteLine("Draw RECTS");
        }
    }
    public class DrawEllipsesStrategy : IDrawStrategy
    {
        private static DrawEllipsesStrategy instance;

        private DrawEllipsesStrategy() { }

        public static DrawEllipsesStrategy GetInstance()
        {
            if (instance == null)
            {
                instance = new DrawEllipsesStrategy();
            }
            return instance;
        }
        public Shape GetDrawStrategy()
        {
            Shape ellipse = new Ellipse()
            {
                Name = "ellipse",
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.AliceBlue)
            };
            return ellipse;
        }
        public void showMessage()
        {
            Console.WriteLine("Draw elllleelel");
        }
    }
}
