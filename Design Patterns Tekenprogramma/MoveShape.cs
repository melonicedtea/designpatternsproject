﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class MoveShape : Task
    {
        private MyShape shape;

        public MoveShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void Execute()
        {
            shape.Move();
        }

        public void Undo()
        {
            shape.UndoMove();
        }

        public void Redo()
        {
            shape.RedoMove();
        }
    }
}
