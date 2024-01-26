/**
*--------------------------------------------------------------------
* File name: RunningTotal.cs
* Project name: Lab4RunningTotal
* Solution name: Lab 4
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/27/2023
* Modified Date: 09/27/2023
* -------------------------------------------------------------------
*/

namespace Lab4RunningTotal;

class RunningTotal
{
    static void Main(string[] args)
    {

        int numPrices;      
        double total = 0.0; //Double to store the running total initialized to 0.0 

        ///<summary>
        /// Prompt user to ask for number of prices they wish to add together
        /// Prompts user to input each price with {i + 1} to account for beginning for-loop iteration at 0
        /// Running total is by definition the sum of the prices as they are entered which would be the sum itself + each new additional price
        /// Price is output to user using interpolated string formatting to round value off to 2 decimal places
        /// </summary>
        Console.Write("I will keep a running total. How many prices would you like to enter: ");
        numPrices = int.Parse(Console.ReadLine());

        for(int i = 0; i < numPrices; i++)
        {
            Console.Write($"Enter price {i + 1}: $");
            total += double.Parse(Console.ReadLine());
        }

        Console.WriteLine($"The total amount is ${total:F2}");

    }
}

