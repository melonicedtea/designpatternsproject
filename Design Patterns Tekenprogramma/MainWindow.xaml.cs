using System;
using System.Collections.Generic;
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
            KeyDown += new KeyEventHandler(Window_KeyDown);
            
        }
        
   
        //////////////////////INPUT MANAGER////////////////////////////////


        //TODO: more commenting
        private bool drawMode = false;
        private Point startPoint;
        private Point newPos;
        private bool drag = false;
        private Shape shape;
        private List<Shape> shapes = new List<Shape>();
        private string mode;
        private bool shapeClicked = false;
        MyShape myShape = null;

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

            myShape = new MyShape();//contains actions
            DrawShape drawShapeTask = new DrawShape(myShape);// contains actions -> execute
            Invoker invoker = new Invoker();
            invoker.AddTask(drawShapeTask);// add
            invoker.DoTasks(); // do

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

            DrawHoldShape drawHoldShapeTask = new DrawHoldShape(myShape);// contains actions -> execute
            Invoker receiver = new Invoker();
            receiver.AddTask(drawHoldShapeTask);// add
            receiver.DoTasks(); // do

        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
            shape = null;
            drawMode = false;

            
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
            MyShape.SetIsCalled();
            shapeClicked = true;

            shape = sender as System.Windows.Shapes.Shape;
            startPoint = e.GetPosition(canvas);
            

            if (mode == "select")
            {
                drag = true;
                shape.Stroke = Brushes.Red;
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


                MyShape myShape = new MyShape();//contains actions
                MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
                Invoker invoker = new Invoker();
                invoker.AddTask(moveShapeTask);// add
                invoker.DoTasks(); // do

            }
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //shape = null;
            drag = false;
            shapeClicked = false;
        }
        /// <summary>
        /// Scroll to resize shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (mode == "select")
            {
                shape = sender as Shape;
                shape.Width += shape.Width * (1 + e.Delta * 0.001);
                shape.Height += shape.Height * (1 + e.Delta * 0.001);
            }

            
        }
        /// <summary>
        /// Undo: CTRL+Z
        /// Redo : CTRL+Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z )
            {//TODO fix this
                MyShape myShape = new MyShape();//contains actions
                MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
                moveShapeTask.Undo();
                e.Handled = true;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Y)
            {//TODO fix this
                MyShape myShape = new MyShape();//contains actions
                MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
                moveShapeTask.Redo();
                e.Handled = true;
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
    }
}
