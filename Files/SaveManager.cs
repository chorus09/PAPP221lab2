using System.Text.Json;
using lab2.Logs;
using lab2.Models;

namespace lab2.Files; 
public class SaveManager {
    private readonly string _filePath;
    private readonly Logger _logger;

    public SaveManager(string filePath, Logger logger) {
        _filePath = filePath;
        _logger = logger;
    }

    public void SaveData(List<Faculty> faculties) {
        try {
            if (!File.Exists(_filePath)) {
                File.Create(_filePath).Close();
                _logger.Log($"New file created at {_filePath}");
            }
            string json = JsonSerializer.Serialize(faculties);
            File.WriteAllText(_filePath, json);
            _logger.Log("Data saved successfully");
        } catch (Exception ex) {
            _logger.Log($"Error occurred while saving data: {ex.Message}");
            throw new Exception($"Error occurred while saving data: {ex.Message}");
        }
    }
}
