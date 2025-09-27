

namespace Task_tracker.Models
{
    enum Status
    {
        done,
        todo,
        inProgress
    }
    internal class TodoModel
    {
        public int Id { get; set; } = 0;
        public string? Description { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get;} = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public TodoModel () { }

        public TodoModel (int id, string? description, Status status)
        {
            Id = id;
            Description = description;
            Status = status;
        }
    }
}
