using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// Abstract Class representing the Platonic idea of a Ship
    /// </summary>
    internal abstract class Ship : IHealth, IInfomatic
    {
        /// <summary>
        /// (X, Y) Coordinate of the <see cref="Ship"/> Starting Point
        /// </summary>
        public Coord2D Position { get; private set; }

        /// <summary>
        /// Length of the <see cref="Ship"/>
        /// </summary>
        public byte Length { get; private set; }

        /// <summary>
        /// <see cref="Array"/> of <see cref="Coord2D"/> representing the points of the <see cref="Ship"/>
        /// </summary>
        public Coord2D[] Points { get; private set; }

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="Coord2D"/> representing the damaged points of the <see cref="Ship"/>
        /// </summary>
        public List<Coord2D> DamagedPoints { get; private set; }

        /// <summary>
        /// <see cref="Enum"/> representing the direction of the <see cref="Ship"/>
        /// </summary>
        public DirectionType Direction { get; private set; }

        /// <summary>
        /// Virtual Method to check if the <see cref="Coord2D"/> input is a valid <see cref="Ship"/> location
        /// </summary>
        /// <param name="point"><see cref="Coord2D"/> representing the player's guess for a strike</param>
        /// <returns>True if the point is a hit</returns>
        public virtual bool CheckIfHit(Coord2D point)
        {
            return Points.Contains(point);
        }

        /// <summary>
        /// Method damage a <see cref="Ship"/> and prevent that point from being used to damage again.
        /// If the guessed point is not the in <see cref="DamagedPoints"/> then it is added to the list
        /// </summary>
        /// <param name="point"><see cref="Coord2D"/> representing the player's guess for a strike</param>
        public virtual void TakeDamage(Coord2D point)
        {
            if(!DamagedPoints.Contains(point))
            {
                DamagedPoints.Add(point);
            }
        }

        /// <summary>
        /// Abstract method to return the Name of a <see cref="Ship"/>
        /// </summary>
        /// <returns><see cref="string"/> representing the name of the <see cref="Ship"/></returns>
        public abstract string GetName();

        /// <inheritdoc/>
        public int GetMaxHealth()
        {
            return this.Length;
        }

        /// <inheritdoc/>
        public int GetCurrentHealth()
        {
            return this.GetMaxHealth() - this.DamagedPoints.Count;
        }

        /// <inheritdoc/>
        public bool IsDead()
        {
            return GetCurrentHealth() <= 0;
        }

        /// <inheritdoc/>
        public string GetInfo()
        {
            return $"Max Health: {GetMaxHealth()}\nCurrent Health: {GetCurrentHealth()}\nAlive/Dead: {(IsDead() ? "Dead" : "Alive")}\nPosition: ({Position.x}, {Position.y})\n" +
                $"Length: {this.Length}\nDirection: {(this.Direction == DirectionType.Vertical ? "Vertical" : "Horizontal")} ";
        }

        /// <summary>
        /// Constructor for <see cref="Ship"/>.
        /// Takes the length to create <see cref="Points"/> based on length, then creates a new instance of <see cref="DamagedPoints"/>, 
        /// Assigns values to <see cref="Points"/> based on <see cref="Length"/> and <see cref="Direction"/>
        /// </summary>
        /// <param name="position"><see cref="Coord2D"/> representing the top left of the ship</param>
        /// <param name="direction"><see cref="DirectionType"/> representing orientation of <see cref="Ship"/></param>
        /// <param name="length"><see cref="int"/> representing lenght of <see cref="Ship"/></param>
        public Ship(Coord2D position, DirectionType direction, byte length)
        {
            this.Direction = direction;
            this.Length = length;
            this.Points = new Coord2D[Length];
            this.DamagedPoints = new List<Coord2D>();
            this.Position = position;

            for(int i = 0; i < this.Length; i++)
            {
                if(direction == DirectionType.Vertical)
                {
                    this.Points[i] = new Coord2D(position.x, position.y + i);
                }
                else
                {
                    this.Points[i] = new Coord2D(position.x + i, position.y);
                }
            }
        }
    }
}
