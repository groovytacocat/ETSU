using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Battleship
{
    internal class Program
    {
        #region MAIN
        static void Main(string[] args)
        {
            ShipFactory shipFactory = new ShipFactory(); 
            Ship[] Fleet;
            
            /*
             * Tries: 
                 * Open a file from the command-line if one was given, if not prompts user for a filepath. Attempts to open the file at the given filepath if it fails for any reason an Exception is thrown
                 * Do-While loop prompting user to enter commands/play the game. 
                     * If a user enters 'info' displays all info of ships loaded from file
                     * If a user enters coordinates in the form X, Y (uses RegEx to recognize digit input and splits/parses to int values) then checks to see if that location hit a ship and applies damage if so
                        * After damaging the ship(s) checks to see if all ships are dead then ends game automatically if they are
                     * If a user enters 'exit' the game quits
                     * All other input is met with 'Command not recognized'
             * Catches:
                * Any exceptions thrown by either parsing the ship file, parsing the input data of the ship file, or any other unexpected errors
             * Finally:
                * Thanks the player for playing
             */
            try
            {
                if (args.Length == 0)
                {
                    string filePath = String.Empty;
                    Console.Write("Enter a filepath containing a Ship file: ");
                    filePath = Console.ReadLine();
                    Fleet = shipFactory.ParseShipFile(filePath);
                }
                else
                {
                    Fleet = shipFactory.ParseShipFile(args[0]);
                }

                //if(Fleet is null)
                //{
                //    throw new Exception("File not found/cannot be opened");
                //}

                string input = String.Empty;
                bool gameDone = false;

                do
                {
                    Console.Write("Enter a command: (info for ship information, exit to quit, coordinates in the form X, Y to make a guess: ");
                    input = Console.ReadLine();

                    int lives = Fleet.Length;

                    if (input.ToUpper().Equals("INFO"))
                    {
                        ShipInfo(Fleet);
                    }
                    else if (Regex.IsMatch(input, @"\d+, \d+"))
                    {
                        int x = int.Parse(input.Split(",")[0].Trim());
                        int y = int.Parse(input.Split(",")[1].Trim());

                        Coord2D guess = new Coord2D(x, y);

                        PlayerGuess(guess, Fleet);

                        gameDone = GameState(Fleet);
                    }
                    else if (input.ToUpper().Equals("EXIT"))
                    {
                        gameDone = true;
                    }
                    else
                    {
                        Console.WriteLine("Command not recognized");
                    }

                } while (!gameDone); ;
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            finally
            {
                Console.WriteLine("\nThanks for playing");
            }
        }
        #endregion

        /// <summary>
        /// Method to print info about all <see cref="Ship"/> objects in <see cref="Array"/>
        /// </summary>
        /// <param name="ships"><see cref="Array"/> of <see cref="Ship"/> objects</param>
        public static void ShipInfo(Ship[] ships)
        {
            foreach(Ship ship in ships)
            {
                Console.WriteLine($"\nName: {ship.GetName()}\n{ship.GetInfo()}");
            }
        }

        /// <summary>
        /// Method to take players guess and apply damage if a hit was successful
        /// </summary>
        /// <param name="guess"><see cref="Coord2D"/> representing player guess</param>
        /// <param name="ships"><see cref="Array"/> of <see cref="Ship"/> objects in game</param>
        public static void PlayerGuess(Coord2D guess, Ship[] ships)
        {
            foreach(Ship s in ships)
            {
                if (s.CheckIfHit(guess)) s.TakeDamage(guess);
            }
        }

        /// <summary>
        /// Method to determine game state. Checks all <see cref="Ship"/> objects for status. If dead decrements total ships from game, once 0 game is completed
        /// </summary>
        /// <param name="ships"><see cref="Array"/> of <see cref="Ship"/> objects</param>
        /// <returns>True if all <see cref="Ship"/>s in <see cref="Array"/> are dead</returns>
        public static bool GameState(Ship[] ships)
        {
            int lives = ships.Length;

            foreach(Ship ship in ships)
            {
                if(ship.IsDead())
                {
                    lives--;
                }
            }

            return lives == 0;
        }
    }
}
