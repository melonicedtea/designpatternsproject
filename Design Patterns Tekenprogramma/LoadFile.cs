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
    class LoadFile 

    {
        Shape currentShape;
        string path = "Mytxt.txt";
        MainWindow myWin = (MainWindow)Application.Current.MainWindow;


        public void loadFile()
        {
            if (File.Exists(path))
            {
                foreach (var myString in File.ReadAllLines(path))
                {
                    string[] splittedText = myString.Split(' ');
                    if (splittedText[0] == "ellipse")
                    {
                        currentShape = new Ellipse()
                        {
                            Name = "ellipse",
                            Stroke = Brushes.LightBlue,
                            StrokeThickness = 2,
                            Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue),
                            Width = Convert.ToInt32(splittedText[3]),
                            Height = Convert.ToInt32(splittedText[4]),


                        };
                        myWin.AddShape(currentShape);
                        myWin.AddMethods(currentShape);
                        Canvas.SetLeft(currentShape, Convert.ToInt32(splittedText[1]));
                        Canvas.SetTop(currentShape, Convert.ToInt32(splittedText[2]));
                        myWin.canvas.Children.Add(currentShape);

                    }
                    if (splittedText[0] == "rectangle")
                    {
                        currentShape = new Rectangle()
                        {
                            Name = "rectangle",
                            Stroke = Brushes.LightBlue,
                            StrokeThickness = 2,
                            Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue),
                            Width = Convert.ToInt32(splittedText[3]),
                            Height = Convert.ToInt32(splittedText[4]),


                        };
                        myWin.AddShape(currentShape);
                        myWin.AddMethods(currentShape);
                        Canvas.SetLeft(currentShape, Convert.ToInt32(splittedText[1]));
                        Canvas.SetTop(currentShape, Convert.ToInt32(splittedText[2]));
                        myWin.canvas.Children.Add(currentShape);

                    }
                }
            }
            

            
        }
          
    }

    
}
