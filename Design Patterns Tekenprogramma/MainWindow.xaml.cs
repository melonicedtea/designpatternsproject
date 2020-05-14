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

            
        }
        
        

        private bool drawMode = false;
        private Point startPoint;
        private Point newPos;
        private bool drag = false;
        private Shape shape;
        public List<Shape> shapes = new List<Shape>();
        public string mode;
        public bool shapeClicked = false;
        public int scc;
        MyShape myShape = null;


        public Shape GetShape()
        {
            return shape;
        }

        public Point GetStartPoint()
        {
            return startPoint;
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {

            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            mode = Convert.ToString(radioButton.Content.ToString());
        }

        //private void AddRectangleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    drawmode = true;
        //}

        public void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mode == "select" && shapeClicked == false)
            {
                if (shapes != null)
                    foreach (Shape shape in shapes)
                        shape.Stroke = Brushes.LightBlue;

            }

            myShape = new MyShape();//contains actions
            DrawShape drawShapeTask = new DrawShape(myShape);// contains actions -> execute
            Invoker receiver = new Invoker();
            receiver.addTask(drawShapeTask);// add
            receiver.doTasks(); // do

        }

        public void setShape(Shape currentShape)
        {
            shape = currentShape;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || shape == null || sender.GetType().Name == "Shape" || mode == "select")
                return;



            DrawHoldShape drawHoldShapeTask = new DrawHoldShape(myShape);// contains actions -> execute
            Invoker receiver = new Invoker();
            receiver.addTask(drawHoldShapeTask);// add
            receiver.doTasks(); // do


        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
            shape = null;
            drawMode = false;

        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {

            MyShape.setIsCalled();
            shapeClicked = true;

            shape = sender as System.Windows.Shapes.Shape;
            startPoint = e.GetPosition(canvas);
            drag = true;

            if (mode == "select")
                shape.Stroke = Brushes.Red;

        }
        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Released || shape == null)
                return;

            if (drag)
            {
                //newPos = Mouse.GetPosition(canvas);
                //double x = Canvas.GetLeft(shape);
                //double y = Canvas.GetTop(shape);

                //Canvas.SetLeft(shape, x + (newPos.X - startPoint.X));
                //Canvas.SetTop(shape, y + (newPos.Y - startPoint.Y));

                //startPoint = newPos;


                MyShape myShape = new MyShape();//contains actions
                MoveShape moveShapeTask = new MoveShape(myShape);// contains actions -> execute
                Invoker receiver = new Invoker();
                receiver.addTask(moveShapeTask);// add
                receiver.doTasks(); // do

            }
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            shape = null;
            drag = false;
            shapeClicked = false;
        }

        private void Shape_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (mode == "select")
            {
                shape = sender as System.Windows.Shapes.Shape;
            shape.Width = shape.Width * (1 + e.Delta * 0.001);
            shape.Height = shape.Height * (1 + e.Delta * 0.001);
            }
        }

        public void addMethods(Shape shape)
        {
            shape.MouseDown += Shape_MouseDown;
            shape.MouseMove += Shape_MouseMove;
            shape.MouseUp += Shape_MouseUp;
            shape.MouseWheel += Shape_MouseWheel;
        }
    }
}
