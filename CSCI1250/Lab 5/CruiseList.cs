/**
*--------------------------------------------------------------------
* File name: CruiseList.cs
* Project name: CruiseList
* Solution name: Lab 5
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 10/4/2023
* Modified Date: 10/9/2023
* -------------------------------------------------------------------
*/

using System;
using MyDLL;

namespace CruiseList;

class CruiseList
{
    static void Main(string[] args)
    {
        List<string> cruiseLocation = new List<string>();
        List<double> cruisePrices = new List<double>();

        double averagePrice = 0.0;
        int maxIndex = 0;
        int minIndex = 0;
        double price;

        Console.WriteLine("Welcome to the Cruise Price Checker!! (Now with a Lists)");

        ///<summary>
        /// Prompts user to enter a cruise location and a price
        /// If the price is not a valid double or if price is negative notify user that input is invalid and re-prompt for input
        /// After getting values from user asks if user wishes to add another
        /// If user inputs 'n' or "N" ends loading the cruise price/location
        ///</summary>
        do
        {
            Console.Write("\nEnter a Cruise Location: ");
            cruiseLocation.Add(Console.ReadLine());

            do
            {
                price = CSCI1250.Validate<double>("Enter the price of the cruise: $", "\nPlease enter a valid double\n");
                if(price < 0)
                {
                    Console.WriteLine("\nPlease enter a non-negative value\n");
                }
            } while (price < 0);

            cruisePrices.Add(price);

            Console.WriteLine();

        } while (CSCI1250.Repeat("Would you like to add another cruise? ", "\nPlease enter Y to continue or N to stop\n"));

        ///<summary>
        /// Iterates through Cruise Prices and stores running total in Average
        /// Compares current price value to that of the value at the index of maxIndex if it is greater then maxIndex gets the index of current price
        /// Same as above but for the minimum case
        ///</summary>
        for (int i = 0; i < cruisePrices.Count; i++)
        {
            averagePrice += cruisePrices[i];

            if (cruisePrices[i] >= cruisePrices[maxIndex])
            {
                maxIndex = i;
            }
            else if (cruisePrices[i] <= cruisePrices[minIndex])
            {
                minIndex = i;
            }
        }

        averagePrice /= cruisePrices.Count; //Takes running total calculated from above and gets average by dividing by the number of cruises (cast to a double to avoid truncation of the decimal)

        // Format to print the price as a currency to include $ a ',' if applicable and rounded to 2 decimal places
        Console.WriteLine($"\nMost Expensive Cruise:\n\t{cruiseLocation[maxIndex]}\n\t{cruisePrices[maxIndex]:C}");

        Console.WriteLine($"\nLeast Expensive Cruise:\n\t{cruiseLocation[minIndex]}\n\t{cruisePrices[minIndex]:C}");

        Console.WriteLine($"\nAverage Cruise Price: {averagePrice:C}");
    }
}
