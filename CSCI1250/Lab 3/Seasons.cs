/**
*--------------------------------------------------------------------
* File name: Seasons.cs
* Project name: Lab3Seasons
* Solution name: Lab3
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/20/2023
* Modified Date: 09/20/2023
* -------------------------------------------------------------------
*/
namespace Lab3Seasons;


/// <summary>
/// Class that has 5 (Autumn/Fall) Seasons and their translation to Spanish
/// Prompts user for a season and then prints its translation out to the user
/// If a user provided something that is not a season they are notified
/// Thanks the user for using program
/// </summary>

class Seasons
{
    static void Main(string[] args)
    {
        //Variable declarations and pre-defined spanish translations
        string season;
        string springSpanish = "la primavera";
        string summerSpanish = "el Verano";
        string autumnFallSpanish = "el otono";
        string winterSpanish = "el invierno";

        Console.WriteLine("Enter the name of a season and I'll translate it to Spanish");
        season = Console.ReadLine();

        ///<summary>
        /// Converts input string to all uppercase to avoid any issues with all lower or mixed case of a season
        /// Default case covers the case of invalid input.
        /// </summary>
        switch(season.ToUpper())
        {
            case "SPRING":
                Console.WriteLine($"{season.ToUpper()} in Spanish is {springSpanish}");
                break;
            case "SUMMER":
                Console.WriteLine($"{season.ToUpper()} in Spanish is {summerSpanish}");
                break;
            case "AUTUMN":
                Console.WriteLine($"{season.ToUpper()} in Spanish is {autumnFallSpanish}");
                break;
            case "FALL":
                Console.WriteLine($"{season.ToUpper()} in Spanish is {autumnFallSpanish}");
                break;
            case "WINTER":
                Console.WriteLine($"{season.ToUpper()} in Spanish is {winterSpanish}");
                break;
            default:
                Console.WriteLine($"{season.ToUpper()} is an invalid season.");
                break;
        }

        Console.WriteLine("Thank you for using my program!");
    }
}

