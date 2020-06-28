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
    public partial class MainWindow : Window, IVisitable
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(MainWindow_KeyDown);//listen to keyboard key-presses 
            Closed += MainWindow_Closed;// handle close event: save file
            status3.IsChecked = true;//select-mode is checked by default

            FileLoader fileLoader = new FileLoader();
           // fileLoader.loadFile();
        }

        public List<MyShapeGroup> eatenGroups = new List<MyShapeGroup>();
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            List<string> shapesListStrings = new List<string>();
            List<MyShapeGroup> finalGroup = new List<MyShapeGroup>();
            foreach (var item in eatenGroups)
            {
                Console.WriteLine("imeaten: " + item);
            }
            foreach (MyShapeGroup group in existingGroups)
            {
                if (!eatenGroups.Contains(group))
                {
                    finalGroup.Add(group);
                }             
                
            }
            foreach (MyShapeGroup group in finalGroup)
            {

                shapesListStrings.AddRange(group.GetStrings());

                Console.WriteLine("IMIN" + group.groupNumber);
            }
            //shapesListStrings.AddRange(finalGroup[0].GetStrings());
            foreach (MyShape ms in myShapes)
            {
                if (ms.groupNumber == 999)
                {
                    shapesListStrings.Add(ms.ToString());
                }

            }
            

            int wordsToTab = 0;
            for (int i = 0; i < shapesListStrings.Count;i++)
            { 
                if (wordsToTab > 0 )
                {
                    shapesListStrings[i] = shapesListStrings[i].Insert(0, "\t");
                    wordsToTab--;
                    
                }
                if (shapesListStrings[i].Contains("group"))
                {
                   int lastGroup = i;
                    foreach (char c in shapesListStrings[i])
                    {
                        if (char.IsDigit(c))
                        {
                            wordsToTab = Convert.ToInt32(c.ToString());
                            Console.WriteLine(wordsToTab);
                        }
                    }
                    char tab = '\t';
                    foreach (char c in shapesListStrings[lastGroup])
                    {
                        if (c == tab)
                        {
                            for (int j = 1; j < wordsToTab + 1; j++)
                            {
                                shapesListStrings[lastGroup + j] = shapesListStrings[lastGroup + j].Insert(0, "\t");
                            }
                        }
                    }
                }
            }

            File.WriteAllLines("Mytxt.txt", shapesListStrings.ToArray());

            //SaveFileVisitor saveFileVisitor = new SaveFileVisitor();
            //Accept(saveFileVisitor);
        }

        //TODO: more commenting
        private Point startPoint = new Point(0,0); // startPoint to be set at mousedown event
        private string mode = "select"; // existing modes: rect, ellipse, select 
        private bool draw = false; // am I drawing a shape?      
        private bool drag = false; // am I dragging a shape? 
        private bool dragGroup = false; // am i dragging a group?
        private bool shapeClicked = false;// am i clicking a shape?

        private MyShape myShape = null;// custom shape object

        private Shape shape = null; // traditional shape object
        public List<KeyVal<Shape, int>> existingShapes = new List<KeyVal<Shape, int>>(); // list of traditional shapes (as Shape) with groupnumber (as int) (custom KeyVal object, see class KeyVal)

        public List<MyShape> myShapes = new List<MyShape>();
        
        private List<ITask> taskList = new List<ITask>(); // List of tasks/actions
        private int taskCounter = 0; // now I am at task {taskCounter}

        public List<MyShapeGroup> existingGroups = new List<MyShapeGroup>(); // List of existing groups

        public int uniqueGroupNumber = 0; // unique number to given to a group
        public MyShapeGroup currentGroup = null; // current selected group: {currentGroupNumber}

        /// <summary>
        /// Handles radiobutton events
        /// 
        /// Use the radiobuttons to change mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            mode = Convert.ToString(radioButton.Content.ToString());

            if (existingShapes != null)
                foreach (KeyVal<Shape, int> s in existingShapes)
                    s.Id.Stroke = Brushes.LightBlue;
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
                if (existingShapes != null)
                {
                    foreach (KeyVal<Shape, int> s in existingShapes)
                    {
                        s.Id.Stroke = Brushes.LightBlue;
                    }
                }

                //Console.WriteLine("-----------------------");
                //existingGroups[0].DisplayShapeInfo();
            }

            if (mode == "rect" || mode == "ellipse")
            {
                draw = true;
                myShape = new MyShape();
                myShapes.Add(myShape);
                myShape.points.Add(new Point(startPoint.X, startPoint.Y));
                myShape.moves++;
                myShape.groupNumber = 999;
            }
            if (mode == "rect")
            {
                DrawRectangleStrategy drawStrategy = DrawRectangleStrategy.GetInstance();
                myShape.SetDrawStrategy(drawStrategy);
                myShape.Draw();
            }
            if (mode == "ellipse")
            {
                DrawEllipsesStrategy drawStrategy = DrawEllipsesStrategy.GetInstance();
                myShape.SetDrawStrategy(drawStrategy);
                myShape.Draw();
            }
        }
        /// <summary>
        /// Handles mouse movement events on canvas
        ///
        /// Enable draw visualization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)// ellipses
                return;

            if (draw)
            {
                shape.CaptureMouse();
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
                DrawShape drawFinishedShapeTask = new DrawShape(myShape);// contains actions -> execute 
                Executor executor = new Executor();
                executor.AddTask(drawFinishedShapeTask);// add
                executor.DoTasks(); // do
                Console.WriteLine("HEIGTH AT INIT: "+ myShape.h);

                while (taskList.Count > taskCounter)
                {
                    taskList.RemoveAt(taskCounter);
                }
                
                taskCounter++;
                taskList.Add(drawFinishedShapeTask);

                Console.WriteLine("counter: " + taskCounter);
                Console.WriteLine("taskList counter: " + taskList.Count);
                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }
                shape.ReleaseMouseCapture();
            }
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

            foreach(MyShape ms in myShapes)
            {
                if(ms.GetShape() == shape)
                {
                    myShape = ms;
                }
            }
            Console.WriteLine("GROUPNUMBER: " + myShape.groupNumber);
            if (existingGroups.Count > 0)
            {
                foreach (MyShapeGroup msg in existingGroups)
                {
                    foreach (MyShapeComponent sc in msg.GetComponents())
                    {
                        if (sc is MyShape)
                        {
                            MyShape ms = sc as MyShape;
                            //foreach (KeyVal<Shape, int> s in existingShapes)
                            //{
                            //    if (ms.groupNumber == s.Text && s.Id == shape)
                            //    {
                            //            currentGroup = msg;
                            //    }
                            //}
                            if (ms.groupNumber == myShape.groupNumber)
                            {
                                currentGroup = msg;
                            }
                        }
                    }
                }

            }
            
            if (mode == "select")
            {
                if (currentGroup != null)
                {
                    if (myShape.groupNumber == currentGroup.groupNumber)
                    {
                        Console.WriteLine(currentGroup.groupNumber);
                        currentGroup.SetStrokeColor(Brushes.Red);
                    }
                }
                
                myShape.SetStrokeColor(Brushes.Red);
                Console.WriteLine(myShape.groupNumber);
            }

            if (mode == "drag")
            {
                drag = true;

                shape.CaptureMouse();

                shape.Stroke = Brushes.Yellow;

                startPoint = e.GetPosition(canvas);

                myShape.SetStartPoint();

                if (currentGroup != null)
                {
                    if (myShape.groupNumber == currentGroup.groupNumber)
                    {
                        currentGroup.SetStartPoint();
                        dragGroup = true;
                        drag = false;
                        currentGroup.SetStrokeColor(Brushes.Yellow);
                    }
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
            if (dragGroup)
            {
                currentGroup.MoveHold();
            }

            if (drag)
            {
                myShape.MoveHold();
            }
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shapeClicked = false;
            if (drag)
            {
                // MyShape myShape = new MyShape();//contains actions
                MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
                Executor invoker = new Executor();
                invoker.AddTask(moveShapeTask);// add
                invoker.DoTasks(); // do

                while (taskList.Count > taskCounter)
                {
                    taskList.RemoveAt(taskCounter);
                }

                taskCounter++;
                taskList.Add(moveShapeTask);
                Console.WriteLine("counter: " + taskCounter);
                Console.WriteLine("taskList counter: " + taskList.Count);

                

                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }

                myShape.SetStrokeColor(Brushes.LightBlue);
                shape.ReleaseMouseCapture();
            }

            if (dragGroup)
            {

                MoveShapeGroup moveFinishedShapeGroupTask = new MoveShapeGroup(currentGroup);// contains actions -> execute
                Executor invoker = new Executor();
                invoker.AddTask(moveFinishedShapeGroupTask);// add
                invoker.DoTasks(); // do

                while (taskList.Count > taskCounter)
                {
                    taskList.RemoveAt(taskCounter);
                }

                taskCounter++;
                taskList.Add(moveFinishedShapeGroupTask);
                Console.WriteLine("counter: " + taskCounter);
                Console.WriteLine("taskList counter: " + taskList.Count);

                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }

                currentGroup.SetStrokeColor(Brushes.LightBlue);
                shape.ReleaseMouseCapture();
            }
            drag = false;
            dragGroup = false;

            
        }
        /// <summary>
        /// Scroll to resize shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool resizeGroup = false;
            shape = sender as Shape;
            if (mode == "select")
            {
                //if (existingGroups.Count > 0)
                //{
                //    foreach (MyShapeGroup msg in existingGroups)
                //    {
                //        foreach (MyShapeComponent sc in msg.GetComponents())
                //        {
                //            if (sc is MyShape)
                //            {
                //                MyShape ms = sc as MyShape;
                //                foreach (KeyVal<Shape, int> s in existingShapes)
                //                {
                //                    if (ms.groupNumber == s.Text)
                //                    {
                //                        currentGroup = msg;
                //                    }
                //                }

                //            }
                //        }
                //    }
                //}
                if (currentGroup != null)
                {
                    if (myShape.groupNumber == currentGroup.groupNumber)
                    {
                        resizeGroup = true;
                    }
                }
                //myShape = new MyShape(shape);
                if (resizeGroup == false)
                {
                    if (e.Delta > 0)
                    {
                        EnlargeShape enlargeShapeTask = new EnlargeShape(myShape);// contains actions -> execute
                        Executor invoker = new Executor();
                        invoker.AddTask(enlargeShapeTask);// add
                        invoker.DoTasks(); // do

                        while (taskList.Count > taskCounter)
                        {
                            taskList.RemoveAt(taskCounter);
                        }

                        taskCounter++;
                        taskList.Add(enlargeShapeTask);
                        Console.WriteLine("counter: " + taskCounter);
                        Console.WriteLine("taskList counter: " + taskList.Count);

                        foreach (ITask t in taskList)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    if (e.Delta < 0)
                    {
                        ShrinkShape shrinkShapeTask = new ShrinkShape(myShape);// contains actions -> execute
                        Executor invoker = new Executor();
                        invoker.AddTask(shrinkShapeTask);// add
                        invoker.DoTasks(); // do

                        while (taskList.Count > taskCounter)
                        {
                            taskList.RemoveAt(taskCounter);
                        }

                        taskCounter++;
                        taskList.Add(shrinkShapeTask);
                        Console.WriteLine("counter: " + taskCounter);
                        Console.WriteLine("taskList counter: " + taskList.Count);



                        foreach (ITask t in taskList)
                        {
                            Console.WriteLine(t);
                        }
                    }
                }
                if (resizeGroup == true)
                {
                    if (e.Delta > 0)
                    {
                        EnlargeShapeGroup enlargeShapeGroupTask = new EnlargeShapeGroup(currentGroup);// contains actions -> execute
                        Executor executor = new Executor();
                        executor.AddTask(enlargeShapeGroupTask);// add
                        executor.DoTasks(); // do

                        while (taskList.Count > taskCounter)
                        {
                            taskList.RemoveAt(taskCounter);
                        }

                        taskCounter++;
                        taskList.Add(enlargeShapeGroupTask);
                        Console.WriteLine("counter: " + taskCounter);
                        Console.WriteLine("taskList counter: " + taskList.Count);

                        foreach (ITask t in taskList)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    if (e.Delta < 0)
                    {
                        ShrinkShapeGroup shrinkShapeGroupTask = new ShrinkShapeGroup(currentGroup);// contains actions -> execute
                        Executor invoker = new Executor();
                        invoker.AddTask(shrinkShapeGroupTask);// add
                        invoker.DoTasks(); // do

                        while (taskList.Count > taskCounter)
                        {
                            taskList.RemoveAt(taskCounter);
                        }

                        taskCounter++;
                        taskList.Add(shrinkShapeGroupTask);
                        Console.WriteLine("counter: " + taskCounter);
                        Console.WriteLine("taskList counter: " + taskList.Count);



                        foreach (ITask t in taskList)
                        {
                            Console.WriteLine(t);
                        }
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
            {
                if (taskList.Count > 0)
                {
                    taskCounter--;
                    if (taskCounter < 0)
                    {
                        taskCounter = 0;
                    }
                    ITask currentTask = taskList[taskCounter];
                    currentTask.Undo();
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                e.Handled = true;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Y)
            {

                if (taskList.Count > taskCounter)
                {

                    ITask currentTask = taskList[taskCounter];
                    currentTask.Redo();
                    taskCounter++;
                    Console.WriteLine("counter: " + taskCounter);
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
                MakeGroup makeGroupTask = new MakeGroup();// contains actions -> execute
                Executor executor = new Executor();
                executor.AddTask(makeGroupTask);// add
                executor.DoTasks(); // do

                while (taskList.Count > taskCounter)
                {
                    taskList.RemoveAt(taskCounter);
                }

                taskCounter++;
                taskList.Add(makeGroupTask);
                Console.WriteLine("counter: " + taskCounter);
                Console.WriteLine("taskList counter: " + taskList.Count);



                foreach (ITask t in taskList)
                {
                    Console.WriteLine(t);
                }

            }
            bool group = false;
            //if (existingGroups.Count > 0)
            //{
            //    foreach (MyShapeGroup msg in existingGroups)
            //    {
            //        foreach (MyShapeComponent sc in msg.GetComponents())
            //        {
            //            if (sc is MyShape)
            //            {
            //                MyShape ms = sc as MyShape;
            //                foreach (KeyVal<Shape, int> s in existingShapes)
            //                {
            //                    if (ms.groupNumber == s.Text && s.Id == shape)
            //                    {
            //                        currentGroup = msg;
            //                    }
            //                }

            //            }
            //        }
            //    }

            //}
            if (currentGroup != null)
            {
                if (myShape.groupNumber == currentGroup.groupNumber)
                {
                    group = true;
                }
            }
            
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.T)
            {
                
                if (!group)
                {
                    AddOrnament addOrnamentTask = new AddOrnament(myShape, "top", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                if (group)
                {
                    AddOrnamentGroup addOrnamentTask = new AddOrnamentGroup(currentGroup, "top", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }

            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.B)
            {
                if (!group)
                {
                    AddOrnament addOrnamentTask = new AddOrnament(myShape, "bottom", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                if (group)
                {
                    AddOrnamentGroup addOrnamentTask = new AddOrnamentGroup(currentGroup, "bottom", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.L)
            {
                if (!group)
                {
                    AddOrnament addOrnamentTask = new AddOrnament(myShape, "left", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                if (group)
                {
                    AddOrnamentGroup addOrnamentTask = new AddOrnamentGroup(currentGroup, "left", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.R)
            {
                if (!group)
                {
                    AddOrnament addOrnamentTask = new AddOrnament(myShape, "right", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
                if (group)
                {
                    AddOrnamentGroup addOrnamentTask = new AddOrnamentGroup(currentGroup, "right", textBox.Text);
                    Executor executor = new Executor();
                    executor.AddTask(addOrnamentTask);
                    executor.DoTasks();

                    while (taskList.Count > taskCounter)
                    {
                        taskList.RemoveAt(taskCounter);
                    }

                    taskCounter++;
                    taskList.Add(addOrnamentTask);
                    Console.WriteLine("counter: " + taskCounter);
                    Console.WriteLine("taskList counter: " + taskList.Count);
                    foreach (ITask t in taskList)
                    {
                        Console.WriteLine(t);
                    }
                }
            }
        }

        ///////////////////////GETTERS, SETTERS, ADDERS//////////////////////////////
        public void SetShape(Shape currentShape)
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
            KeyVal<Shape, int> kv = new KeyVal<Shape, int>(shape, 999);
            existingShapes.Add(kv);

        }

        public List<KeyVal<Shape, int>> GetShapes()
        {
            return existingShapes;
        }

        public List<MyShapeGroup> GetShapeGroups()
        {
            return existingGroups;
        }

        public void AddGroup(MyShapeGroup shapeGroup)
        {
            existingGroups.Add(shapeGroup);
        }

        public Point GetStartPoint()
        {
            return startPoint;
        }

        //////////////////////////////////////////////////////VISITOR PATTERN ACCEPT
        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
