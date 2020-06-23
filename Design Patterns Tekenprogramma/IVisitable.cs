using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
