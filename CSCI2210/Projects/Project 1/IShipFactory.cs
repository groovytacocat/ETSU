using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal interface IShipFactory
    {
        /// <summary>
        /// Method to verify a given string input is a valid <see cref="Ship"/> object.
        /// Uses RegEx to ensure that string fits the pattern of SHIP TYPE, LENGTH (Numerical Value), v/h (for Direction), X COORD (Numerical Value), Y COORD (Numerical Value). If this fails no further validation is attempted and an exception is thrown
        /// If RegEx is a positive match it then splits the given comma-delimited <see cref="string"/> to an array, trims whitepace then converts the Length, and Positional Coordinate values to <see cref="int"/>s, and the direction character to the appropriate <see cref="DirectionType"/> enum 
        /// Checks to ensure the Length value is within the accepted limits as well as checking to see if in combination with the <see cref="DirectionType"/> to ensure the <see cref="Ship"/> does not extend passed the game board boundaries
        /// </summary>
        /// <param name="description"><see cref="string"/> containg a comma-delimited list of <see cref="Ship"/> values</param>
        /// <returns>Returns True if and only if all criteria for a valid <see cref="Ship"/> is met</returns>
        /// <exception cref="Exception">Throws exception if positional coordinates provided would place <see cref="Ship"/> off grid or if provided <see cref="Ship"/> length is invalid</exception>
        public bool VerifyShipString(string description);

        /// <summary>
        /// Takes a line of input and calls <see cref="VerifyShipString(string)"/> to ensure validity of <see cref="Ship"/> object then constructs an instance of that type
        /// Once validated, the input line is split by the comma delimiter creates a <see cref="Coord2D"/> out of the trimmed and parsed coordinates from the input, 
        /// converts the 'v' or 'h' character into the corresponding <see cref="DirectionType"/> then uses a switch statement to determine which <see cref="Ship"/> subtype to construct with the given position and direction
        /// </summary>
        /// <param name="description"><see cref="string"/> containg a comma-delimited list of <see cref="Ship"/> values</param>
        /// <returns>Instance of a <see cref="Ship"/> object as specified by input</returns>
        /// <exception cref="Exception">If an invalid <see cref="Ship"/> type is given in the input an exception is thrown</exception>
        public Ship ParseShipString(string description);

        /// <summary>
        /// Method to take a filepath from either command-line or console, then read contents of file line by line.
        /// If line starts with '#' it is ignored, otherwise will call <see cref="ParseShipFile(string)"/> to attempt to construct a <see cref="Ship"/> object
        /// If valid the <see cref="Ship"/> object is added to a <see cref="List{T}"/> of type <see cref="Ship"/> and then converted to an <see cref="Array"/> and returned.
        /// </summary>
        /// <param name="filePath"><see cref="string"/> representing the file path of the game board</param>
        /// <returns><see cref="Array"/> of <see cref="Ship"/> objects </returns>
        /// <exception cref="Exception">If <see cref="ParseShipFile(string)"/> throws an exception due to an invalid line input</exception>
        public Ship[] ParseShipFile(string filePath);
    }
}
