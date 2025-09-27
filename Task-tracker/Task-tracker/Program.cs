using System;
using System.Text.Json;
using Task_tracker.Models;
using Task_tracker.Services;

namespace Task_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                PrintBanner();

                Console.WriteLine("Welcome! Type 'help' to see available commands.");

                string? userInput = Console.ReadLine();

                StringParser(userInput!);

                Console.Write("Press any key to continue...");

                Console.ReadLine();
            }
        }
        static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════╗");
            Console.WriteLine("║     Task tracker     ║");
            Console.WriteLine("╚══════════════════════╝\n");
        }

        static void StringParser(string input)
        {
            string lineWithCommandPart = input.Substring(9);

            string[] parts = lineWithCommandPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string command = parts[0];

            int? index = null;
            if (parts.Length > 1 && int.TryParse(parts[1], out int parsedIndex))
            {
                index = parsedIndex;
            }

            string userMessage = "";
            if (index != null && parts.Length > 2)
            {
                userMessage = string.Join(' ', parts.Skip(2));
            }
            else if (index == null && parts.Length > 1)
            {
                userMessage = string.Join(' ', parts.Skip(1));
            }

            HandleCommand(command, index, userMessage);
        }

        static void HandleCommand(string command, int? index = 0, string userInput = "")
        {
            var file = new FileIOService("UserData.json");
            TodoModel task = new TodoModel();

            switch (command)
            {
                case "add":
                    task.Description = userInput;
                    task.Status = Status.todo;
                    file.AddTask(task);
                    Console.WriteLine($"Task added successfully (ID: {task.Id})");
                    break;
                case "update":
                    file.UpdateTask(index, userInput);
                    Console.WriteLine("Task update successfully");
                    break;
                case "delete":
                    file.DeleteTask(index);
                    Console.WriteLine("Task delete successfully");
                    break;
                case "list":
                    file.PrintAll();
                    break;
                case "mark-in-progress":
                    file.MarkingTask(index, Status.inProgress);
                    Console.WriteLine("Task status changed successfully");
                    break;
                case "mark-done":
                    file.MarkingTask(index, Status.done);
                    Console.WriteLine("Task status changed successfully");
                    break;
                case "todo":
                    file.MarkingTask(index, Status.todo);
                    Console.WriteLine("Task status changed successfully");
                    break;
                case "help":
                    Console.WriteLine("**add <task text>**\r\n    Adds a new task.\r\n    Example: task-cli add \"Buy groceries\"\r\n    Output: Task added successfully (ID: X)\r\n\r\n**update <ID> <new task text>**\r\n    Updates the text of an existing task by its ID.\r\n    Example: task-cli update 1 \"Buy groceries and cook dinner\"\r\n\r\n**delete <ID>**\r\n    Deletes a task by its ID.\r\n    Example: task-cli delete 1\r\n\r\n**mark-in-progress <ID>**\r\n    Marks a task as \"In Progress\" by its ID.\r\n    Example: task-cli mark-in-progress 1\r\n\r\n**mark-done <ID>**\r\n    Marks a task as \"Done\" by its ID.\r\n    Example: task-cli mark-done 1\r\n\r\n**list [status]**\r\n    Displays a list of all tasks or tasks with a specific status.\r\n    - No arguments: Displays all tasks.\r\n    - Statuses: `done`, `todo`, `in-progress`.\r\n    Examples:\r\n        task-cli list\r\n        task-cli list done\r\n        task-cli list todo\r\n        task-cli list in-progress");
                    break;
            }
        }
    }
}
