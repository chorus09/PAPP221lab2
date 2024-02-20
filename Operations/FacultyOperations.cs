using lab2.Logs;
using lab2.Manages;
using lab2.Models;

namespace lab2.Operations;
public class FacultyOperations {
    private readonly CommandBoard _commandBoard;
    private readonly FacultyManage _facultyManage;
    private readonly Logger _logger;

    public FacultyOperations(CommandBoard commandBoard, Faculty faculty, Logger logger) {
        _commandBoard = commandBoard;
        _facultyManage = new FacultyManage(faculty, logger);
        _logger = logger;
    }

    public void Execute() {
        while (true) {
            _commandBoard?.BoardMessage?.Invoke("Faculty Operations Menu:\n" +
                                                "1 - Create and assign a student to a faculty.\n" +
                                                "2 - Graduate a student from a faculty.\n" +
                                                "3 - Display current enrolled students.\n" +
                                                "4 - Display graduates.\n" +
                                                "5 - Check if a student belongs to this faculty.\n" +
                                                "ESC - Back to main menu");

            string? choice = _commandBoard?.ReadMessage?.Invoke();
            switch (choice) {
                case "1":
                    ExecuteWithRetry(CreateAndAssignStudent);
                    break;
                case "2":
                    ExecuteWithRetry(GraduateStudent);
                    break;
                case "3":
                    ExecuteWithRetry(DisplayCurrentEnrolledStudents);
                    break;
                case "4":
                    ExecuteWithRetry(DisplayGraduates);
                    break;
                case "5":
                    ExecuteWithRetry(CheckStudentBelonging);
                    break;
                case "ESC":
                    // Exit Faculty Operations and return to main menu
                    return;
                default:
                    _commandBoard?.BoardMessage?.Invoke("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void CreateAndAssignStudent() {
        _commandBoard?.BoardMessage?.Invoke("Enter student details:");

        _commandBoard?.BoardMessage?.Invoke("First Name:");
        string? firstName = _commandBoard?.ReadMessage?.Invoke() ?? "";

        _commandBoard?.BoardMessage?.Invoke("Last Name:");
        string? lastName = _commandBoard?.ReadMessage?.Invoke() ?? "";

        _commandBoard?.BoardMessage?.Invoke("Email:");
        string email = _commandBoard?.ReadMessage?.Invoke() ?? "";

        _commandBoard?.BoardMessage?.Invoke("Enrollment Date (YYYY-MM-DD):");
        DateOnly enrollmentDate = DateOnly.Parse(_commandBoard?.ReadMessage?.Invoke() ?? "");

        _commandBoard?.BoardMessage?.Invoke("Date of Birth (YYYY-MM-DD):");
        DateOnly dateOfBirth = DateOnly.Parse(_commandBoard?.ReadMessage?.Invoke() ?? "");

        Student student = new(firstName, lastName, email, enrollmentDate, dateOfBirth, _logger);
        _facultyManage.AddStudent(student);
        _logger.Log($"Student added: {student.FirstName} {student.LastName}");
        _commandBoard?.BoardMessage?.Invoke("Student added to the faculty successfully.");
    }

    private void GraduateStudent() {
        List<Student>? students = _facultyManage.GetCurrentEnrolledStudents();
        if (students != null && students.Count > 0) {
            _commandBoard?.BoardMessage?.Invoke("Current Enrolled Students:");
            for (int i = 0; i < students.Count; i++) {
                _commandBoard?.BoardMessage?.Invoke($"{i}: {students[i].FirstName} {students[i].LastName} - {students[i].Email}");
            }

            _commandBoard?.BoardMessage?.Invoke("Enter the index of the student to graduate:");
            if (int.TryParse(_commandBoard?.ReadMessage?.Invoke(), out int index)) {
                if (index >= 0 && index < students.Count) {
                    Student studentToGraduate = students[index];
                    _facultyManage.GraduateStudentByIndex(studentToGraduate);
                    _logger.Log($"Student graduated: {studentToGraduate.FirstName} {studentToGraduate.LastName}");
                    _commandBoard?.BoardMessage?.Invoke($"Student {studentToGraduate.FirstName} {studentToGraduate.LastName} has been graduated.");
                } else {
                    _commandBoard?.BoardMessage?.Invoke("Invalid student index.");
                }
            } else {
                _commandBoard?.BoardMessage?.Invoke("Invalid input. Please enter a valid index.");
            }
        } else {
            _commandBoard?.BoardMessage?.Invoke("No currently enrolled students.");
        }
    }

    private void DisplayCurrentEnrolledStudents() {
        List<Student>? students = _facultyManage.GetCurrentEnrolledStudents();
        if (students != null && students.Count > 0) {
            _commandBoard?.BoardMessage?.Invoke("Current Enrolled Students:");
            foreach (var student in students) {
                _commandBoard?.BoardMessage?.Invoke($"{student.FirstName} {student.LastName} - {student.Email}");
            }
        } else {
            _commandBoard?.BoardMessage?.Invoke("No currently enrolled students.");
        }
    }

    private void DisplayGraduates() {
        List<Student>? graduates = _facultyManage.GetCurrentGraduatedStudents();
        if (graduates != null && graduates.Count > 0) {
            _commandBoard?.BoardMessage?.Invoke("Graduated Students:");
            foreach (var graduate in graduates) {
                _commandBoard?.BoardMessage?.Invoke($"{graduate.FirstName} {graduate.LastName} - {graduate.Email}");
            }
        } else {
            _commandBoard?.BoardMessage?.Invoke("No graduates.");
        }
    }

    private void CheckStudentBelonging() {
        _commandBoard?.BoardMessage?.Invoke("Enter the email of the student to check:");
        string? email = _commandBoard?.ReadMessage?.Invoke() ?? "";

        bool belongs = _facultyManage.IsStudentEnrolled(email);
        if (belongs) {
            _commandBoard?.BoardMessage?.Invoke($"Student with email {email} belongs to this faculty.");
        } else {
            _commandBoard?.BoardMessage?.Invoke($"Student with email {email} does not belong to this faculty.");
        }
    }

    private void ExecuteWithRetry(Action operation) {
        while (true) {
            try {
                operation();
                break;
            } catch (Exception ex) {
                _commandBoard?.BoardMessage?.Invoke($"Error: {ex.Message}. Please try again.");
                _logger.Log($"Error: {ex.Message}. Please try again.");
            }
        }
    }
}
