﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patterns_Tekenprogramma
{
    class DrawShape : Task
    {
        private MyShape shape;

        public DrawShape(MyShape shape)
        {
            this.shape = shape;
        }

        public void execute()
        {
            shape.draw();
        }
    }
}
