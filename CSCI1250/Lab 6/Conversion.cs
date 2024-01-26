/**
*--------------------------------------------------------------------
* File name: Conversion.cs
* Project name: Convserion
* Solution name: Lab6
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1250
* Creation Date: 10/25/2023
* Modified Date: 10/25/2023
* -------------------------------------------------------------------
*/


using MyDLL;

namespace Conversion;

class Conversion
{
    /// <summary>
    /// Converts meters to kilometers and formats to 3 decimal places
    /// </summary>
    /// <param name="meters"></param>
    static void ShowKilometers(double meters)
    {
        Console.WriteLine($"{meters} meters is {meters * 0.001:F3} kilometers\n");
    }

    /// <summary>
    /// Converts meters to inches and formats to 3 decimal places
    /// </summary>
    /// <param name="meters"></param>
    static void ShowInches(double meters)
    {
        Console.WriteLine($"{meters} meters is {meters * 39.37:F3} inches\n");
    }

    /// <summary>
    /// Converts meters to feet and formats to 3 decimal places
    /// </summary>
    /// <param name="meters"></param>
    static void ShowFeet(double meters)
    {
        Console.WriteLine($"{meters} meters is {meters * 3.281:F3} feet\n");
    }


    /// <summary>
    /// void Method that displays the menu options in the desired formatting
    /// </summary>
    static void Menu()
    {
        Console.WriteLine("\t1. Convert to kilometers\n\t2. Convert to Inches\n\t3. Convert to feet\n\t0. Quit the program");
    }


    static void Main(string[] args)
    {
        int menuChoice;        
        double meters; 

        Console.WriteLine("I will convert a distance into a choice of measurements");

        //Prompts user for distance in meters, ensures meters is a valid double and then further ensures the value is positive using do-while loop
        do
        {
            meters = CSCI1250.Validate<double>("Enter a distance in meters", "\nEnter a valid double value\n");

            if (meters < 0)
            {
                Console.WriteLine("\nEnter a positive value\n");
            }
        } while (meters < 0);


        //Displays menu of options and then gets user input, user input is validated to be an integer
        //Then performs the relevant method calls based on the menu, or exits, or notifies user that the choice is not an acceptable
        do
        {
            Menu();

            menuChoice = CSCI1250.Validate<int>("Please enter your choice", "Please enter a valid integer");

            switch (menuChoice)
            {
                case 1:
                    ShowKilometers(meters);
                    break;
                case 2:
                    ShowInches(meters);
                    break;
                case 3:
                    ShowFeet(meters);
                    break;
                case 0:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine($"{menuChoice} is an invalid menu selection");
                    break;
            }
        } while (menuChoice != 0);
    }
}
