using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Object representing a <see cref="Submarine"/> which is a type of <see cref="Ship"/> with Length 3
    /// </summary>
    internal class Submarine : Ship
    {
        /// <inheritdoc/>
        public override string GetName()
        {
            return "Submarine";
        }

        /// <summary>
        /// Constructor to create a <see cref="Submarine"/>. Passes position and direction to parent constructor with pre-defined length
        /// </summary>
        /// <param name="position">(X, Y) Coordinate of the <see cref="Ship"/> Starting Point</param>
        /// <param name="direction"><see cref="Enum"/> representing the direction of the <see cref="Ship"/></param>
        public Submarine(Coord2D position, DirectionType direction) : base(position, direction, 3)
        { 
        }
    }
}
