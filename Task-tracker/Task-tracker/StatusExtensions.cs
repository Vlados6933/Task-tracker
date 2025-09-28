using Task_tracker.Models;

namespace Task_tracker
{
    internal static class StatusExtensions
    {
        public static string ToDisplayString(this Status status)
        {
            switch (status) 
            {
                case Status.done: return "done";
                case Status.todo: return "todo";
                case Status.inProgress: return "in-progress";
                default: return status.ToString();
            }
        }
    }
}
