namespace Task_tracker.Services
{
    internal class StringParser
    {
        public static void Parser(string userInput)
        {
            if (!string.IsNullOrWhiteSpace(userInput) && !userInput.Contains('~'))
            {
                if (!userInput.StartsWith("task-cli"))
                {
                    Console.WriteLine("Request must be started with 'task-cli'\n");
                    return;
                }

                string lineWithCommandPart = userInput.Substring(8).Trim();
                if (string.IsNullOrWhiteSpace(lineWithCommandPart))
                {
                    Console.WriteLine("Bad request, type 'task-cli help' to see available commands.\n");
                    return;
                }

                string[] parts = lineWithCommandPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0];

                if (!CommandHandler.AvailableCommands().Contains(command))
                {
                    Console.WriteLine($"Unknown command '{command}'. Type 'task-cli help' to see available commands.\n");
                    return;
                }

                int? index = null;
                if (parts.Length > 1 && int.TryParse(parts[1], out int parsedIndex))
                {
                    index = parsedIndex;
                }

                string argument = "";
                if (parts.Length > 1)
                {
                    if (parts[1].Contains("done") || parts[1].Contains("todo") || parts[1].Contains("in-progress"))
                    {
                        argument = parts[1];
                    }
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

                CommandHandler.Handle(command, argument, index, userMessage);
            }
        }
    }
}
