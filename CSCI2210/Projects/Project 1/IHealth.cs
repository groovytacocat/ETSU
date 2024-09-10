using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal interface IHealth
    {
        /// <summary>
        /// Method to get the Maximum Health of a <see cref="Ship"/> (aka its Length)
        /// </summary>
        /// <returns><see cref="int"/> value representing the length of the <see cref="Ship"/></returns>
        public int GetMaxHealth();

        /// <summary>
        /// Calculates the current health of the <see cref="Ship"/> by subtracting number of damaged points from maximum health
        /// </summary>
        /// <returns><see cref="int"/> representing current health of the ship</returns>
        public int GetCurrentHealth();

        /// <summary>
        /// Method to determine if a <see cref="Ship"/> is dead based on its current health
        /// </summary>
        /// <returns>True if <see cref="Ship"/> has current health less than or equal to 0</returns>
        public bool IsDead();

    }
}
