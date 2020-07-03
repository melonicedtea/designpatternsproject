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
    interface ITask
    {
        void Execute();
        void Undo();
        void Redo();
    }
    class Executor
    {
        private List<ITask> taskList = new List<ITask>();
        public void AddTask(ITask task)
        {
            taskList.Add(task);
        }

        public void DoTasks()
        {
            foreach (ITask task in taskList)
            {
                task.Execute();
            }
            taskList.Clear();
        }
    }
    class SelectShape : ITask
    {
        private MyShape myShape;

        public SelectShape(MyShapeComponent msc)
        {
            myShape = msc as MyShape;
        }

        public void Execute()
        {
            myShape.SetStrokeColor(Brushes.Red);
            myShape.SetSelected(true);
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            myShape.SetStrokeColor(Brushes.LightBlue);
            myShape.SetSelected(false);
        }
    }
    class SelectShapeGroup : ITask
    {
        private MyShapeGroup myShapeGroup;

        public SelectShapeGroup(MyShapeComponent msc)
        {
            myShapeGroup = msc as MyShapeGroup;
        }

        public void Execute()
        {

            myShapeGroup.SetStrokeColor(Brushes.Red);
            myShapeGroup.SetSelected(true);
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            myShapeGroup.SetStrokeColor(Brushes.LightBlue);
            myShapeGroup.SetSelected(false);
        }
    }
    class UnselectShapes : ITask
    {
        public List<MyShapeComponent> selectedShapes;


        public UnselectShapes(List<MyShapeComponent> selectedShapes)
        {
            this.selectedShapes = selectedShapes;
        }

        public void Execute()
        {
            foreach (MyShapeComponent msc in selectedShapes)
            {
                msc.SetStrokeColor(Brushes.LightBlue);
                msc.SetSelected(false);
            }
            
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            Console.WriteLine("IMHERE"); 
            foreach (MyShapeComponent msc in selectedShapes)
            {
                msc.SetStrokeColor(Brushes.Red);
                msc.SetSelected(true);
            }
        }
    }
    class DrawShape : ITask
    {
        private MyShape myShape;

        public DrawShape(MyShapeComponent msc)
        {
            myShape = msc as MyShape;
        }

        public void Execute()
        {
            myShape.DrawFinished();
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            myShape.UndoDraw();
        }
    }

    class MoveShape : ITask
    {
        private MyShape myShape;

        public MoveShape(MyShapeComponent msc)
        {
            myShape = msc as MyShape;
        }

        public void Execute()
        {
            MoveShapeVisitor sv = new MoveShapeVisitor();
            myShape.Accept(sv);
        }

        public void Undo()
        {
            myShape.UndoMove();
        }

        public void Redo()
        {
            myShape.RedoMove();
        }

    }

    class MoveShapeGroup : ITask
    {
        private MyShapeGroup myShapeGroup;

        public MoveShapeGroup(MyShapeComponent myShapeGroup)
        {
            this.myShapeGroup = myShapeGroup as MyShapeGroup;
        }

        public void Execute()
        {
            //shapeGroup.MoveFinished();
            MoveShapeVisitor sv = new MoveShapeVisitor();
            myShapeGroup.Accept(sv);
        }

        public void Redo()
        {
            foreach (MyShape ms in myShapeGroup.GetComponents())
                ms.RedoMove();
        }

        public void Undo()
        {
            myShapeGroup.UndoMove();
        }

    }
    class EnlargeShape : ITask
    {
        private MyShape myShape;

        public EnlargeShape(MyShapeComponent shapeComponent)
        {
            myShape = shapeComponent as MyShape;
        }

        public void Execute()
        {
            EnlargeShapeVisitor enlargeShapeVisitor = new EnlargeShapeVisitor();
            myShape.Accept(enlargeShapeVisitor);
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            myShape.UndoEnlarge();
        }
    }
    class EnlargeShapeGroup : ITask
    {
        private MyShapeGroup myShapeGroup;

        public EnlargeShapeGroup(MyShapeComponent shapeComponent)
        {
            myShapeGroup = shapeComponent as MyShapeGroup;
        }

        public void Execute()
        {
            EnlargeShapeVisitor enlargeShapeVisitor = new EnlargeShapeVisitor();
            myShapeGroup.Accept(enlargeShapeVisitor);
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            myShapeGroup.UndoEnlarge();
        }
    }
    class ShrinkShape : ITask
    {
        private MyShape myShape;

        public ShrinkShape(MyShapeComponent shapeComponent)
        {
            myShape = shapeComponent as MyShape;
        }

        public void Execute()
        {
            Console.WriteLine("Height: "+myShape.h);
            ShrinkShapeVisitor shrinkShapeVisitor = new ShrinkShapeVisitor();
            myShape.Accept(shrinkShapeVisitor);
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            myShape.UndoShrink();
        }
    }
    class ShrinkShapeGroup : ITask
    {
        private MyShapeGroup myShapeGroup;

        public ShrinkShapeGroup(MyShapeComponent shapeComponent)
        {
            myShapeGroup = shapeComponent as MyShapeGroup;
        }

        public void Execute()
        {
            ShrinkShapeVisitor shrinkShapeVisitor = new ShrinkShapeVisitor();
            myShapeGroup.Accept(shrinkShapeVisitor);
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            myShapeGroup.UndoShrink();
        }
    }
    class AddOrnament : ITask
    {
        private MyShape myShape;
        private string position;
        private string text;
        private Canvas canvas;
        private OrnamentShapeDecorator decorator;

        public AddOrnament(MyShapeComponent shapeComponent, string position, string textBoxText, Canvas canvas)
        {
            myShape = shapeComponent as MyShape;
            this.position = position;
            text = textBoxText;
            this.canvas = canvas;
        }

        public void Execute()
        {
            decorator = new OrnamentShapeDecorator(myShape, position, text, canvas);
        }

        public void Redo()
        {
            decorator.AddOrnament();
        }

        public void Undo()
        {
            decorator.RemoveOrnament();
        }
    }
    class AddOrnamentGroup : ITask
    {
        private MyShapeGroup myShapeGroup;
        private string position;
        private string text;
        private Canvas canvas;
        private List<OrnamentShapeDecorator> decorators = new List<OrnamentShapeDecorator>();
        private OrnamentShapeDecorator decorator;

        public AddOrnamentGroup(MyShapeComponent shapeComponent, string position, string textBoxText, Canvas canvas)
        {
            myShapeGroup = shapeComponent as MyShapeGroup;
            this.position = position;
            text = textBoxText;
            this.canvas = canvas;
        }

        public void Execute()
        {
            foreach (MyShape ms in myShapeGroup.GetComponents())
            {
                decorator = new OrnamentShapeDecorator(ms, position, text, canvas);
                decorators.Add(decorator);
            }
        }

        public void Redo()
        {
            foreach (OrnamentShapeDecorator decorator in decorators)
            {
                decorator.AddOrnament();
            }
        }

        public void Undo()
        {
            foreach (OrnamentShapeDecorator decorator in decorators)
            {
                decorator.RemoveOrnament();
            }
        }
    }
    public class MakeGroup : ITask
    {
        private static int uniqueGroupNumber;
        private MyShapeGroup newGroup;
        private int amountInGroup = 0;
        public void Execute()
        {
            MainWindow myWin = (MainWindow)Application.Current.MainWindow;
            List<KeyVal<Shape, int>> existingShapes = myWin.existingShapes;
            List<MyShapeGroup> existingGroups = myWin.existingGroups;
            
            foreach (KeyVal<Shape, int> s in existingShapes)
            {
                bool isSelected = s.Id.Stroke == Brushes.Red;
                bool isInLegitGroup = s.Text != 999;
                if (isSelected && isInLegitGroup)
                {
                    amountInGroup++;
                }
            }

            bool everySelectedIsNotInGroup = amountInGroup == 0;
            if (everySelectedIsNotInGroup)
            {
                newGroup = new MyShapeGroup(uniqueGroupNumber);
                myWin.existingGroups.Add(newGroup);
                foreach (KeyVal<Shape, int> s in existingShapes)
                {
                    if (s.Id.Stroke == Brushes.Red)
                    {
                        foreach (MyShape ms in myWin.myShapes)
                        {
                            if (s.Id == ms.GetShape())
                            {
                                s.Text = uniqueGroupNumber;
                                ms.groupNumber = newGroup.groupNumber;
                                newGroup.Add(ms);
                            }
                        }
                    }
                }
                uniqueGroupNumber++;
                newGroup.DisplayShapeInfo();
            }
            else if (amountInGroup > 0)
            {
                foreach (KeyVal<Shape, int> s in existingShapes)
                {
                    if (s.Id.Stroke == Brushes.Red && myWin.currentGroup.groupNumber != s.Text)
                    {
                        if (s.Text == 999)
                        {
                            MyShape myShape = null;
                            s.Text = myWin.currentGroup.groupNumber;
                            foreach (MyShape ms in myWin.myShapes)
                            {
                                if (s.Id == ms.GetShape())
                                {
                                    myShape = ms;
                                }
                            }
                            if (myShape != null)
                            {
                                myShape.groupNumber = myWin.currentGroup.groupNumber;
                                myWin.existingGroups[myWin.currentGroup.groupNumber].Add(myShape);
                            }
                            
                        }
                        else if (s.Text != 999 && s.Text != myWin.currentGroup.groupNumber)
                        {
                            if (!myWin.existingGroups[myWin.currentGroup.groupNumber].GetComponents().Contains(myWin.existingGroups[s.Text]))
                            {
                                myWin.existingGroups[myWin.currentGroup.groupNumber].Add(myWin.existingGroups[s.Text]);
                                //myWin.existingGroups[s.Text].Add(myWin.existingGroups[myWin.currentGroup.groupNumber]);
                                myWin.addedGroups.Add(existingGroups[s.Text]);
                            }

                        }

                        //if (!myWin.existingGroups[s.Text].GetComponents().Contains(myWin.existingGroups[currentGroupNumber]))
                        //{
                        //    myWin.existingGroups[s.Text].Add(myWin.existingGroups[currentGroupNumber]);
                        //}
                    }
                }
                Console.WriteLine("---------------------------");
                existingGroups[myWin.currentGroup.groupNumber].DisplayShapeInfo();
            }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {

        }
    }

}
