/**
*--------------------------------------------------------------------
* File name: Friend.cs
* Project name: Lab2Friend
* Solution name: Lab2 
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/13/2023
* Modified Date: 09/13/2023
* -------------------------------------------------------------------
* 
*
*/
namespace Lab2;

class Friends
{
    static void Main(string[] args)
    {
        //Variable Declarations
        string userName, friendName;
        int userAge, friendAge;

        Console.Write("Enter your name: ");                                                     //Prompt user for name of user
        userName = Console.ReadLine();                                                          //Read user input for name of user

        Console.Write("Enter your age: ");                                                      //Prompt user for age
        userAge = int.Parse(Console.ReadLine());                                                //Read user input and convert to int for age of user

        Console.WriteLine($"Your name is {userName} and you are {userAge} years old\n");        //Display User name and age

        Console.Write("Enter your friend's name: ");                                            //Prompt User for name of friend
        friendName = Console.ReadLine();                                                        //Ready user input for Friend Name

        Console.Write("Enter your friend's age: ");                                             //Prompt User for age of friend
        friendAge = int.Parse(Console.ReadLine());                                              //Read user input and convert to int for age of friend

        Console.WriteLine($"Your friend's name is {friendName} and is {friendAge} years old");  //Displays friend name and age

        //Displays number of years (absolute value of difference) between user and friend 
        Console.WriteLine($"There are {Math.Abs(userAge - friendAge)} years between you");
    }
}

