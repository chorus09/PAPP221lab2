using System.ComponentModel;
using lab2.Logs;

namespace lab2.Models; 
public enum StudyField {
    MECHANICAL_ENGINEERING,
    SOFTWARE_ENGINEERING,
    FOOD_TECHNOLOGY,
    URBANISM_ARCHITECTURE,
    VETERINARY_MEDICINE
}

public class Faculty : INotifyPropertyChanged {
    private string _name;
    private string _abbreviation;
    private List<Student>? _students;
    private StudyField _studyField;
    private readonly Logger _logger;

    public Faculty(string name, string abbreviation, StudyField studyField, Logger logger) {
        _logger = logger;
        _name = name;
        _abbreviation = abbreviation;
        Students = new List<Student>();
        _studyField = studyField;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Name {
        get => _name;
        set {
            if (_name != value) {
                _name = value;
                OnPropertyChanged(nameof(Name));
                _logger.Log($"Faculty name updated: {value}");
            }
        }
    }

    public string Abbreviation {
        get => _abbreviation;
        set {
            if (_abbreviation != value) {
                _abbreviation = value;
                OnPropertyChanged(nameof(Abbreviation));
                _logger.Log($"Faculty abbreviation updated: {value}");
            }
        }
    }

    public List<Student>? Students {
        get => _students;
        set {
            if (_students != value) {
                _students = value;
                OnPropertyChanged(nameof(Students));
                _logger.Log("Faculty students list updated");
            }
        }
    }

    public StudyField StudyField {
        get => _studyField;
        set {
            if (_studyField != value) {
                _studyField = value;
                OnPropertyChanged(nameof(StudyField));
                _logger.Log($"Faculty study field updated: {value}");
            }
        }
    }

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
