using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Struct representing the X and Y coordinates of a <see cref="Ship"/>
    /// </summary>
    internal struct Coord2D
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coord2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
