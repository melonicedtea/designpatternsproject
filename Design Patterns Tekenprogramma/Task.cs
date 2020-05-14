using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    interface Task
    {
        void Execute();
        void Undo();
        void Redo();
    }
}
