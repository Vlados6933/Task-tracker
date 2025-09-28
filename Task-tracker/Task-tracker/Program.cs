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
                Console.WriteLine("Welcome! Type 'task-cli help' to see available commands.\n");
                StringParser.Parser(Console.ReadLine()!);
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════╗");
            Console.WriteLine("║     Task tracker     ║");
            Console.WriteLine("╚══════════════════════╝\n");
        }
    }
}
