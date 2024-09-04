using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal interface IInfomatic
    {
        /// <summary>
        /// Method to provide all pertinent info about a <see cref="Ship"/> as a formatted <see cref="string"/> sans name.
        /// </summary>
        /// <returns><see cref="string"/> containing <see cref="Ship"/> info </returns>
        public string GetInfo();
    }
}
