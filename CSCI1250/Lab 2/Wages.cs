/**
*--------------------------------------------------------------------
* File name: Wages.cs
* Project name: Lab2Wages
* Solution name: Lab2 
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/13/2023
* Modified Date: 09/13/2023
* -------------------------------------------------------------------
*/
using System;

namespace Lab2;

class Wages
{
    static void Main(string[] args)
    {
        //Variable delcaration
        string name;
        double regularPay, overtimePay, overtimeHours, regularHours, totalWages;

        Console.Write("Enter your name: ");                          //Display prompt to user 
        name = Console.ReadLine();                                   //Read User input and store value in name

        Console.Write("Enter your regular pay: ");                   //Display prompt to user 
        regularPay = double.Parse(Console.ReadLine());               //Read User input and convert to double then store in variable

        Console.Write("Enter your overtime pay: ");                  //Display prompt to user 
        overtimePay = double.Parse(Console.ReadLine());              //Read User input and convert to double then store in variable

        Console.Write("Enter number of regular hours worked: ");     //Display prompt to user 
        regularHours = double.Parse(Console.ReadLine());             //Read User input and convert to double then store in variable

        Console.Write("Enter number of overtime hours worked: ");    //Display prompt to user 
        overtimeHours = double.Parse(Console.ReadLine());            //Read User input and convert to double then store in variable

        //Calculates User's total wage which is the sum of Reg Hours * Reg Pay + Overtime Hours * Overtime Pay
        totalWages = (regularPay * regularHours) + (overtimePay * overtimeHours);

        //Displays formatted output of User's name and total wages
        Console.WriteLine($"Total Wages for {name} is ${totalWages:F2}"); 
    }
}

