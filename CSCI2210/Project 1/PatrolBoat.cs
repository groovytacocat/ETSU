using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Object representing a <see cref="PatrolBoat"/> which is a type of <see cref="Ship"/> with Length 2
    /// </summary>
    internal class PatrolBoat : Ship
    {
        ///<inheritdoc />
        public override string GetName()
        {
            return "Patrol Boat";
        }

        /// <summary>
        /// Constructor to create a <see cref="PatrolBoat"/>. Passes position and direction to parent constructor with pre-defined length
        /// </summary>
        /// <param name="position">(X, Y) Coordinate of the <see cref="Ship"/> Starting Point</param>
        /// <param name="direction"><see cref="Enum"/> representing the direction of the <see cref="Ship"/></param>
        public PatrolBoat(Coord2D position, DirectionType direction) : base(position, direction, 2)
        {
        }
    }
}
