using System.Text.RegularExpressions;


namespace IT_step_midterm.Student_Admin
{
    public class StudentManager
    {
        private List<Student> Students;
        private readonly List<char> Grades = new(){ '_','A','B','C','D','F' };
       public StudentManager()
       {
            Students = new List<Student>();
       }
       public StudentManager(List<Student> students)
       {
            this.Students = students;
       }


        public void RunStudentManager()
        {
            char[] options = { '1', '2', '3', '4' };
            char option;
            ConsoleKeyInfo keyInfo = new();

            Console.WriteLine("");
            //offers user stuent managment funtionality
            do
            {
                Console.WriteLine("Choose coresponding number for desired functinality: ");
                Console.WriteLine("1 - Add a student");
                Console.WriteLine("2 - View all the students");
                Console.WriteLine("3 - Find student by roll number");
                Console.Write("4 - Edit student grade");
                Console.Write("5 - delete student by Roll number");

                option = Console.ReadKey(true).KeyChar;
                Console.Write("\n\n");
                if (!options.Contains(option))
                {
                    Console.WriteLine("Invalid option try again");
                    continue;
                }
                RunOperation(option);

                Console.Write("\n\nIf you want to colse menu click tab\nClick any other key to continue: ");
                keyInfo = Console.ReadKey(true);
                Console.Write("\n");
            } while (keyInfo.Key != ConsoleKey.Tab);
        }
        //runs operation that user has chosen
        private void RunOperation(char option)
        {
            switch (option)
            {
                case '1':
                    {
                        string title = EnterName();
                        char grade = EnterGrade();
                        int rollNumber = EnterRollNumber();
                        AddStudent(title, rollNumber, grade);
                        break;
                    }
                case '2':
                    {
                        GetStudents();
                        break;
                    }
                case '3':
                    {
                        int rollNumber = EnterRollNumber();
                        GetStudent(rollNumber);
                        break;
                    }
                case '4':
                    {
                        int rollNumber = EnterRollNumber();
                        char grade = EnterGrade();
                        UpdateStudentGrade(rollNumber,grade);
                        break;
                    }
                default:
                    {
                        int rollNumber = EnterRollNumber();
                        DeleteByRollNumber(rollNumber);
                        break;
                    }

            }
        }
        //adds student to student list
        private Student AddStudent(string name,int roll,char grade)
        {
            Student student = new(name, roll, grade);
            Students.Add(student);
            Console.WriteLine($"new student was added: {student.ToString}");
            return student;
        }
        //prints all the students
        private void GetStudents()
        {
            Console.WriteLine($"View All the students({Students.Count}): ");
            Students.ForEach(student => student.ToString());
        }
        
        //get student by Roll number
        private bool GetStudent(int rollNumber)
        {
            var studnet = Students.Find(student => student.RollNumber == rollNumber);
            if (studnet == null)
            {
                Console.WriteLine("studnet was not found :(");
                return false;
            }
            Console.WriteLine("Found student By this roll number: ");
            Console.WriteLine(studnet?.ToString());
            return true;

        }

        private void UpdateStudentGrade(int rollNumber, char grade)
        {
            var studnet = Students.Find(student => student.RollNumber == rollNumber);
            if (studnet == null)
            {
                Console.WriteLine("studnet was not found :(");
                return;
            }
            studnet.EditGrade(grade);
            Console.WriteLine("Student's grade was updated");
            Console.WriteLine(studnet?.ToString());
            return;
        }

        //deletes student from list by roll number
        private void DeleteByRollNumber(int rollNumber)
        {
            Students.RemoveAll(student => student.RollNumber == rollNumber);
            Console.WriteLine($"student with roll number: {rollNumber} was removed");
        }
        //validates title and author
        private string EnterName()
        {
            string Id;
            Regex IdRegex = new(@"^[\w\s]{2,30}$");

            do
            {
                Console.Write($"Enter valid Name : ");
                Id = Console.ReadLine() ?? "";
            } while (!IdRegex.IsMatch(Id));
            return Id;
        }
        //validates publish year
        private int EnterRollNumber()
        {
            int value = -1;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Enter unique roll number: ");
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    if (value>0 && Students.Find(student=>student.RollNumber== value)==null)
                    {
                        return value;
                    }
                }
                Console.Write("\n");
            }
            return value;
        }

        private char EnterGrade()
        {
            char grade='_';
            bool isValid = false;
            while (!isValid)
            {
                Console.Write($"Enter a valid grade ({string.Join(", ", Grades)})");
                grade = Console.ReadKey().KeyChar;
                
                if (Grades.Contains(grade))
                {
                   return grade;
                }
                Console.Write("\n");
            }
            return grade;
        }
    }


}

