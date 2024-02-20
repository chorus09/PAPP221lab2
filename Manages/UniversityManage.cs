using System;
using lab2.Logs;
using lab2.Models;

namespace lab2.Manages; 
public class UniversityManage {
    private readonly List<Faculty> _faculties;
    private readonly Logger _logger;

    public UniversityManage(Logger logger) {
        _faculties = new List<Faculty>();
        _logger = logger;
    }

    public void AddFaculty(Faculty faculty) {
        _faculties.Add(faculty);
        _logger.Log($"Faculty added: {faculty.Name}");
    }

    public void RemoveFacultyByIndex(int index) {
        if (index >= 0 && index < _faculties.Count) {
            Faculty removedFaculty = _faculties[index];
            _faculties.RemoveAt(index);
            _logger.Log($"Faculty removed: {removedFaculty.Name}");
        } else {
            _logger.Log("Invalid faculty index");
        }
    }

    public Faculty? FindFacultyByEmail(string email) {
        Faculty? faculty = _faculties.FirstOrDefault(f => f.Students != null && f.Students.Any(s => s.Email == email));
        if (faculty != null) {
            _logger.Log($"Faculty found by email: {faculty.Name}");
        } else {
            _logger.Log($"No faculty found by email: {email}");
        }
        return faculty;
    }
    public List<Faculty> GetFacultiesByStudyField(StudyField field) {
        try {
            return _faculties.Where(f => f.StudyField == field).ToList();
        } catch (Exception ex) {
            _logger.Log($"Error occurred while getting faculties by study field: {ex.Message}");
            throw;
        }
    }

    public List<Faculty> GetAllFaculties() {
        _logger.Log("All faculties retrieved");
        return _faculties;
    }
}
