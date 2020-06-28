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
    class FileLoader 

    {
        Shape currentShape;
        MyShapeGroup parentGroup;
        MyShapeGroup childGroup;
        MyShape myShape;
        bool putInGroup = false;
        int n = 0;
        string path = "Mytxt.txt";
        MainWindow myWin = (MainWindow)Application.Current.MainWindow;


        public void loadFile()
        {
            if (File.Exists(path))
            {
                foreach (var myString in File.ReadAllLines(path))
                {
                    string[] splittedText = myString.Split(' ');
                    Console.WriteLine(splittedText[0]);
                    if (splittedText[0] == "ellipse" && !putInGroup)
                    {
                        currentShape = new Ellipse()
                        {
                            Name = "ellipse",
                            Stroke = Brushes.LightBlue,
                            StrokeThickness = 2,
                            Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue),
                            Width = Convert.ToInt16(splittedText[3]),
                            Height = Convert.ToInt16(splittedText[4]),


                        };
                        myWin.AddShape(currentShape);
                        myWin.AddMethods(currentShape);
                        Canvas.SetLeft(currentShape, Convert.ToInt16(splittedText[1]));
                        Canvas.SetTop(currentShape, Convert.ToInt16(splittedText[2]));
                        myWin.canvas.Children.Add(currentShape);

                    }
                    if (splittedText[0] == "rectangle" && !putInGroup)
                    {
                        currentShape = new Rectangle()
                        {
                            Name = "rectangle",
                            Stroke = Brushes.LightBlue,
                            StrokeThickness = 2,
                            Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue),
                            Width = Convert.ToInt16(splittedText[3]),
                            Height = Convert.ToInt16(splittedText[4]),


                        };
                        myWin.AddShape(currentShape);
                        myWin.AddMethods(currentShape);
                        Canvas.SetLeft(currentShape, Convert.ToInt16(splittedText[1]));
                        Canvas.SetTop(currentShape, Convert.ToInt16(splittedText[2]));
                        myWin.canvas.Children.Add(currentShape);

                    }
                    if (splittedText[0] == "group")
                    {
                        parentGroup = new MyShapeGroup(Convert.ToInt16(splittedText[1]));
                        myWin.AddGroup(parentGroup);
                        putInGroup = true;
                        n = Convert.ToInt32(splittedText[1]);

                    }
                    if (splittedText[0] == "\tgroup")
                    {
                        childGroup = new MyShapeGroup(Convert.ToInt16(splittedText[1]));
                        myWin.AddGroup(childGroup);
                        putInGroup = true;

                        n = Convert.ToInt32(splittedText[1]);
                        parentGroup.Add(childGroup);

                    }
                    if (splittedText[0] == "\tellipse" && putInGroup)
                    {
                        Console.WriteLine("TEEEEE");
                        currentShape = new Ellipse()
                        {
                            Name = "ellipse",
                            Stroke = Brushes.LightBlue,
                            StrokeThickness = 2,
                            Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue),
                            Width = Convert.ToInt16(splittedText[3]),
                            Height = Convert.ToInt16(splittedText[4]),


                        };
                        myWin.AddShape(currentShape);
                        myWin.AddMethods(currentShape);
                        Canvas.SetLeft(currentShape, Convert.ToInt16(splittedText[1]));
                        Canvas.SetTop(currentShape, Convert.ToInt16(splittedText[2]));
                        myWin.canvas.Children.Add(currentShape);
                        myWin.canvas.Children.Add(currentShape);
                        myShape = new MyShape(currentShape);
                        parentGroup.Add(myShape);
                    }

                    if (splittedText[0] == "\t\trectangle" && putInGroup)
                    {
                        Console.WriteLine("TEEEEE");
                        currentShape = new Rectangle()
                        {
                            Name = "rectangle",
                            Stroke = Brushes.LightBlue,
                            StrokeThickness = 2,
                            Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue),
                            Width = Convert.ToInt16(splittedText[3]),
                            Height = Convert.ToInt16(splittedText[4]),


                        };
                        myWin.AddShape(currentShape);
                        myWin.AddMethods(currentShape);
                        Canvas.SetLeft(currentShape, Convert.ToInt16(splittedText[1]));
                        Canvas.SetTop(currentShape, Convert.ToInt16(splittedText[2]));
                        myWin.canvas.Children.Add(currentShape);
                        childGroup.Add(myShape);
                    }
                }
            }
            

            
        }
          
    }

    
}
