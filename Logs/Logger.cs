﻿
namespace lab2.Logs {
    public class Logger {
        private readonly string _logFilePath;

        public Logger(string logFilePath) {
            _logFilePath = logFilePath;
        }

        public void Log(string message) {
            try {
                using StreamWriter writer = File.AppendText(_logFilePath);
                writer.WriteLine($"[{DateTime.Now}] - {message}");
            } catch (Exception ex) {
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }
    }
}
