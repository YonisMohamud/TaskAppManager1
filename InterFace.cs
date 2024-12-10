using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppManager1
{


    
    public interface ITaskService
    {
        void CreateTask(Task task);
        Task ReadTask(int id);
        void UpdateTask(int id, Task task);
        void DeleteTask(int id);
        List<Task> GetAllTasks();
    }
}









