/**
*--------------------------------------------------------------------
* File name: CruiseArray.cs
* Project name: CruiseArray
* Solution name: Lab 5
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 10/4/2023
* Modified Date: 10/9/2023
* -------------------------------------------------------------------
*/

using MyDLL;

namespace CruiseArray;

class CruiseArray
{
    static void Main(string[] args)
    {
        int numCruises;
        int minIndex = 0;
        int maxIndex = 0;
        double averagePrice = 0.0;

        Console.WriteLine("Welcome to the Cruise Price Checker!!\n");


        ///<summary>
        /// Prompts user to input number of cruises they wish to price
        /// Ensures user input is not only a valid integer but also a non-negative integer
        /// Notifies if the user's input is invalid and prompts them for correct values
        ///</summary>
        do
        {
            numCruises = CSCI1250.Validate<int>("How many cruises are you price checking? ", "\nPlease enter a valid integer\n");

            if(numCruises < 0)
            {
                Console.WriteLine("\nPlease enter a non-negative value\n");
            }

        } while (numCruises < 0);

        double[] cruisePrices = new double[numCruises];
        string[] cruiseLocation = new string[numCruises];


        ///<summary>
        /// Prompts user to give a location for a cruise, then prompts user for price of that cruise
        /// If the price is not a valid double or the value input is negative User is notified that their input is invalid and prompts for new input
        /// Calculates a running total of Cruise prices as they are entered
        /// Stores index of the highest price and the lowest price
        /// Compares the current price value to the value at maxIndex if current is greater maxIndex is set to current index
        /// Same as above however if current price is less than value at minIndex minIndex is set to current index
        ///</summary>
        for (int i = 0; i < numCruises; i++)
        {
            Console.Write($"\nEnter the location of Cruise {i + 1}: ");
            cruiseLocation[i] = Console.ReadLine();

            do
            {
                cruisePrices[i] = CSCI1250.Validate<double>($"Enter the price of the Cruise {i + 1}: $", "\nPlease enter a valid double\n");

                if (cruisePrices[i] < 0)
                {
                    Console.WriteLine("\nPlease enter a non-negative value\n");
                }

            } while (cruisePrices[i] < 0);

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

        averagePrice /= numCruises;     //Takes total sum of all cruises and divides by number of cruises

        // Format to print the price as a currency to include $ a ',' if applicable and rounded to 2 decimal places
        Console.WriteLine($"\nMost Expensive Cruise:\n\t{cruiseLocation[maxIndex]}\n\t{cruisePrices[maxIndex]:C}"); 

        Console.WriteLine($"\nLeast Expensive Cruise:\n\t{cruiseLocation[minIndex]}\n\t{cruisePrices[minIndex]:C}");

        Console.WriteLine($"\nAverage Cruise Price: {averagePrice:C}");

    }
}
