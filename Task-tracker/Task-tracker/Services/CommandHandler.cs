using Task_tracker.Models;

namespace Task_tracker.Services
{
    internal class CommandHandler
    {
        public static List<string> AvailableCommands()
        {
            List<string> availableCommands = new List<string>()
            {
                "add",
                "update",
                "delete",
                "list",
                "mark-in-progress",
                "mark-done",
                "mark-todo",
                "help",
                "list-by-done",
                "list-by-in-progress",
                "list-by-todo"
            };

            return availableCommands;
        }

        public static void Handle(string command, string argument = "", int? index = 0, string userInput = "")
        {
            var file = new FileIOService("UserData.json");
            var todos = file.GetData();
            TodoModel task = new TodoModel();

            switch (command, argument)
            {
                case ("add", ""):
                    task.Description = userInput;
                    task.Status = Status.todo;
                    file.AddTask(task);
                    Console.WriteLine($"Task added successfully (ID: {task.Id})");
                    break;
                case ("update", ""):
                    if (file.UpdateTask(index, userInput))
                        Console.WriteLine("Task update successfully");
                    else if (index == null)
                        Console.WriteLine("You need to enter the index");
                    else
                        Console.WriteLine("Index is incorrectly specified");
                    break;
                case ("delete", ""):
                    if (file.DeleteTask(index))
                        Console.WriteLine("Task delete successfully");
                    else if (index == null)
                        Console.WriteLine("You need to enter the index");
                    else
                        Console.WriteLine("Index is incorrectly specified");
                    break;
                case ("list", ""):
                    Print(todos);
                    break;
                case ("mark-in-progress", ""):
                    if (file.MarkingTask(index, Status.inProgress))
                        Console.WriteLine("Task status changed successfully");
                    else if (index == null)
                        Console.WriteLine("You need to enter the index");
                    else
                        Console.WriteLine("Index is incorrectly specified");
                    break;
                case ("mark-done", ""):
                    if (file.MarkingTask(index, Status.done))
                        Console.WriteLine("Task status changed successfully");
                    else if (index == null)
                        Console.WriteLine("You need to enter the index");
                    else
                        Console.WriteLine("Index is incorrectly specified");
                    break;
                case ("mark-todo", ""):
                    if (file.MarkingTask(index, Status.todo))
                        Console.WriteLine("Task status changed successfully");
                    else if (index == null)
                        Console.WriteLine("You need to enter the index");
                    else
                        Console.WriteLine("Index is incorrectly specified");
                    break;
                case ("list", "done"):
                    var listByDone = todos.Where(t => t.Status == Status.done).ToList();
                    Print(listByDone);
                    break;
                case ("list", "todo"):
                    var listByInProgress = todos.Where(t => t.Status == Status.todo).ToList();
                    Print(listByInProgress);
                    break;
                case ("list","in-progress"):
                    var listByTodo = todos.Where(t => t.Status == Status.inProgress).ToList();
                    Print(listByTodo);
                    break;
                case ("help", ""):
                    Console.WriteLine("**add <task text>**\r\n    Adds a new task.\r\n    Example: task-cli add \"Buy groceries\"\r\n    Output: Task added successfully (ID: X)\r\n\r\n**update <ID> <new task text>**\r\n    Updates the text of an existing task by its ID.\r\n    Example: task-cli update 1 \"Buy groceries and cook dinner\"\r\n\r\n**delete <ID>**\r\n    Deletes a task by its ID.\r\n    Example: task-cli delete 1\r\n\r\n **mark-todo <ID>**\r\n Marks a task as \"Todo\" by its ID.\r\n Example: task-cli mark-todo 1\r\n\r\n **mark-in-progress <ID>**\r\n    Marks a task as \"In Progress\" by its ID.\r\n    Example: task-cli mark-in-progress 1\r\n\r\n**mark-done <ID>**\r\n    Marks a task as \"Done\" by its ID.\r\n    Example: task-cli mark-done 1\r\n\r\n**list [status]**\r\n    Displays a list of all tasks or tasks with a specific status.\r\n    - No arguments: Displays all tasks.\r\n    - Statuses: `done`, `todo`, `in-progress`.\r\n    Examples:\r\n        \r        task-cli list done\r\n        task-cli list todo\r\n        task-cli list in-progress");
                    break;
            }
        }

        private static void Print(List<TodoModel> todos)
        {
            foreach (var item in todos)
            {
                Console.WriteLine($"ID: {item.Id}\nDescription: {item.Description}\nStatus: {item.Status.ToDisplayString()}\nCreated at: {item.CreatedAt}\nUpdated at: {item.UpdatedAt}");
            }
        }
    }
}
