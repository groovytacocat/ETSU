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
        #region ParseShipFile
        ///<inheritdoc/>
        public Ship[] ParseShipFile(string filePath)
        {
            List<Ship> shipList = new List<Ship>();

            using (StreamReader shipFile = new StreamReader(filePath))
            {
                string line;

                while ((line = shipFile.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }

                    try
                    {
                        shipList.Add(ParseShipString(line));
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Unable to add ship: {e.Message}", e);
                    }
                }
            }

            return shipList.ToArray();
        }
        #endregion

        #region ParseShipString
        /// <inheritdoc/>
        public Ship ParseShipString(string description)
        {
            bool valid = VerifyShipString(description);

            if (!valid) throw new Exception("Invalid Ship String");

            string[] shipVals = description.Split(',');

            Coord2D pos = new Coord2D(int.Parse(shipVals[3].Trim()), int.Parse(shipVals[4].Trim()));

            DirectionType dir = shipVals[2].Trim().ToLower().Equals("h") ? DirectionType.Horizontal : DirectionType.Vertical;

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
        #endregion

        #region VerifyShipString
        /// <inheritdoc/>
        public bool VerifyShipString(string description)
        {
            string pattern = @"^(Carrier|Battleship|Destroyer|Patrol Boat|Submarine), \d+, (v|h), \d+, \d+$";

            bool valid = Regex.IsMatch(description, pattern, RegexOptions.IgnoreCase);

            if (!valid) return false;

            string[] vals = description.Split(",");
            int len = int.Parse(vals[1].Trim());

            if (len >= 6 || len <= 0) throw new Exception("LENGTH BAD");

            if (vals[2].Trim().ToLower().Equals("h") && (int.Parse(vals[3].Trim()) + len >= 10 || int.Parse(vals[3].Trim()) <= 0)) throw new Exception("HORIZONTAL LENGTH BAD");

            if (vals[2].Trim().ToLower().Equals("v") && (int.Parse(vals[4].Trim()) + len >= 10 || int.Parse(vals[4].Trim() <= 0))) throw new Exception("VERTICAL LENGTH BAD");

            return true;
        }
        #endregion
    }
}
