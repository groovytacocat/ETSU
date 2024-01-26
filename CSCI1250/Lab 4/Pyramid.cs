/**
*--------------------------------------------------------------------
* File name: Pyramid.cs
* Project name: Lab4Pyramid
* Solution name: Lab 4
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven
* Course-Section: CSCI 1250
* Creation Date: 09/27/2023
* Modified Date: 09/27/2023
* -------------------------------------------------------------------
*/


namespace Lab4Pyramid;

class Pyramid
{
    static void Main(string[] args)
    {
        int pyramidHeight;
        int width;
        int numStar, numSpace;

        Console.WriteLine("How tall do you want your pyramid to be");
        pyramidHeight = int.Parse(Console.ReadLine());

        width = pyramidHeight + 2;


        ///<summary>
        ///Takes the pyramid height and uses for loop to that number of times
        ///The number of stars to be printed to each row is equal to the row number it is
        ///assign numStar i + 1 due to 0 indexing of for loop
        ///prints the specified number of stars and decrements numStars after each *
        ///Once numStars is 0 exits while loop and for loop iterates again setting numStar to the new value of the row
        /// </summary>
        for(int i = 0; i < pyramidHeight; i++)
        {
            numStar = i + 1;

            while(numStar > 0)
            {
                Console.Write("*");
                numStar--;
            }
            Console.Write("\n");
        }
    }
}
