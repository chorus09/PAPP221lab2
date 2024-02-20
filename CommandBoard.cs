using lab2.Files;
using lab2.Logs;
using lab2.Manages;
using lab2.Models;
using lab2.Operations;
using System;

namespace lab2 {
    public delegate string? ReadDataHandler();

    public class CommandBoard {
        private readonly Logger _logger;
        private readonly UniversityManage _universityManage;
        private readonly SaveManager _saveManager;
        private readonly FileManager _fileManager;

        public Action<string>? BoardMessage;
        public ReadDataHandler? ReadMessage;

        public CommandBoard(Action<string>? action, ReadDataHandler? readMessage, string filePath, Logger logger) {
            BoardMessage = action;
            ReadMessage = readMessage;
            _universityManage = new UniversityManage(logger);
            _saveManager = new SaveManager(filePath, logger);
            _fileManager = new FileManager(filePath, logger);
            _logger = logger;
        }

        public void Execute() {
            LoadData();

            while (true) {
                BoardMessage?.Invoke("TUM BOARD\n" +
                    "Choose an option:\n" +
                    "1 - Faculty Operations\n" +
                    "2 - General Operations\n" +
                    "ESC - exit");

                string? choice = ReadMessage?.Invoke();
                switch (choice) {
                    case "1":
                        ExecuteFacultyOperations();
                        break;
                    case "2":
                        ExecuteGeneralOperations();
                        break;
                    case "ESC":
                        SaveData(); // Save data to file when exiting the program
                        Environment.Exit(0);
                        break;
                    default:
                        BoardMessage?.Invoke("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ExecuteFacultyOperations() {
            List<Faculty> faculties = _universityManage.GetAllFaculties();
            if (faculties.Count == 0) {
                BoardMessage?.Invoke("There are no faculties available. Please create a faculty first.");
                return;
            }

            BoardMessage?.Invoke("Select a faculty to perform operations:");
            for (int i = 0; i < faculties.Count; i++) {
                BoardMessage?.Invoke($"{i + 1}. {faculties[i].Name}");
            }

            int facultyChoice = -1;
            while (facultyChoice < 1 || facultyChoice > faculties.Count) {
                string? choice = ReadMessage?.Invoke();
                if (int.TryParse(choice, out int parsedChoice)) {
                    facultyChoice = parsedChoice;
                } else {
                    BoardMessage?.Invoke("Invalid choice. Please enter a valid faculty number.");
                }
            }

            Faculty chosenFaculty = faculties[facultyChoice - 1];
            FacultyOperations facultyOperations = new FacultyOperations(this, chosenFaculty, _logger);
            facultyOperations.Execute();
        }

        private void ExecuteGeneralOperations() {
            GeneralOperations generalOperations = new GeneralOperations(this, _universityManage, _logger);
            generalOperations.Execute();
        }

        private void SaveData() {
            try {
                _saveManager.SaveData(_universityManage.GetAllFaculties());
            } catch (Exception ex) {
                _logger.Log($"Error occurred while saving data: {ex.Message}");
                BoardMessage?.Invoke($"Error occurred while saving data: {ex.Message}");
            }
        }

        private void LoadData() {
            try {
                List<Faculty>? faculties = _fileManager.LoadData();
                foreach (var faculty in faculties) {
                    _universityManage.AddFaculty(faculty);
                }
            } catch (Exception ex) {
                _logger.Log($"Error occurred while loading data: {ex.Message}");
                BoardMessage?.Invoke($"Error occurred while loading data: {ex.Message}");
            }
        }
    }
}
