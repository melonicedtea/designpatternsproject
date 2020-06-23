using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(MainWindow_KeyDown);//listen to keyboard key-presses 
            Closed += MainWindow_Closed;
            status3.IsChecked = true;//select-mode is checked by default

            LoadFile fileLoader = new LoadFile();
            fileLoader.loadFile();


        }

        List<string> shapesListStrings = new List<string>();
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            
            foreach (Shape shape in canvas.Children)
            {
                string line =
                    shape.Name + " " +
                   (int)Canvas.GetLeft(shape) + " " +
                    (int)Canvas.GetTop(shape) + " " +
                    (int)shape.Width + " " +
                    (int)shape.Height;
                Console.WriteLine(line);
                shapesListStrings.Add(line);

            }
            File.WriteAllLines("Mytxt.txt", shapesListStrings.ToArray());
            shapesListStrings.Clear();
            //SaveFileVisitor saveFileVisitor = new SaveFileVisitor();
            //Accept(saveFileVisitor);
        }


        //////////////////////INPUT MANAGER////////////////////////////////


        //TODO: more commenting
        private bool draw = false;
        private Point startPoint;
        private Point newPos;
        private bool drag = false;
        private bool dragGroup = false;
        private Shape shape;
        public List<KeyVal<Shape, int>> shapes = new List<KeyVal<System.Windows.Shapes.Shape, int>>();
        private string mode = "select";
        private bool shapeClicked = false;
        private MyShape myShape = null;
        private TextBlock textBlock = null;
        

        private List<ITask> taskList = new List<ITask>();
        private int counter = 0;

        private List<ShapeGroup> shapeGroups = new List<ShapeGroup>();
        private ShapeGroup everyShape = new ShapeGroup("group 0");
        private int uniqueGroupNumber = 0;
        private int groupNumber = 0;
        public List<List<object>> lists = new List<List<object>>();

        /// <summary>
        /// Handles radiobutton events
        /// 
        /// Use the radiobuttons to change mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {

            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            mode = Convert.ToString(radioButton.Content.ToString());
        }

        /// <summary>
        /// Handles mousedown events on canvas
        /// 
        /// Click on canvas to deselect
        /// Click on canvas to start drawing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            startPoint = e.GetPosition(canvas);

            if (mode == "select" && shapeClicked == false)
            {
                if (shapes != null)
                    foreach (KeyVal<System.Windows.Shapes.Shape, int> s in shapes)
                        s.Id.Stroke = Brushes.LightBlue;

                
            }

            //if (mode == "rect" || mode == "ellipse")
            //{

            //    draw = true;

            //    myShape = new MyShape();//contains actions
            //    DrawShape drawShapeTask = new DrawShape(myShape);// contains actions -> execute
            //    Invoker invoker = new Invoker();
            //    invoker.AddTask(drawShapeTask);// add
            //    invoker.DoTasks(); // do


            //}
            if (mode == "rect" || mode == "ellipse")
            {
                draw = true;
                myShape = new MyShape();//contains actions
                
                
            }
            if (mode == "rect")
            {
                DrawRectangleStrategy drawStrategy = DrawRectangleStrategy.GetInstance();
                myShape.SetDrawStrategy(drawStrategy);
                myShape.Draw();

                //RedFillShapeDecorator redFillMyShape = new RedFillShapeDecorator(myShape);
                //OrnamentShapeDecorator ornamentShapeDecorator = new OrnamentShapeDecorator(myShape, "top", "MFDSA");
                //DrawShape drawShapeTask = new DrawShape(myShape);// contains actions -> execute
                //Invoker invoker = new Invoker();
                //invoker.AddTask(drawShapeTask);// add
                //invoker.DoTasks(); // do
            }
            if (mode == "ellipse")
            {
                DrawEllipsesStrategy drawStrategy = DrawEllipsesStrategy.GetInstance();
                myShape.SetDrawStrategy(drawStrategy);
                myShape.Draw();
                //draw = true;
                //myShape.setDrawType(new ItDrawsEllipses());
                //myShape.Draw();
                //DrawShape drawShapeTask = new DrawShape(myShape);// contains actions -> execute
                //Invoker invoker = new Invoker();
                //invoker.AddTask(drawShapeTask);// add
                //invoker.DoTasks(); // do
            }
            


        }
        /// <summary>
        /// Handles mouse movement events on canvas
        ///
        /// Enable initial draw visualization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || shape == null || sender.GetType().Name == "Shape" || mode == "select")
                return;

            if (draw)
            {
                shape.CaptureMouse();
                //DrawHoldShape drawHoldShapeTask = new DrawHoldShape(myShape);// contains actions -> execute
                //Invoker receiver = new Invoker();
                //receiver.AddTask(drawHoldShapeTask);// add
                //receiver.DoTasks(); // do
                myShape.drawHold();
            }



        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (draw)
            {
                

                DrawFinishedShape drawFinishedShapeTask = new DrawFinishedShape(myShape);// contains actions -> execute 
                Invoker receiver = new Invoker();
                receiver.AddTask(drawFinishedShapeTask);// add
                receiver.DoTasks(); // do


                    while (taskList.Count > counter)
                    {
                        taskList.RemoveAt(counter);
                    }


                
                counter++;
                taskList.Add(drawFinishedShapeTask);
                Console.WriteLine("counter: " + counter);
                Console.WriteLine("taskList counter: " + taskList.Count);
                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }
                shape.ReleaseMouseCapture();
            }
            shape = null;
            draw = false;

        }
        MoveHoldShapeVisitor sv = new MoveHoldShapeVisitor();
        /// <summary>
        /// Handles mousedown events
        /// 
        /// Click on shape to select it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {

            shapeClicked = true;

            shape = sender as System.Windows.Shapes.Shape;

            myShape = new MyShape(shape);

            if (mode == "select")
            {
                shape.CaptureMouse();

                startPoint = e.GetPosition(canvas);

                sv.SetStartPoint();

                drag = true;

                shape.Stroke = Brushes.Red;


                //shapeComponents = everyShape.GetComponents();
                //foreach (ShapeComponent shapeComponent in shapeComponents)
                //{
                //    shapeComponent.SetStartPoint();
                //}
                //everyShape.SetStartPoint();


                if (shapeGroups.Count > 0)
                {                  
                    for(int i = 0; i < shapeGroups.Count; i++)
                    {
                        foreach (ShapeComponent sc in shapeGroups[i].GetComponents())
                        {
                            if (sc is MyShape)
                            {
                                MyShape ms = sc as MyShape;
                                if (ms.GetShape() == shape)
                                {
                                    dragGroup = true;
                                    drag = false;
                                    if (i != 999)
                                    {
                                        groupNumber = i;
                                    }
                                }
                            }
                            
                        }
                    }

                    shapeGroups[groupNumber].SetStartPoint();
                }
                

            }
        }
        /// <summary>
        /// Click and Drag to move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Released || shape == null)
                return;

            if (dragGroup)
            {

                //MoveHoldShape moveHoldShapeTask = new MoveHoldShape(everyShape);// contains actions -> execute
                //Invoker invoker = new Invoker();
                //invoker.AddTask(moveHoldShapeTask);// add
                //invoker.DoTasks(); // do

                //shapeGroups[groupNumber].MoveHold();

                //MoveHoldShape moveHoldShapeTask = new MoveHoldShape(myShape);// contains actions -> execute
                //Invoker invoker = new Invoker();
                //invoker.AddTask(moveHoldShapeTask);// add
                //invoker.DoTasks(); // do


                shapeGroups[groupNumber].Accept(sv);
            }

            if (drag)
            {
                //MoveHoldShape moveHoldShapeTask = new MoveHoldShape(myShape);// contains actions -> execute
                //Invoker invoker = new Invoker();
                //invoker.AddTask(moveHoldShapeTask);// add
                //invoker.DoTasks(); // do

                myShape.Accept(sv);
            }
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
            shapeClicked = false;
            if (drag)
            {


                // MyShape myShape = new MyShape();//contains actions
                MoveFinishedShape moveFinishedShapeTask = new MoveFinishedShape(myShape);// contains actions -> execute
                Invoker invoker = new Invoker();
                invoker.AddTask(moveFinishedShapeTask);// add
                invoker.DoTasks(); // do

                while (taskList.Count > counter)
                {
                    taskList.RemoveAt(counter);
                }

                counter++;
                taskList.Add(moveFinishedShapeTask);
                Console.WriteLine("counter: " + counter);
                Console.WriteLine("taskList counter: " + taskList.Count);

                

                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }

              
            }

            if (dragGroup)
            {
                
                    MoveFinishedShapeGroup moveFinishedShapeGroupTask = new MoveFinishedShapeGroup(shapeGroups[groupNumber]);// contains actions -> execute
                    Invoker invoker = new Invoker();
                    invoker.AddTask(moveFinishedShapeGroupTask);// add
                    invoker.DoTasks(); // do
                
                while (taskList.Count > counter)
                {
                    taskList.RemoveAt(counter);
                }

                counter++;
                taskList.Add(moveFinishedShapeGroupTask);
                Console.WriteLine("counter: " + counter);
                Console.WriteLine("taskList counter: " + taskList.Count);



                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }
            }
            drag = false;
            dragGroup = false;

            shape.ReleaseMouseCapture();


        }
        /// <summary>
        /// Scroll to resize shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            shape = sender as System.Windows.Shapes.Shape;
            if (mode == "select")
            {
                myShape = new MyShape(shape);
                if (e.Delta > 0)
                {
                    EnlargeShape enlargeShapeTask = new EnlargeShape(myShape);// contains actions -> execute
                    Invoker invoker = new Invoker();
                    invoker.AddTask(enlargeShapeTask);// add
                    invoker.DoTasks(); // do

                    while (taskList.Count > counter)
                    {
                        taskList.RemoveAt(counter);
                    }

                    counter++;
                    taskList.Add(enlargeShapeTask);
                    Console.WriteLine("counter: " + counter);
                    Console.WriteLine("taskList counter: " + taskList.Count);

                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                if (e.Delta < 0)
                {
                    ShrinkShape shrinkShapeTask = new ShrinkShape(myShape);// contains actions -> execute
                    Invoker invoker = new Invoker();
                    invoker.AddTask(shrinkShapeTask);// add
                    invoker.DoTasks(); // do

                    while (taskList.Count > counter)
                    {
                        taskList.RemoveAt(counter);
                    }

                    counter++;
                    taskList.Add(shrinkShapeTask);
                    Console.WriteLine("counter: " + counter);
                    Console.WriteLine("taskList counter: " + taskList.Count);



                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
            }


        }
        /// <summary>
        /// Undo: CTRL+Z
        /// Redo : CTRL+Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z)
            {//TODO fix this
                //MyShape myShape = new MyShape();//contains actions
                //MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
                //DrawShape drawShapeTask = new DrawShape(myShape);
                //Invoker invoker = new Invoker();

                //invoker.AddTask(drawShapeTask);
                //invoker.AddTask(moveShapeTask);

                //invoker.UndoTasks();

                if (taskList.Count > 0)
                {
                    counter--;
                    if (counter < 0)
                    {
                        counter = 0;
                    }
                    ITask currentTask = taskList[counter];
                    currentTask.Undo();
                    Console.WriteLine("counter: " + counter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                e.Handled = true;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Y)
            {//TODO fix this
             //MyShape myShape = new MyShape(shape);//contains actions
             //MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
             //Invoker invoker = new Invoker();
             //invoker.AddTask(moveShapeTask);
             //invoker.RedoTasks();

                if (taskList.Count > counter)
                {

                    ITask currentTask = taskList[counter];
                    currentTask.Execute();
                    counter++;
                    Console.WriteLine("counter: " + counter);
                    Console.WriteLine("taskList counter: " + taskList.Count);

                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                e.Handled = true;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.G)
            {
                ShapeGroup newGroup;
                List<System.Windows.Shapes.Shape> tempShapes = new List<System.Windows.Shapes.Shape>();
                int amountInGroup = 0;
                bool allInGroup = shapes.Count == amountInGroup;
                foreach (KeyVal<System.Windows.Shapes.Shape, int> s in shapes)
                {
                    if (s.Id.Stroke == Brushes.Red)
                    {
                        tempShapes.Add(s.Id);
                        if (s.Text != 999)
                        {
                            amountInGroup++;
                        }

                    }
                }
                if (amountInGroup == 0)
                {
                    newGroup = new ShapeGroup("group" + uniqueGroupNumber);
                    shapeGroups.Add(newGroup);
                    foreach (KeyVal<System.Windows.Shapes.Shape, int> s in shapes)
                    {
                        if (s.Id.Stroke == Brushes.Red)
                        {
                            s.Text = uniqueGroupNumber;
                            MyShape myShape = new MyShape(s.Id);
                            myShape.SetGroupName(newGroup.GetGroupName());
                            newGroup.Add(myShape);
                        }
                    }
                    uniqueGroupNumber++;
                    newGroup.DisplayShapeInfo();
                }
                else if (amountInGroup > 0)
                {
                    foreach (KeyVal<System.Windows.Shapes.Shape, int> s in shapes)
                    {
                        if (s.Id.Stroke == Brushes.Red)
                        {
                            if (s.Text == 999)
                            {
                                s.Text = groupNumber;
                                MyShape myShape = new MyShape(s.Id);
                                myShape.SetGroupName(shapeGroups[groupNumber].GetGroupName());
                                shapeGroups[groupNumber].Add(myShape);
                            }
                            else if (s.Text != 999 && s.Text != groupNumber)
                            {
                                if (!shapeGroups[groupNumber].GetComponents().Contains(shapeGroups[s.Text]))
                                {
                                    shapeGroups[groupNumber].Add(shapeGroups[s.Text]);
                                }

                            }
                        }
                    }
                    shapeGroups[groupNumber].DisplayShapeInfo();
                    Console.WriteLine(shapeGroups[groupNumber].GetComponents().Count);
                }

            }
            if (e.Key == Key.T)
            {
                OrnamentShapeDecorator ornament = new OrnamentShapeDecorator(myShape, "top", textBox.Text);
            }
        }

        ///////////////////////GETTERS, SETTERS//////////////////////////////
        public void SetShape(System.Windows.Shapes.Shape currentShape)
        {
            shape = currentShape;
        }
        public void AddMethods(System.Windows.Shapes.Shape shape)
        {
            shape.MouseDown += Shape_MouseDown;
            shape.MouseMove += Shape_MouseMove;
            shape.MouseUp += Shape_MouseUp;
            shape.MouseWheel += Shape_MouseWheel;
        }

        public void AddShape(System.Windows.Shapes.Shape shape)
        {
            KeyVal<System.Windows.Shapes.Shape, int> kv = new KeyVal<Shape, int>(shape, 999);
            shapes.Add(kv);
        }
        public void AddShape(System.Windows.Shapes.Shape shape, int gn)
        {
            KeyVal<System.Windows.Shapes.Shape, int> kv = new KeyVal<System.Windows.Shapes.Shape, int>(shape, gn);
            shapes.Add(kv);
        }

        public void AddGroup(ShapeGroup shapeGroup)
        {
            shapeGroups.Add(shapeGroup);
        }
        public string GetMode()
        {
            return mode;
        }
        public Shape GetShape()
        {
            return shape;
        }

        public Point GetStartPoint()
        {
            return startPoint;
        }

        public bool GetDrag()
        {
            return drag;
        }

        public bool GetShapeClicked()
        {
            return shapeClicked;
        }

        public void Accept(SaveFileVisitor visitor)
        {
            visitor.Visit();
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }
    }
}
