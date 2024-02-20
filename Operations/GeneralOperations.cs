using System;
using lab2.Logs;
using lab2.Manages;
using lab2.Models;

namespace lab2.Operations {
    public class GeneralOperations {
        private readonly CommandBoard _commandBoard;
        private readonly UniversityManage _universityManage;
        private readonly Logger _logger;

        public GeneralOperations(CommandBoard commandBoard, UniversityManage manage, Logger logger) {
            _commandBoard = commandBoard;
            _universityManage = manage;
            _logger = logger;
        }

        public void Execute() {
            while (true) {
                _commandBoard?.BoardMessage?.Invoke("General Operations Menu:\n" +
                                                    "1 - Create a new faculty.\n" +
                                                    "2 - Search what faculty a student belongs to by email.\n" +
                                                    "3 - Display University faculties.\n" +
                                                    "4 - Display all faculties belonging to a field.\n" +
                                                    "ESC - Back to main menu");

                string? choice = _commandBoard?.ReadMessage?.Invoke();
                switch (choice) {
                    case "1":
                        ExecuteWithRetry(CreateNewFaculty);
                        break;
                    case "2":
                        ExecuteWithRetry(SearchFacultyByEmail);
                        break;
                    case "3":
                        ExecuteWithRetry(DisplayUniversityFaculties);
                        break;
                    case "4":
                        ExecuteWithRetry(DisplayFacultiesByField);
                        break;
                    case "ESC":
                        // Exit General Operations and return to main menu
                        return;
                    default:
                        _commandBoard?.BoardMessage?.Invoke("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateNewFaculty() {
            _commandBoard?.BoardMessage?.Invoke("Enter the name of the new faculty:");
            string? name = _commandBoard?.ReadMessage?.Invoke();

            _commandBoard?.BoardMessage?.Invoke("Enter the abbreviation of the new faculty:");
            string? abbreviation = _commandBoard?.ReadMessage?.Invoke();

            _commandBoard?.BoardMessage?.Invoke("Enter the study field of the new faculty (0 - MECHANICAL_ENGINEERING, 1 - SOFTWARE_ENGINEERING, 2 - FOOD_TECHNOLOGY, 3 - URBANISM_ARCHITECTURE, 4 - VETERINARY_MEDICINE):");
            string? fieldInput = _commandBoard?.ReadMessage?.Invoke();
            if (Enum.TryParse(fieldInput, out StudyField field)) {
                var newFaculty = new Faculty(name, abbreviation, field, _logger);
                _universityManage.AddFaculty(newFaculty);
                _commandBoard?.BoardMessage?.Invoke("New faculty created successfully.");
            } else {
                _commandBoard?.BoardMessage?.Invoke("Invalid field input. Please try again.");
            }
        }

        private void SearchFacultyByEmail() {
            _commandBoard.BoardMessage?.Invoke("Enter the email of the student:");
            string? email = _commandBoard.ReadMessage?.Invoke();

            Faculty? faculty = _universityManage.FindFacultyByEmail(email);
            if (faculty != null) {
                _commandBoard.BoardMessage?.Invoke($"The student with email {email} belongs to the faculty: {faculty.Name}");
            } else {
                _commandBoard.BoardMessage?.Invoke($"No student found with email {email}.");
            }
        }

        private void DisplayUniversityFaculties() {
            var faculties = _universityManage.GetAllFaculties();
            if (faculties.Count > 0) {
                _commandBoard.BoardMessage?.Invoke("List of University Faculties:");
                foreach (var faculty in faculties) {
                    _commandBoard.BoardMessage?.Invoke(faculty.ToString());
                }
            } else {
                _commandBoard?.BoardMessage?.Invoke("No faculties found in the university.");
            }
        }

        private void DisplayFacultiesByField() {
            _commandBoard.BoardMessage?.Invoke("Enter the study field (0 - MECHANICAL_ENGINEERING, 1 - SOFTWARE_ENGINEERING, 2 - FOOD_TECHNOLOGY, 3 - URBANISM_ARCHITECTURE, 4 - VETERINARY_MEDICINE):");
            string? fieldInput = _commandBoard.ReadMessage?.Invoke();
            if (Enum.TryParse(fieldInput, out StudyField field)) {
                List<Faculty> faculties = _universityManage.GetFacultiesByStudyField(field);
                if (faculties.Count > 0) {
                    _commandBoard.BoardMessage?.Invoke($"List of Faculties in {field}:");
                    foreach (var faculty in faculties) {
                        _commandBoard.BoardMessage?.Invoke(faculty.ToString());
                    }
                } else {
                    _commandBoard.BoardMessage?.Invoke($"No faculties found in the field {field}.");
                }
                _logger.Log($"Displayed faculties by field: {field}");
            } else {
                _commandBoard?.BoardMessage?.Invoke("Invalid field input. Please try again.");
                _logger.Log($"Invalid field input: {fieldInput}");
            }
        }

        private void ExecuteWithRetry(Action operation) {
            while (true) {
                try {
                    operation();
                    break;
                } catch (Exception ex) {
                    _commandBoard.BoardMessage?.Invoke($"Error: {ex.Message}. Please try again.");
                    _logger.Log($"Error: {ex.Message}. Please try again.");
                }
            }
        }
    }
}
