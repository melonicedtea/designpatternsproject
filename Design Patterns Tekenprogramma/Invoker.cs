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

        public void UndoTasks()
        {
            foreach (Task task in taskList)
            {
                task.Undo();
            }
            taskList.Clear();
        }

        public void RedoTasks()
        {
            foreach (Task task in taskList)
            {
                task.Redo();
            }
            taskList.Clear();
        }

        //Task task;

        //public Invoker(Task task)
        //{
        //    this.task = task;
        //}

        //public void DoTask()
        //{
        //    task.Execute();
        //}

        //public void UndoTask()
        //{
        //    task.Undo();
        //}
    }
}
