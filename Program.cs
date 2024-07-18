using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Grade { get; set; }
}

class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
}

class Class
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public List<int> StudentIds { get; set; } = new List<int>();
}

class SchoolSystem
{
    private List<Student> students = new List<Student>();
    private List<Teacher> teachers = new List<Teacher>();
    private List<Class> classes = new List<Class>();
    private int nextStudentId = 1;
    private int nextTeacherId = 1;
    private int nextClassId = 1;

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AddTeacher();
                    break;
                case "3":
                    AddClass();
                    break;
                case "4":
                    EnrollStudent();
                    break;
                case "5":
                    ViewStudents();
                    break;
                case "6":
                    ViewTeachers();
                    break;
                case "7":
                    ViewClasses();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("=== School Database System ===");
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Add Teacher");
        Console.WriteLine("3. Add Class");
        Console.WriteLine("4. Enroll Student in Class");
        Console.WriteLine("5. View All Students");
        Console.WriteLine("6. View All Teachers");
        Console.WriteLine("7. View All Classes");
        Console.WriteLine("8. Exit");
        Console.Write("Enter your choice (1-8): ");
    }

    private void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();
        Console.Write("Enter student grade: ");
        if (int.TryParse(Console.ReadLine(), out int grade))
        {
            students.Add(new Student { Id = nextStudentId++, Name = name, Grade = grade });
            Console.WriteLine("Student added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid grade. Student not added.");
        }
    }

    private void AddTeacher()
    {
        Console.Write("Enter teacher name: ");
        string name = Console.ReadLine();
        Console.Write("Enter teacher subject: ");
        string subject = Console.ReadLine();
        
        teachers.Add(new Teacher { Id = nextTeacherId++, Name = name, Subject = subject });
        Console.WriteLine("Teacher added successfully.");
    }

    private void AddClass()
    {
        Console.Write("Enter class name: ");
        string name = Console.ReadLine();
        Console.Write("Enter teacher ID: ");
        if (int.TryParse(Console.ReadLine(), out int teacherId) && teachers.Any(t => t.Id == teacherId))
        {
            classes.Add(new Class { Id = nextClassId++, Name = name, TeacherId = teacherId });
            Console.WriteLine("Class added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid teacher ID. Class not added.");
        }
    }

    private void EnrollStudent()
    {
        Console.Write("Enter student ID: ");
        if (int.TryParse(Console.ReadLine(), out int studentId) && students.Any(s => s.Id == studentId))
        {
            Console.Write("Enter class ID: ");
            if (int.TryParse(Console.ReadLine(), out int classId))
            {
                Class classObj = classes.FirstOrDefault(c => c.Id == classId);
                if (classObj != null)
                {
                    if (!classObj.StudentIds.Contains(studentId))
                    {
                        classObj.StudentIds.Add(studentId);
                        Console.WriteLine("Student enrolled successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student is already enrolled in this class.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid class ID. Enrollment failed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid class ID. Enrollment failed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student ID. Enrollment failed.");
        }
    }

    private void ViewStudents()
    {
        Console.WriteLine("Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Grade: {student.Grade}");
        }
    }

    private void ViewTeachers()
    {
        Console.WriteLine("Teachers:");
        foreach (var teacher in teachers)
        {
            Console.WriteLine($"ID: {teacher.Id}, Name: {teacher.Name}, Subject: {teacher.Subject}");
        }
    }

    private void ViewClasses()
    {
        Console.WriteLine("Classes:");
        foreach (var classObj in classes)
        {
            Teacher teacher = teachers.First(t => t.Id == classObj.TeacherId);
            Console.WriteLine($"ID: {classObj.Id}, Name: {classObj.Name}, Teacher: {teacher.Name}");
            Console.WriteLine("Enrolled Students:");
            foreach (var studentId in classObj.StudentIds)
            {
                Student student = students.First(s => s.Id == studentId);
                Console.WriteLine($"  - {student.Name}");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        SchoolSystem schoolSystem = new SchoolSystem();
        schoolSystem.Run();
    }
}