/**
*--------------------------------------------------------------------
* File name: AverageScore.cs
* Project name: Lab3Average
* Solution name: Lab3
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/20/2023
* Modified Date: 09/20/2023
* -------------------------------------------------------------------
*/

namespace Lab3;

/// <summary>
/// Average Score class
/// Prompts user for 3 double-typed score values and computes their average
/// If the average is greater than 95 prints a bonus compliment
/// </summary>
class AverageScore
{
    static void Main(string[] args)
    {
        //Variable Declarations
        double score1, score2, score3, average;

        Console.Write("Enter Score 1: ");           //Prompt user for input
        score1 = double.Parse(Console.ReadLine());  //Read user input

        Console.Write("Enter Score 2: ");           //Prompt user for input 2
        score2 = double.Parse(Console.ReadLine());  //Read user input

        Console.Write("Enter Score 3: ");           //Prompt user for input 3
        score3 = double.Parse(Console.ReadLine());  //Read user input

        average = (score1 + score2 + score3) / 3.0; //Calculate the average of the 3 scores

        Console.WriteLine($"Your average score is: {average:F2}");  //Print the average rounded to 2 decimal places.

        if(average > 95.0)
        {
            Console.WriteLine("That's a great score!");
        }
    }
}

