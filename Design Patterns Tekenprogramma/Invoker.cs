using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
   
    class Invoker
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
}
