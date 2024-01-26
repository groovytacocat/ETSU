/**
*--------------------------------------------------------------------
* File name: Madlibs.cs
* Project name: Lab2Madlibs
* Solution name: Lab2 
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/13/2023
* Modified Date: 09/13/2023
* -------------------------------------------------------------------
*/
namespace Lab2;

class Madlibs
{
    static void Main(string[] args)
    {
        //Variable declaration for string inputs and the one int for age of user
        string userName, cityName, countryName, profession, animalType, petName;
        int userAge;

        Console.Write("Enter your name: ");                     //Prompt user for input             
        userName = Console.ReadLine();                          //Read input from user and store in variable

        Console.Write("Enter your age: ");                      //Prompt user for input
        userAge = int.Parse(Console.ReadLine());                //Read input from user and store in variable after converting to appropriate data type

        Console.Write("Enter the name of a City: ");            //Prompt user for input
        cityName = Console.ReadLine();                          //Read input from user and store in variable

        Console.Write("Enter a country name: ");                //Prompt user for input
        countryName = Console.ReadLine();                       //Read input from user and store in variable

        Console.Write("Enter the name of a profession: ");      //Prompt user for input
        profession = Console.ReadLine();                        //Read input from user and store in variable

        Console.Write("Enter a type of animal: ");              //Prompt user for input
        animalType = Console.ReadLine();                        //Read input from user and store in variable

        Console.Write("Enter the name of the pet: ");           //Prompt user for input
        petName = Console.ReadLine();                           //Read input from user and store in variable


        ///<summary>
        /// Print output using User provided input for madlib (for readability WriteLine split across 2 lines
        /// </summary>
        Console.WriteLine($"There once was a person name {userName} who lived in {cityName}. At the age of {userAge}, {userName} decided to move to {countryName} and " +
            $"become a {profession}. Then, {userName} adopted a(n) {animalType} named {petName}. They both lived happily ever after!");
    }
}

