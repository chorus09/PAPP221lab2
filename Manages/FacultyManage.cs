using lab2.Logs;
using lab2.Models;

namespace lab2.Manages {
    public class FacultyManage {
        private readonly Faculty? _faculty;
        private readonly Logger _logger;

        public FacultyManage(Faculty? faculty, Logger logger) {
            _faculty = faculty;
            _logger = logger;
        }

        public void AddStudent(Student student) {
            _faculty?.Students?.Add(student);
            _logger.Log($"Student added to {_faculty?.Name}: {student.FirstName} {student.LastName}");
        }

        public void RemoveStudentByIndex(int index) {
            if (_faculty?.Students != null && index >= 0 && index < _faculty.Students.Count) {
                _faculty.Students.RemoveAt(index);
                _logger.Log($"Student removed from {_faculty.Name} by index: {index}");
            } else {
                _logger.Log("Invalid index or no students available to remove");
            }
        }

        public void GraduateStudentByIndex(Student student) {
            student.IsGraduated = true;
            _logger.Log($"Student graduated from {_faculty?.Name}: {student.FirstName} {student.LastName}");
        }

        public List<Student>? GetCurrentEnrolledStudents() {
            _logger.Log("Retrieved current enrolled students");
            return _faculty?.Students?.FindAll(s => !s.IsGraduated);
        }

        public List<Student>? GetCurrentGraduatedStudents() {
            _logger.Log("Retrieved currently graduated students");
            return _faculty?.Students?.FindAll(s => s.IsGraduated);
        }

        public bool IsStudentEnrolled(string email) {
            _logger.Log($"Checking if student with email {email} is enrolled");
            return _faculty?.Students?.Exists(s => s.Email == email) ?? false;
        }
    }
}
