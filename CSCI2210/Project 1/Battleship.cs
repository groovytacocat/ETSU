using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Object representing a <see cref="Battleship"/> which is a type of <see cref="Ship"/> with Length 4
    /// </summary>
    internal class Battleship : Ship
    {
        ///<inheritdoc/>
        public override string GetName()
        {
            return "Battleship";
        }

        /// <summary>
        /// Constructor to create a <see cref="Battleship"/>. Passes position and direction to parent constructor with pre-defined length
        /// </summary>
        /// <param name="position">(X, Y) Coordinate of the <see cref="Ship"/> Starting Point</param>
        /// <param name="direction"><see cref="Enum"/> representing the direction of the <see cref="Ship"/></param>
        public Battleship(Coord2D position, DirectionType direction) : base(position, direction, 4)
        {
        }
    }
}
