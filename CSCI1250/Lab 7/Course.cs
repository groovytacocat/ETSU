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
using System;

namespace SemesterDriver
{
    public class Course
    {
        private List<int> grades;
        private string dept;
        private int courseNum;
        private int section;


        //Getter/Setters for each attribute of Course except for the List<int>
        public string Department
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        public int CourseNumber
        {
            get
            {
                return this.courseNum;
            }
            set
            {
                this.courseNum = value;
            }
        }

        public int Section
        {
            get
            {
                return this.section;
            }
            set
            {
                this.section = value;
            }
        }

        //Method to add an integer grade value to the grades List<int>
        public void AddGrade(int grade)
        {
            this.grades.Add(grade);
        }

        /// <summary>
        /// Caculates the average of the grades provided and prints the average to the Console formatted to 1 decimal place.
        /// </summary>
        private void CalcAverage()
        {
            double avg = 0.0;

            foreach(int i in this.grades)
            {
                avg += i;
            }

            avg /= this.grades.Count();

            Console.WriteLine($"Average: {avg:F1}%");
        }

        /// <summary>
        /// Method override to create a formatted string of the Course information
        /// Displays:
        /// Course's department name with an all caps department name
        /// Course's number
        /// Course's Section with leading 0s based on the integer value (eg. section 1 will print 001, section 77 will print 077, section 100 will print 100)
        /// Course's Grades (on one line separated by spaces)
        /// </summary>
        /// <returns>Formatted string of all class attributes</returns>
        public override string ToString()
        {
            string output = "Course: " + this.Department.ToUpper() + " " + this.CourseNumber + "-";

            if(this.Section >= 100)
            {
                output += this.Section;
            }
            else if(this.Section > 9)
            {
                output += "0" + this.Section;
            }
            else
            {
                output += "00" + this.Section;
            }

            output += "\nGrades: ";

            foreach(int g in this.grades)
            {
                output += g + " ";
            }

            return output;
        }

        /// <summary>
        /// Displays the formatted string from the ToString() method and then calls CalcAverage() to display the course's average
        /// This is due to the fact that CalcAverage() is private and void, thus it cannot be called in ToString() as it will perform its print statements prior to the formatted string being returned
        /// This allows for the Console output to match the desired formatting
        /// </summary>
        public void PrintTranscript()
        {
            Console.WriteLine(this.ToString());

            CalcAverage();
        }

        /// <summary>
        /// Paramterized constructor to create an instance of Course with values known prior to instantiating
        /// </summary>
        /// <param name="inputGrades">int List that will contain a student's grades</param>
        /// <param name="inputDept">Name of Course department</param>
        /// <param name="inputCourseNum">Course Number</param>
        /// <param name="inputSection">Section Number of specified course</param>
        public Course(List<int> inputGrades, string inputDept, int inputCourseNum, int inputSection)
        {
            this.grades = inputGrades;
            this.dept = inputDept;
            this.courseNum = inputCourseNum;
            this.section = inputSection;
        }

        /// <summary>
        /// Default constructor that creates an instance of course whose attributes are empty or 0 
        /// </summary>
        public Course() :
            this(new List<int>(), String.Empty, 0, 0)
            {
            }

        /// <summary>
        /// Constructor to copy data from one instance to another
        /// </summary>
        /// <param name="copy">Course object to be copied</param>
        public Course(Course copy)
        {
            this.dept = copy.Department;
            this.courseNum = copy.CourseNumber;
            this.section = copy.Section;

            foreach(int grade in copy.grades)
            {
                this.grades.Add(grade);
            }
        }

    }
}

