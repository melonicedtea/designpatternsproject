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
using System.Windows.Navigation;
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
            //fileLoader.loadFile();


        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            foreach (Shape shape in canvas.Children)
            {
                string line =
                    shape.Name + " " +
                    Canvas.GetLeft(shape) + " " +
                    Canvas.GetTop(shape) + " " +
                    shape.Width + " " +
                    shape.Height;
                Console.WriteLine(line);
                shapesListStrings.Add(line);

            }
            File.WriteAllLines("Mytxt.txt", shapesListStrings.ToArray());
            shapesListStrings.Clear();
        }


        //////////////////////INPUT MANAGER////////////////////////////////


        //TODO: more commenting
        private bool draw = false;
        private Point startPoint;
        private Point newPos;
        private bool drag = false;
        private Shape shape;
        private List<Shape> shapes = new List<Shape>();
        private string mode = "select";
        private bool shapeClicked = false;
        private MyShape myShape = null;
        private List<string> shapesListStrings = new List<string>();

        private List<Task> taskList = new List<Task>();
        private int counter = 0;

        private List<ShapeGroup> shapeGroups = new List<ShapeGroup>();
        private ShapeGroup everyShape = new ShapeGroup("group 0");
        List<ShapeComponent> shapeComponents = new List<ShapeComponent>();

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
                    foreach (Shape shape in shapes)
                        shape.Stroke = Brushes.LightBlue;
                
                
            }

            if (mode == "rect" || mode == "ellipse")
            {

                draw = true;

                myShape = new MyShape();//contains actions
                DrawShape drawShapeTask = new DrawShape(myShape);// contains actions -> execute
                Invoker invoker = new Invoker();
                invoker.AddTask(drawShapeTask);// add
                invoker.DoTasks(); // do


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
                DrawHoldShape drawHoldShapeTask = new DrawHoldShape(myShape);// contains actions -> execute
                Invoker receiver = new Invoker();
                receiver.AddTask(drawHoldShapeTask);// add
                receiver.DoTasks(); // do
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
                foreach (Task t in taskList)
                {
                    Console.WriteLine(t);
                }
                shape.ReleaseMouseCapture();
            }
            shape = null;
            draw = false;

        }
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

            shape = sender as Shape;

            myShape = new MyShape(shape);

            if (mode == "select")
            {
                shape.CaptureMouse();

                startPoint = e.GetPosition(canvas);

                myShape.SetStartPoint();

                drag = true;

                shape.Stroke = Brushes.Red;

                shapeComponents = everyShape.GetComponents();
                foreach (ShapeComponent shapeComponent in shapeComponents)
                {
                    shapeComponent.SetStartPoint();
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

            if (drag)
            {
                foreach (ShapeComponent shapeComponent in shapeComponents)
                {
                    MoveHoldShape moveHoldShapeTask = new MoveHoldShape(shapeComponent);// contains actions -> execute
                    Invoker invoker = new Invoker();
                    invoker.AddTask(moveHoldShapeTask);// add
                    invoker.DoTasks(); // do
                }

                //MoveHoldShape moveHoldShapeTask = new MoveHoldShape(myShape);// contains actions -> execute
                //Invoker invoker = new Invoker();
                //invoker.AddTask(moveHoldShapeTask);// add
                //invoker.DoTasks(); // do

            }
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //shape = null;
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

                shape.ReleaseMouseCapture();

                foreach (Task t in taskList)
                {
                    Console.WriteLine(t);
                }
            }
            drag = false;





        }
        /// <summary>
        /// Scroll to resize shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            shape = sender as Shape;
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

                    foreach (Task t in taskList)
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



                    foreach (Task t in taskList)
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
                    Task currentTask = taskList[counter];
                    currentTask.Undo();
                    Console.WriteLine("counter: " + counter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (Task t in taskList)
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

                    Task currentTask = taskList[counter];
                    currentTask.Execute();
                    counter++;
                    Console.WriteLine("counter: " + counter);
                    Console.WriteLine("taskList counter: " + taskList.Count);

                    foreach (Task t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                e.Handled = true;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.G)
            {
               

                foreach(Shape s in shapes)
                {
                    if (s.Stroke == Brushes.Red)
                    {
                        MyShape myShape = new MyShape(s);
                        everyShape.Add(myShape);
                    }
                }

                everyShape.SetGroupName();
                everyShape.DisplayShapeInfo();
                //everyShape.Enlarge();
            }
        }

        ///////////////////////GETTERS, SETTERS//////////////////////////////
        public void SetShape(Shape currentShape)
        {
            shape = currentShape;
        }
        public void AddMethods(Shape shape)
        {
            shape.MouseDown += Shape_MouseDown;
            shape.MouseMove += Shape_MouseMove;
            shape.MouseUp += Shape_MouseUp;
            shape.MouseWheel += Shape_MouseWheel;
        }


        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
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
    }
}
