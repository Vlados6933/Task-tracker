using System.Globalization;
using System.Text.Json;
using Task_tracker.Models;

namespace Task_tracker.Services
{
    internal class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        private List<TodoModel> LoadAll()
        {
            if (!File.Exists(PATH))
                return new List<TodoModel>();

            using FileStream fs = new FileStream(PATH, FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<TodoModel>>(fs) ?? new List<TodoModel>();
        }

        private void SaveAll(List<TodoModel> todos)
        {
            using FileStream fs = new FileStream(PATH, FileMode.Create, FileAccess.Write);
            JsonSerializer.Serialize(fs, todos);
        }

        public void AddTask(TodoModel todo)
        {
            var todos = LoadAll();

            if (todo.Id == 0)
            {
                if (todos.Count > 0)
                {
                    todo.Id = todos.Max(t => t.Id) + 1;
                }
                else
                {
                    todo.Id = 1;
                }
            }

            todos.Add(todo);
            SaveAll(todos);
        }

        public bool UpdateTask(int? id, string newDescription)
        {
            var todos = LoadAll();
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null) return false;

            todo.Description = newDescription;
            todo.UpdatedAt = DateTime.Now;

            SaveAll(todos);
            return true;
        }

        public bool MarkingTask(int? id, Status newStatus)
        {
            var todos = LoadAll();
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null) return false;

            todo.Status = newStatus;
            todo.UpdatedAt = DateTime.Now;

            SaveAll(todos);
            return true;
        }

        public bool DeleteTask(int? id)
        {
            var todos = LoadAll();
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null) return false;

            todos.Remove(todo);

            SaveAll(todos);
            return true;
        }

        public void PrintAll()
        {
            var todos = LoadAll();

            foreach (var item in todos)
            {
                Console.WriteLine($"ID: {item.Id}\nDescription: {item.Description}\nStatus: {item.Status}\nCreated at: {item.CreatedAt}\nUpdated at: {item.UpdatedAt}\n");
            }
        }
    }
}
