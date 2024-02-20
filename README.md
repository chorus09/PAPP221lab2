FileManager: This class is responsible for loading and saving data to a file. I added exception handling and logging of all actions.

SaveManager: This class also handles saving data to a file. It also includes exception handling and logging.

FacultyManage: This class manages operations on faculties, such as adding students, removing students, marking students as graduates, etc.

UniversityManage: This class manages operations related to the university, such as adding faculties, searching faculties by students' email, etc.

Faculty: This class represents a faculty in the university. I added logging of property changes to the faculty.

Student: This class represents a student in the university. I also added logging of property changes to the student.

FacultyOperations: This class contains operations that can be performed on a faculty, such as creating and assigning a student, graduating a student, etc.

GeneralOperations: This class contains general operations that can be performed in the university, such as creating a new faculty, searching for a faculty by a student's email, etc.

CommandBoard: This class represents the command board of the program. It contains the main Execute method that starts the main program loop. It also implements methods for performing operations on faculties and the university, as well as saving and loading data using SaveManager and FileManager. Additionally, user action logging is added.

Logger: This class adds the ability to log program actions. Logs are saved in a text file for later analysis.
