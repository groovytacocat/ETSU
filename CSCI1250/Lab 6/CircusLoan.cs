/**
*--------------------------------------------------------------------
* File name: CircusLoan.cs
* Project name: CircusLoan
* Solution name: Lab6
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 10/25/2023
* Modified Date: 10/25/2023
* -------------------------------------------------------------------
*/

using MyDLL;

namespace CircusLoan;


class CircusLoan
{

    /// <summary>
    /// Queries user for a decimal valued salary. Calls method to validate input is a valid integer and notifies otherwise
    /// Performs a further check to ensure that user's valid decimal is also positive (Salary cannot be a negative value)
    /// </summary>
    /// <returns></returns>
    static decimal GetSalary()
    {
        decimal userSalary = 0;

        userSalary = CSCI1250.Validate<decimal>("What is your annual income", "\nPlease enter a valid decimal value\n");

        if(userSalary < 0)
        {
            Console.WriteLine("Please enter a positive decimal value\n");
            userSalary = GetSalary();
        }

        return userSalary;
    }


    /// <summary>
    /// Queries user for an integer valued credit score. Calls method to validate input is a valid integer and notifies otherwise
    /// Compares valid integer input to acceptable bounds as further input validation
    /// </summary>
    /// <returns></returns>
    static int GetCreditScore()
    {
        const int MIN_SCORE = 1;
        const int MAX_SCORE = 10;

        int userScore = 0;

        userScore = CSCI1250.Validate<int>("On a scale of 1-10 (10 being best, 1 being worst). What is your credit score?", "\nPlease enter a valid integer\n");

        if(userScore < MIN_SCORE || userScore > MAX_SCORE)
        {
            Console.WriteLine("\nPlease enter a score between 1 and 10");
            userScore = GetCreditScore();
        }

        return userScore;

    }

    /// <summary>
    /// Void method to print that the user qualifies 
    /// </summary>
    static void Qualify()
    {
        Console.WriteLine("Congratulations!! You qualify for the circus loan");
    }

    /// <summary>
    /// Same as above but informs they are not eligible
    /// </summary>
    static void NoQualify()
    {
        Console.WriteLine("I'm sorry. You do not qualify for the circus loan.");
    }


    /// <summary>
    /// Method that compares salary and credit score to the acceptable conditions
    /// If user is eligible Qualify() is called NoQualify() if not
    /// </summary>
    /// <param name="income"></param>
    /// <param name="rating"></param>
    static void Assess(decimal income, int rating)
    {
        const int MIN_CREDIT_SCORE = 7;
        const decimal MIN_SALARY = 20000;

        if (income >= MIN_SALARY && rating >= MIN_CREDIT_SCORE)
        {
            Qualify();
        }
        else
        {
            NoQualify();
        }
    }

    static void Main(string[] args)
    {
        /*
         * Performs the above defined methods and then queries user if they wish to go again
         * If user does not enter one of y, n, Y, N Repeat() will prompt them to enter one of the listed values
         * If user enters y/Y Repeat returns true and repeats else ends and says goodbye.
         */
        do
        {
            decimal salary = GetSalary();
            int creditScore = GetCreditScore();

            Assess(salary, creditScore);

            Console.WriteLine();
        } while (CSCI1250.Repeat("Would you like to enter a new request?", "\nPlease enter Y to repeat or N to stop\n"));


        Console.WriteLine("Goodbye!");
    }
}

