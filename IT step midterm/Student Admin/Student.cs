using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_step_midterm.Student_Admin
{   //student class
    public class Student
    {
        public string Name { get; private set; }
        public int RollNumber { get;private set; }
        public char Grade { get; private set; }

        public Student(string name, int rollNumber, char grade)
        {
            Name = name;
            RollNumber = rollNumber;
            Grade = grade;
        } 
        
        public void EditGrade(char grade)
        {
            Console.WriteLine($"{Name}'s grade is updated from {Grade} to {grade}");
            Grade = grade;
        }
        
        public override string ToString()
        {
            return $"Roll: {RollNumber}, Name: {Name}, Grade: {Grade}";
        }
    }
}
