using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Object representing a <see cref="Carrier"/> which is a type of <see cref="Ship"/> with Length 5
    /// </summary>
    internal class Carrier : Ship
    {
        ///<inheritdoc/>
        public override string GetName()
        {
            return "Carrier";
        }

        /// <summary>
        /// Constructor to create a <see cref="Carrier"/>. Passes position and direction to parent constructor with pre-defined length
        /// </summary>
        /// <param name="position">(X, Y) Coordinate of the <see cref="Ship"/> Starting Point</param>
        /// <param name="direction"><see cref="Enum"/> representing the direction of the <see cref="Ship"/></param>
        public Carrier(Coord2D position, DirectionType direction) : base(position, direction, 5)
        {
            
        }
    }
}