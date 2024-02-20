
using lab2.Logs;

namespace lab2 {
    internal class Program {
        private static void Main() {
            string logFilePath = "log.txt";
            Logger logger = new(logFilePath);
            CommandBoard board = new(Console.WriteLine, Console.ReadLine, "settings.json", logger);
            board.Execute();
        }
    }
}
