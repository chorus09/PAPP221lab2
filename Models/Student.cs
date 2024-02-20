using lab2.Logs;
using System.ComponentModel;

namespace lab2.Models;
public class Student : INotifyPropertyChanged {

    private string _firstName;
    private string _lastName;
    private string _email;
    private bool _isGraduated;
    private DateOnly _enrollmentDate;
    private DateOnly _dateOfBirth;
    private readonly Logger _logger;

    public string FirstName {
        get => _firstName;
        set {
            if (_firstName == value) return;
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }
    public string LastName {
        get => _lastName;
        set {
            if (_lastName == value) return;
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
    public string Email {
        get => _email;
        set {
            if (_email == value) return;
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    public bool IsGraduated {
        get => _isGraduated;
        set {
            if (_isGraduated == value) return;
            _isGraduated = value;
            OnPropertyChanged(nameof(IsGraduated));
        }
    }
    public DateOnly EnrollmentDate {
        get => _enrollmentDate;
        set {
            if (_enrollmentDate == value) return;
            _enrollmentDate = value;
            OnPropertyChanged(nameof(EnrollmentDate));
        }
    }
    public DateOnly DateOfBirth {
        get => _dateOfBirth;
        set {
            if (_dateOfBirth == value) return;
            _dateOfBirth = value;
            OnPropertyChanged(nameof(DateOfBirth));
        }
    }

    public Student(string firstName, string lastName, string email, DateOnly enrollmentDate, DateOnly dateOfBirth, Logger logger) {
        _logger = logger;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _enrollmentDate = enrollmentDate;
        _dateOfBirth = dateOfBirth;
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        _logger.Log($"Property {propertyName} changed for student {_firstName} {_lastName} (Email: {_email}).");
    }
}
