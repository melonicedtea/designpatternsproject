using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns_Tekenprogramma
{
    class Receiver
    {
        private List<Task> taskList = new List<Task>();

        public void addTask(Task task)
        {
            taskList.Add(task);
        }

        public void doTasks()
        {
            foreach (Task task in taskList)
            {
                task.execute();
            }
            taskList.Clear();
        }
    }
}
