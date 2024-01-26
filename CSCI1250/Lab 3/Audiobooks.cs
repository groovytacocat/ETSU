/**
*--------------------------------------------------------------------
* File name: Audiobooks.cs
* Project name: Lab3Audiobooks
* Solution name: Lab3
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/20/2023
* Modified Date: 09/20/2023
* -------------------------------------------------------------------
*/
using System;
namespace Lab3Audiobook;

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
        char choice;
        int numBooks;
        double totalCharges = 0.0;

        Console.Write("Enter the letter of the Audiobooks package you purchased (A, B, or C): ");
        choice = char.ToUpper(char.Parse(Console.ReadLine()));

        Console.Write("Enter the number of audio books you read: ");
        numBooks = int.Parse(Console.ReadLine());

        ///<summary>
        ///Calculates total bill based on package chosen and number of Books read
        ///for case C since books included is unlimited the price for any number of books read will be the package price
        ///If User read more books than included they must pay additional fee based on the extra book rate * # of extra books
        ///If the user inputs an invalid package choice or negative number of books read an error message will be printed
        ///</summary>

        if ((choice == 'C') && (numBooks >= 0))
        {
            totalCharges = PACKAGE_C_PRICE;
        }
        else if ((choice == 'B') && (numBooks > INCLUDED_B_BOOKS))
        {
            totalCharges = PACKAGE_B_PRICE + (EXTRA_B_BOOKS * (numBooks - INCLUDED_B_BOOKS));
        }
        else if ((choice == 'B') && (numBooks <= INCLUDED_B_BOOKS) && (numBooks >= 0))
        {
            totalCharges = PACKAGE_B_PRICE;
        }
        else if ((choice == 'A') && (numBooks > INCLUDED_A_BOOKS))
        {
            totalCharges = PACKAGE_A_PRICE + (EXTRA_A_BOOKS * (numBooks - INCLUDED_A_BOOKS));
        }
        else if ((choice == 'A') && (numBooks <= INCLUDED_A_BOOKS) && (numBooks >= 0))
        {
            totalCharges = PACKAGE_A_PRICE;
        }
        else
        {
            Console.WriteLine("You made an invalid choice for Audiobook package or entered a negative value for number of books read.\nPlease try again");
        }

        Console.WriteLine($"\nTotal Charges for {numBooks} books for plan {choice} is ${totalCharges:F2}");         //Prints the total cost which will be $0.00 if the user supplies an invalid input earlier.
    }
}
