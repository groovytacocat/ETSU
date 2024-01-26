/**
*--------------------------------------------------------------------
* File name: Sum.cs
* Project name: Lab4Sum
* Solution name: Lab 4
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/27/2023
* Modified Date: 09/27/2023
* -------------------------------------------------------------------
*/

namespace Lab4Sum;

class Sum
{
    static void Main(string[] args)
    {
        int num1, num2, sum;
        char repeat;

        ///<summary>
        ///Asks user for 2 numbers and calculates their sum
        ///Asks user if they wish to repeat the program
        ///If the user inputs any value other than 'n' or 'N' program will
        ///repeat execution
        ///</summary>

        do
        {
            Console.WriteLine("\nI will add two numbers together");

            Console.Write("Enter the first number: ");
            num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter the second number: ");
            num2 = int.Parse(Console.ReadLine());

            sum = num1 + num2;

            Console.WriteLine($"The sum of {num1} and {num2} is {sum}.");

            Console.WriteLine("\nWould you like to perform this operation again?");
            repeat = char.ToUpper(char.Parse(Console.ReadLine()));


        } while (repeat != 'N');

        Console.WriteLine("Goodbye!");
    }
}

