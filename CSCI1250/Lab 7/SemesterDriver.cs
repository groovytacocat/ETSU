/**
*--------------------------------------------------------------------
* File name: SemesterDriver.cs
* Project name: SemesterDriver
* Solution name: Lab 7
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 11/01/2023
* Modified Date: 11/01/2023
* -------------------------------------------------------------------
*/
using MyDLL;

namespace SemesterDriver;

class Program
{
    /// <summary>
    /// Creates a default instance of a Course
    /// Prompts user for Dept name, Course #, and Section #
    /// Course #, Section #, and grades are assigned values using ValidateWithBounds method from CSCI1250/MyDLL
    /// This method performs the standard input validation while also ensuring that input is within a specified upper/lower bounded range.
    /// Prompts user to input 5 grades for the specific course then uses AddGrade() method to add those value to the Course's int List of grades
    /// </summary>
    /// <returns>An instance of Course with user provided data</returns>
    public static Course AddCourse()
    {
        Course collegeClass = new Course();
        int grade;

        Console.WriteLine("\nPlease enter the course information:");
        Console.Write("Department (i.e., CSCI, HIST): ");
        collegeClass.Department = Console.ReadLine();

        collegeClass.CourseNumber = CSCI1250.Validate<int>("Course number (i.e., 1250, 1200): ", "Please enter a valid course number. ", 999, 5999);

        collegeClass.Section = CSCI1250.Validate<int>("Section number (i.e., 001, 901): ", "Please enter a valid section number. ", 1, 999);

        Console.WriteLine($"{collegeClass.Department.ToUpper()} {collegeClass.CourseNumber}-{collegeClass.Section:000} has been added\n");

        Console.WriteLine("Please enter 5 grades for this course: ");
        
        for(int i = 0; i < 5; i++)
        {
            grade = CSCI1250.Validate<int>($"Grade item {i + 1}: ", "Please enter a valid score for ", 0, 100);

            collegeClass.AddGrade(grade);
        }

        return collegeClass;
    }

    static void Main(string[] args)
    {
        List<Course> courses = new List<Course>();

        Console.Write("What is the student name? ");
        string studentName = Console.ReadLine();

        Console.Write("What is the current semester? ");
        string semester = Console.ReadLine();

        //Loop to allow user to enter information for 1 or more courses.
        do
        {
            courses.Add(AddCourse());
        } while (CSCI1250.Repeat("Do you have another course to enter? (Y/N) ", "Pleae enter Y or N. "));

        Console.WriteLine($"\nStudent: {studentName}\nSemester: {semester}");
        Console.WriteLine("============================================");

        //Iterates through all Courses in the list and prints their information
        foreach(Course c in courses)
        {
            c.PrintTranscript();

            Console.WriteLine("----------------------------");
        }
    }
}

