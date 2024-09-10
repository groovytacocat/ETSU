using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Object representing a <see cref="Destroyer"/> which is a type of <see cref="Ship"/> with Length 3
    /// </summary>
    internal class Destroyer : Ship
    {
        ///<inheritdoc />
        public override string GetName()
        {
            return "Destroyer";
        }

        /// <summary>
        /// Constructor to create a <see cref="Destroyer"/>. Passes position and direction to parent constructor with pre-defined length
        /// </summary>
        /// <param name="position">(X, Y) Coordinate of the <see cref="Ship"/> Starting Point</param>
        /// <param name="direction"><see cref="Enum"/> representing the direction of the <see cref="Ship"/></param>
        public Destroyer(Coord2D position, DirectionType direction) : base(position, direction, 3)
        {
        }
    }
}
