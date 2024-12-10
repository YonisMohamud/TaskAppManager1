

namespace TaskAppManager1


{
    class Program
    {
        static void Main(string[] args)
        {
            var taskService = new TaskService();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome To TaskAppManager");
                Console.WriteLine("(1) Create a task");
                Console.WriteLine("(2) Show all to-dos");
                Console.WriteLine("(3) Update an to-do");
                Console.WriteLine("(4) Delete an to-do");
                Console.WriteLine("(5) End the program");
                Console.Write("Välj ett alternativ: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateTask(taskService);
                        break;
                    case "2":
                        ShowAllTasks(taskService);
                        break;
                    case "3":
                        UpdateTask(taskService);
                        break;
                    case "4":
                        DeleteTask(taskService);
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Avslutar...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val!");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
        }

        static void CreateTask(TaskService taskService)
        {
            Console.Write("Titel: ");
            string title = Console.ReadLine();

            Console.Write("Beskrivning: ");
            string description = Console.ReadLine();

            Console.Write("Förfallodatum (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var dueDate))
            {
                Console.WriteLine("Ogiltigt datum.");
                return;
            }

            var task = new Task { Title = title, Description = description, DueDate = dueDate };
            taskService.CreateTask(task);
            Console.WriteLine("Uppgiften har skapats!");
        }

        static void ShowAllTasks(TaskService taskService)
        {
            var tasks = taskService.GetAllTasks();
            if (tasks.Count == 0)
            {
                Console.WriteLine("Inga uppgifter hittades.");
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id} - {task.Title} - Klar: {task.IsCompleted}");
            }
        }

        static void UpdateTask(TaskService taskService)
        {
            Console.Write("Ange ID för att uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) return;

            var task = taskService.ReadTask(id);
            if (task == null)
            {
                Console.WriteLine("Uppgift hittades inte.");
                return;
            }

            Console.Write("Ny titel: ");
            task.Title = Console.ReadLine();

            Console.Write("Ny beskrivning: ");
            task.Description = Console.ReadLine();

            Console.Write("Nytt förfallodatum (yyyy-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out var newDueDate))
            {
                task.DueDate = newDueDate;
            }

            taskService.UpdateTask(id, task);
            Console.WriteLine("Uppgift uppdaterad!");
        }

        static void DeleteTask(TaskService taskService)
        {
            Console.Write("Ange ID för att ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) return;

            taskService.DeleteTask(id);
            Console.WriteLine("Task Removed!");
        }
    }
}

