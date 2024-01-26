/**
*--------------------------------------------------------------------
* File name: Audiobooks.cs
* Project name: Lab4Audiobooks
* Solution name: Lab 4
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 09/27/2023
* Modified Date: 09/27/2023
* -------------------------------------------------------------------
*/
using System;
namespace Lab4Audiobook;

/// <summary>
/// Audiobook program with 3 different price choices available
/// Calculates total monthly bill for a user based on the User indicated package chosen
/// and number of books read
/// </summary>

class Audiobooks
{
    //Prices of each package are constant
    const double PACKAGE_A_PRICE = 9.95;
    const double PACKAGE_B_PRICE = 13.95;
    const double PACKAGE_C_PRICE = 19.95;

    //Number of Books included with subscription.
    //No constant for C declared as number of Books is unlimited
    const double INCLUDED_A_BOOKS = 10;
    const double INCLUDED_B_BOOKS = 20;

    //No C for the same reason as above
    const double EXTRA_A_BOOKS = 2.0;
    const double EXTRA_B_BOOKS = 1.0;

    static void Main(string[] args)
    {
        char packChoice, repeatChoice; //New Char variables to indicate package choice and if user wishes to repeat the program
        int numBooks;
        double totalCharges = 0.0;
        bool validInput;               //Bool value for TryParse in event that user input is not a valid form of the data type


        ///<summary>
        /// Prompts user for package choice and uses do-while loops and TryParse to validate that the given input is
        /// of the correct data type (eg. no integers/doubles/strings for package choice or negative numbers for books read)
        /// Uses switch statement to determine the method of price calculation
        /// Asks user if they wish to run the program again and specifies that N must be entered to exit.
        /// Converts user input to uppercase in the event of an 'n' instead of 'N'.
        /// If user input 'n' or 'N' the program will exit otherwise program executes again
        /// </summary>
        do
        {
            do
            {
                Console.Write("Enter the letter of the Audiobooks package you purchased (A, B, or C): ");
                validInput = char.TryParse(Console.ReadLine(), out packChoice);

                packChoice = char.ToUpper(packChoice);

                if (!validInput || !((packChoice == 'A') || (packChoice == 'B') || (packChoice == 'C')))
                {
                    Console.WriteLine("You made an invalid package choice. Please select A, B, or C: ");
                }


            } while (!validInput || !((packChoice == 'A') || (packChoice == 'B') || (packChoice == 'C')));

            do
            {
                Console.Write("Enter the number of audio books you read: ");
                validInput = int.TryParse(Console.ReadLine(), out numBooks);

                if (numBooks < 0 || !validInput)
                {
                    Console.WriteLine("You entered an invalid number of books. Please enter a number greater than or equal to 0");
                }


            } while (!validInput || numBooks < 0);

            ///<summary>
            ///Calculates total monthly price
            ///Update to use switch statement to make cleaner
            ///No default case included for switch as input was validated prior in the program
            ///</summary>

            switch(packChoice)
            {
                case 'A':
                    if(numBooks > INCLUDED_A_BOOKS)
                    {
                        totalCharges = PACKAGE_A_PRICE + (EXTRA_A_BOOKS * (numBooks - INCLUDED_A_BOOKS));
                    }
                    else
                    {
                        totalCharges = PACKAGE_A_PRICE;
                    }
                    break;
                case 'B':
                    if (numBooks > INCLUDED_B_BOOKS)
                    {
                        totalCharges = PACKAGE_B_PRICE + (EXTRA_B_BOOKS * (numBooks - INCLUDED_B_BOOKS));
                    }
                    else
                    {
                        totalCharges = PACKAGE_B_PRICE;
                    }
                    break;
                case 'C':
                    totalCharges = PACKAGE_C_PRICE;
                    break;
            }

            Console.WriteLine($"\nTotal Charges for {numBooks} books for plan {packChoice} is ${totalCharges:F2}");         //Prints the total cost which will be $0.00 if the user supplies an invalid input earlier.


            ///<summary>
            ///Asks user if they would like to repeat the program. If n or N is entered the program exits
            ///All other inputs will cause the program to repeat again
            /// </summary>
            Console.Write("Would you like to continue? If not please enter N: ");
            char.TryParse(Console.ReadLine(), out repeatChoice);

            repeatChoice = char.ToUpper(repeatChoice);

        } while (repeatChoice != 'N');
    }
}
