using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Battleship
{
    internal class ShipFactory : IShipFactory
    {
        ///<inheritdoc/>
        public Ship[] ParseShipFile(string filePath)
        {
            List<Ship> shipList = new List<Ship>();

            using (FileStream shipFile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new(shipFile))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        else
                        {
                            try
                            {
                                shipList.Add(ParseShipString(line));
                            }
                            catch (Exception e)
                            {
                                throw new Exception($"Unable to add ship: {e.Message}");
                            }
                        }
                    }
                }
            }

            return shipList.ToArray();
        }

        /// <inheritdoc/>
        public Ship ParseShipString(string description)
        {
            bool valid = VerifyShipString(description);

            if (valid)
            {
                string[] shipVals = description.Split(',');

                int x = int.Parse(shipVals[3].Trim());
                int y = int.Parse(shipVals[4].Trim());

                Coord2D pos = new Coord2D(x, y);

                DirectionType dir = shipVals[2].Trim().Equals("h") ? DirectionType.Horizontal : DirectionType.Vertical;

                switch (shipVals[0].ToUpper())
                {
                    case "CARRIER":
                        return new Carrier(pos, dir);
                    case "DESTROYER":
                        return new Destroyer(pos, dir);
                    case "SUBMARINE":
                        return new Submarine(pos, dir);
                    case "BATTLESHIP":
                        return new Battleship(pos, dir);
                    case "PATROL BOAT":
                        return new PatrolBoat(pos, dir);
                    default:
                        throw new Exception("Invalid Ship Type");   //Left here due to "Not all code paths return a value" error even though it would fail before this point
                }
            }
            else
            {
                throw new Exception("Invalid Ship String");
            }
        }

        /// <inheritdoc/>
        public bool VerifyShipString(string description)
        {
            string pattern = @"^(Carrier|Battleship|Destroyer|Patrol Boat|Submarine), \d+, (v|h), \d+, \d+$";

            bool valid = Regex.IsMatch(description, pattern, RegexOptions.IgnoreCase);

            if (valid)
            {
                string[] vals = description.Split(",");
                int len = int.Parse(vals[1].Trim());

                if(len < 6)
                {
                    if (vals[2].Trim().ToLower().Equals("h"))
                    {
                        if (int.Parse(vals[3].Trim()) + len > 10)
                        {
                            throw new Exception($" {vals[0]} has an Invalid Ship Position (Horizontal portion extends past grid)");
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (int.Parse(vals[4].Trim()) + len > 10)
                        {
                            throw new Exception($" {vals[0]} has an Invalid Ship Position (Vertical portion extends past grid)");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    throw new Exception($" {vals[0]} has an Invalid Ship Length");
                }
            }
            else
            {
                return false;
            }
        }
    }
}
/*
    QUESTIONS:
    What should the Parsing do if an invalid ship is parsed but the rest are valid?
    * Currently ends as soon as exception thrown. Assume that is the correct behavior.
    
    How to handle the nested try catch's? 

    Are my class constructors ok/Is it ok to use virtual methods in my abstract class?
 */