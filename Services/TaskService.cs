using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskAppManager1
{
    public class TaskService : ITaskService
    {
        private readonly List<Task> tasks;
        private readonly JsonDataService jsonDataService;
        private int currentId;

        public TaskService()
        {
            jsonDataService = new JsonDataService("tasks.json");
            tasks = jsonDataService.LoadData();
            currentId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        }

        public void CreateTask(Task task)
        {
            task.Id = currentId++;
            tasks.Add(task);
            SaveData();
        }

        public Task ReadTask(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTask(int id, Task task)
        {
            var existingTask = ReadTask(id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.IsCompleted = task.IsCompleted;
                SaveData();
            }
        }

        public void DeleteTask(int id)
        {
            var task = ReadTask(id);
            if (task != null)
            {
                tasks.Remove(task);
                SaveData();
            }
        }

        public List<Task> GetAllTasks()
        {
            return tasks;
        }

        private void SaveData()
        {
            jsonDataService.SaveData(tasks);
        }
    }
}