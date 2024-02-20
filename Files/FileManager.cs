using System.Text.Json;
using lab2.Logs;
using lab2.Models;

namespace lab2.Files; 
public class FileManager {
    private readonly string _filePath;
    private readonly Logger _logger;

    public FileManager(string filePath, Logger logger) {
        _filePath = filePath;
        _logger = logger;
    }

    public List<Faculty>? LoadData() {
        try {
            if (!File.Exists(_filePath)) return new List<Faculty>();
            string json = File.ReadAllText(_filePath);
            _logger.Log("Data loaded successfully");
            return JsonSerializer.Deserialize<List<Faculty>>(json);
        } catch (Exception ex) {
            _logger.Log($"Error occurred while loading data: {ex.Message}");
            throw new Exception($"Error occurred while loading data: {ex.Message}");
        }
    }
}
