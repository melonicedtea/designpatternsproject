using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
   
    class Invoker
    {
        private List<Task> taskList = new List<Task>();
        public void AddTask(Task task)
        {
            taskList.Add(task);
        }

        public void DoTasks()
        {
            foreach (Task task in taskList)
            {
                task.Execute();
            }
            taskList.Clear();
        }
    }
}
