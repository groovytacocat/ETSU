

using static System.Formats.Asn1.AsnWriter;
/**
*--------------------------------------------------------------------
* File name: Project1_v2.cs
* Project name: Project1.2
* Solution name: Project1
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 10/2/2023
* Modified Date: 10/3/2023
* -------------------------------------------------------------------
*/

namespace Project1;

class Project1_v2
{
    const int MAX_QUESTIONS = 10;   //Max questions per Project Guidelines
    public static bool validInput;  //Delcaring a boolean class variable to be used by member functions

    ///<summary>
    /// string array that contains 20 questions and their answer choices
    ///</summary>
    static string[] questionBank = new string[]
    {
        //q1
        "Which of the following CAN be a variable type?" +
        "\nA. int" +
        "\nB. string" +
        "\nC. A Class name" +
        "\nD. All of the above", //CORRECT
        //q2
        "A for-each loop provides definite iteration: the count that the loop body will be executed depends on the size of the collection.  " +
        "An example of indefinite iteration is a(n)" +
        "\nA. if statement" +
        "\nB. package" +
        "\nC. collection" +
        "\nD. while loop", //CORRECT
        //q3
        "Testing should be _____________, meaning that a specific approach is used to help localize the problem." +
        "\nA. Top-Down" +
        "\nB. Systematic" + //CORRECT
        "\nC. Bottom-up" +
        "\nD. Progressive",
        //q4
        "Tests that exercise only individual parts is/are called ___________." +
        "\nA. Case Test" +
        "\nB. Function Analysis" +
        "\nC. Group Test" +
        "\nD. Unit Test", //CORRECT
        //q5
        "What is the correct way to assign a value to the first element of an array in C#?" +
        "\nA. numbers[-1] = 10;" +
        "\nB. numbers[1] = 10;" +
        "\nC. numbers[2] = 10;" +
        "\nD. numbers[0] = 10;", //CORRECT
        //q6
        "Which of the following correctly initializes an array with values during declaration?" +
        "\nA. char[] letters = new char();" +
        "\nB. string names[] = new string[];" +
        "\nC. double prices = {1.99, 3.49, 2.79}" + //CORRECT
        "\nD. int[] numbers = new int[5]",
        //q7
        "What is an array in programming?" +
        "\nA. An object that can store a group of values, all of the same type" + //CORRECT
        "\nB. A data structure used for sorting data" +
        "\nC. A container that can store multiple values of different data types" +
        "\nD. A variable that can hold only one value at a time",
        //q8
        "A(n) ____ stores data used in a program." +
        "\nA. Boolean" +
        "\nB. Entity" +
        "\nC. Variable" + //CORRECT
        "\nD. Data type",
        //q9
        "In the following, what value is stored in x? int x = 5 / 2;" +
        "\nA. 3" +
        "\nB. 2" + //CORRECT
        "\nC. 2.5" +
        "\nD. Nothing, it causes an error",
        //q10
        "The if-else statement in C# executes which block of code when the boolean expression is false?" +
        "\nA. The \"if\" block" +
        "\nB. The \"else\" block" + //CORRECT
        "\nC. Both \"if\" and \"else\" blocks" +
        "\nD. None of the above",
        //q11
        "Which of the following is the correct syntax for writing a for-each loop to print out all last names from an addressbook collection?" +
        "\nA. foreach(string lastname in addressbook) { Console.WriteLine(lastname); }" +
        "\nB. foreach(string : lastname <addressbook>) { Console.WriteLine(lastname); }" +
        "\nC. foreach(<string> lastname in addressbook) { Console.WriteLine(lastname); }" +
        "\nD. for(string addressbook : lastname) { Console.WriteLine(lastname); }",
        //q12
        "When a variable stores an object, ______________." +
        "\nA. The program can't compile" +
        "\nB. An object reference is stored in the variable" + //CORRECT
        "\nC. The variable is no longer considered private" +
        "\nD. The object is stored in the variable directly ",
        //q13
        "What would the result be the result of the following string?\n 4 + 8 + \"truck\"" +
        "\nA. 48+truck" +
        "\nB. 48truck" +
        "\nC. 12truck" + //CORRECT
        "\nD. You'd get an error since you can't add together strings and numbers",
        //q14
        "Null is a C# keyword that means ____________." +
        "\nA. The field is storing a 0" +
        "\nB. The field has not been explicitly initialized" + //CORRECT
        "\nC. The field is storing a reference but no value" +
        "\nD. The field is storing an empty string",
        //q15
        "What does a boolean expression in C# evaluate to?" +
        "\nA. A string" +
        "\nB. A floating-point number" +
        "\nC. True/False" + //CORRECT
        "\nD. An Integer",
        //q16
        "When is it best to use the && operator to check if a numeric value is within a specific range in C#?" +
        "\nA. When determining if the number is inside the range" + //CORRECT
        "\nB. When determining if the number is outside the range" +
        "\nC. Both A and B" +
        "\nD. Neither A nor B",
        //q17
        "Each program can have _____." +
        "\nA. one Main method per Class" +
        "\nB. one Main method per Class if it's static" +
        "\nC. one Main method per file" +
        "\nD. one Main method per program", //CORRECT
        //q18
        "Before a program executes, the program must be ___________ so the machine can understand it." +
        "\nA. Compiled" + //CORRECT
        "\nB. Instantiated" +
        "\nC. Aggregated" +
        "\nD. Interpreted",
        //q19
        "What is the output to the below statement?\nConsole.Write(\"Yo quiero \");\nConsole.Write(\"Taco Bell\");" +
        "\nA. Nothing, it will display an error message" +
        "\nB. Yo quiero Taco Bell" + //CORRECT
        "\nC. Yo quiero \n   Taco Bell" + //3 spaces added for formatting purposes
        "\nD. Yo quieroTacoBell",
        //q20
        "What is the purpose of nesting if statements in C#?" +
        "\nA. To create loops" +
        "\nB. To test a series of decisions" + //CORRECT
        "\nC. To define functions" +
        "\nD. To make the code more efficient"
    };//END QuestionBank

    ///<summary>
    /// Char array that contains the answers to the questions in the QuestionBank array
    ///</summary>
    static char[] answerBank = new char[]
    {
        'D',
        'D',
        'B',
        'D',
        'D',
        'C',
        'A',
        'C',
        'B',
        'B',
        'A',
        'B',
        'C',
        'B',
        'C',
        'A',
        'D',
        'A',
        'B',
        'B'
    };

    ///<summary>
    /// Method that takes a List of Integers and populates it with random unique integers between 0-19 to create
    /// a quiz for the user that will display the questions in a different order each time
    ///</summary>
    public static List<int> LoadQuestions(List<int> userQuestions)
    {
        Random rNum = new Random();

        int questionNum;

        ///<summary>
        /// Generates a random number between 0 and 9
        /// If the random generated integer is not in the List it will be added
        /// If the random integer was already in the List no value is added and the loop iterates again to generate another random integer
        /// This will ensure that the List contains only unique random integers between 0 and 9
        /// Ensures the user will answer different questions each quiz and not potentially multiples of the same question
        ///</summary>
        while (userQuestions.Count < MAX_QUESTIONS)
        {
            questionNum = rNum.Next(0, 20);

            if (!userQuestions.Contains(questionNum))
            {
                userQuestions.Add(questionNum);
            }
        }

        return userQuestions;
    }

    /// <summary>
    /// Takes a char List and populates it with the corresponding correct answers of the integer List for user questions
    /// </summary>
    public static List<char> LoadAnswers(List<char> userAnswers, List<int> userQuestions)
    {
        for (int i = 0; i < userQuestions.Count; i++)
        {
            userAnswers.Add(answerBank[userQuestions[i]]);
        }

        return userAnswers;
    }

    ///<summary>
    /// Prompts user if they wish to continue to next question or stop where they are
    /// If user opts to next question returns boolean value true
    /// If user opts to finish returns boolean value of false
    ///</summary>
    public static bool ContinueQuiz()
    {
        char nextQuestion;

        do
        {
            Console.Write("\nWould you like to go to the next question (Y) or stop and show final score (N)? ");
            validInput = char.TryParse(Console.ReadLine(), out nextQuestion);

            nextQuestion = char.ToUpper(nextQuestion);

            if (!(nextQuestion == 'Y' || nextQuestion == 'N'))
            {
                validInput = false;
                Console.WriteLine("\nYou made an invalid choice please choose 'Y' or 'N'");
            }
        } while (!validInput);

        return nextQuestion == 'N' ? false : true;
    }

    ///<summary>
    /// Prompts user if they would like to repeat the Quiz program again.
    /// Uses input validation to ensure the user not only enters a valid char, but also that the input is either a Y for Yes or N for no
    /// If user wishes to start again returns Boolean value of true
    /// If user wishes to stop returns Boolean value of false
    ///</summary>
    public static bool RepeatProgram()
    {
        char repeatChoice;

        do
        {
            Console.Write("Would you like to do this again? Y/N: ");
            validInput = char.TryParse(Console.ReadLine(), out repeatChoice);

            repeatChoice = char.ToUpper(repeatChoice);

            if (!(repeatChoice == 'Y' || repeatChoice == 'N'))
            {
                validInput = false;
                Console.WriteLine("\nYou made an invalid choice please choose 'Y' or 'N'\n");
            }
        } while (!validInput);

        return repeatChoice == 'Y' ? true : false;
    }

    ///<summary>
    /// Displays question and answer choices to user and prompts user for answer
    /// Validates user input to ensure they not only enter valid char but that the char is one of A, B, C, or D (uses char.ToUpper method to avoid inconsistencies of case with user input)
    /// Compares user input with the char in the answer bank that matches the corresponding question from the question bank
    /// If User is correct point is incremented and function returns 1 to indicate question answered correctly.
    /// If User is incorrect the user is notified and correct answer is displayed, function returns 0 for score calculations
    ///</summary>
    public static int QuizUser(List<int> userQuestions, List<char> quizAnswers, int questionNum)
    {
        char ansChoice;

        do
        {
            Console.WriteLine($"\nQuestion #{questionNum + 1} is:\n{questionBank[userQuestions[questionNum]]}\n");
            Console.Write("Enter your answer choice (A, B, C, D): ");
            validInput = char.TryParse(Console.ReadLine(), out ansChoice);

            ansChoice = char.ToUpper(ansChoice);

            if (!(ansChoice == 'A' || ansChoice == 'B' || ansChoice == 'C' || ansChoice == 'D'))
            {
                validInput = false;
                Console.WriteLine("\nYou made an invalid choice. Please select A, B, C, or D");
            }
        } while (!validInput);

        if (ansChoice.Equals(quizAnswers[questionNum]))
        {
            Console.WriteLine("\nCorrect!");
            return 1;
        }
        else
        {
            Console.WriteLine($"\nIncorrect! You entered {ansChoice}. The Correct answer was: {quizAnswers[questionNum]}");
            return 0;
        }
    }

    ///<summary>
    /// Displays questions 1-10 from the previously generated integer List quiz
    /// Keeps running total of score and questions attempted as user progresses through quiz
    /// Prompts user to choose if they wish to answer more questions or stop 
    ///</summary>
    public static void RunQuiz()
    {
        do
        {
            List<int> quiz = new List<int>();
            List<char> answers = new List<char>();
            int points;
            int attempted;
            double uGrade;

            points = 0;
            attempted = 0;         //Initialize score and questions answered to 0 for each go through

            LoadQuestions(quiz);            //Initializes quiz bank with questions
            LoadAnswers(answers, quiz);     //Populates answer bank with corresponding quiz answers 

            for (int j = 0; j < quiz.Count; j++)
            {
                points += QuizUser(quiz, answers, j);

                attempted++;

                if (j != (quiz.Count - 1)) //Will only display next question as long as program isn't last question to be shown
                {
                    if (!ContinueQuiz()) //Negating ContinueQuiz will iterate again if user wishes to continue or break if user wishes to stop eg If Not Continue then stop.
                    {
                        break;
                    }
                }
            }

            uGrade = (double)points / (double)attempted;

            Console.WriteLine($"\nYou scored a {points} / {attempted} or {uGrade * 100:F0} / 100\n"); //Prints user's score out of questions answered and displays the double percentile grade rounded to 0 decimal places
        } while (RepeatProgram());
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to CSCI 1250 Project 1!");

        RunQuiz();

        Console.WriteLine("\nThank you for using my quiz tool. Goodbye!");
    }
}
