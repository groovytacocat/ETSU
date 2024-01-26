/*
*--------------------------------------------------------------------
* File name: Group11Robot.cs
* Project name: RobotCaveFightTestArena
* Solution name: RobotCaveFightTestArena
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven (hoovenar@etsu.edu) & Larry Deskins (deskinsl@etsu.edu)
* Course-Section: CSCI 1250
* Creation Date: 11/15/2023
* Modified Date: 11/29/2023
* -------------------------------------------------------------------
*/


using System;
using static System.Collections.Specialized.BitVector32;

namespace RobotCaveFightTestArena
{
    public class Group11Robot : IRobot
    {
        private string name;
        private string[] studentNames;
        private double attack;
        private double defense;
        private double speed;
        private double constitution;
        private double health;
        private string primaryColor;
        private string secondaryColor;
        private int lastAction;

        //Setter/Getter for the integer representing the choice of the last action taken
        public int LastAction
        {
            get
            {
                return this.lastAction;
            }

            set
            {
                this.lastAction = value;
            }
        }

        //Getters for Robot Attributes
        public double GetHealth()
        {
            return this.health;
        }

        public double GetMaxHealth()
        {
            return this.constitution * 10;
        }

        public string GetPrimaryColor()
        {
            return this.primaryColor;
        }

        public string GetRobotName()
        {
            return this.name;
        }

        public string GetSecondaryColor()
        {
            return this.secondaryColor;
        }
        public double GetAttack()
        {
            return this.attack;
        }

        public double GetDefense()
        {
            return this.defense;
        }
        public double GetSpeed()
        {
            return this.speed;
        }

        public string GetStats()
        {
            return $"Current Health: {this.GetHealth()}, Attack: {this.attack}, Defense: {this.defense}, Speed: {this.speed}";
        }

        public string[] GetStudentNames()
        {
            return this.studentNames;
        }
        // END Getters for Robot Attributes

        /// <summary>
        /// Generates a random integer with values of 1 - 4 (inclusive)
        /// If Robot health is under 40% and the action chosen was not heal changes to ensure robot heals itself
        /// Similarly if Robot's last action was the buff move and the current action choice isn't attack changes to make sure it attacks after buffing
        /// Performs the Action Chosen and saves that choice in the Last Action variable.
        /// </summary>
        /// <param name="opponent"></param>
        /// <returns>ActionResult representing the Robot's move in the fight</returns>
        public ActionResult PerformAction(IRobot opponent)
        {
            ActionResult action = new ActionResult("", "");

            Random random = new Random();

            int actionChoice = random.Next(1, 5);

            double healthPercent = this.health / GetMaxHealth();

            if(actionChoice != 4 && healthPercent < .4)
            {
                actionChoice = 4;
            }
            else if(actionChoice != 1 && LastAction == 2)
            {
                actionChoice = 1;
            }

            switch (actionChoice)
            {
                case 1:
                    action = Attack(opponent);
                    break;
                case 2:
                    action = SuperBuff();
                    break;
                case 3:
                    action = SuperArmor();
                    break;
                case 4:
                    action = BigHeal();
                    break;
                default:
                    break;
            }

            this.LastAction = actionChoice;

            return action;
        }


        /// <summary>
        /// Method to reset Robot to original stats as defined in the constructor
        /// </summary>
        public void Reset()
        {
            this.attack = 12.0;
            this.defense = 13.0;
            this.speed = 5.0;
            this.constitution = 10.0;
            this.health = constitution * 10.0;
        }

        /// <summary>
        /// Takes value of damage from opponent and calculates mitigated damage value based off defense
        /// Subtracts mitigated damage from health
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(double damage)
        {
            damage -= (this.defense / 100 * damage);

            this.health -= damage;
        }

        /// <summary>
        /// Subtracts 300 speed to heal Robot by 900 points
        /// Chosen for large number value that also doesn't risk potentially overflowing decimal value in the course of 100 rounds
        /// Per guidelines of project negative stat values are allowed 
        /// </summary>
        /// <returns></returns>
        public ActionResult BigHeal()
        {
            this.speed -= 300;
            this.health += 900;

            return new ActionResult("Big Heal", "Heal");
        }

        /// <summary>
        /// Reduces speed by 1000 to increase attack by 1000 points
        /// Chosen for large number value that also doesn't risk potentially overflowing decimal value in the course of 100 rounds
        /// Per guidelines of project negative stat values are allowed 
        /// </summary>
        /// <returns></returns>
        public ActionResult SuperBuff()
        {
            this.speed -= 1000;

            this.attack += 1000;

            return new ActionResult("Super Buff", "Power Up");
        }

        /// <summary>
        /// Reduces speed by 100 to increase defense by 100 points
        /// Once Defense is >= 100 the Robot will take no damage and will actually heal if Defense has values > 100
        /// Per guidelines of project negative stat values are allowed 
        /// </summary>
        /// <returns></returns>
        public ActionResult SuperArmor()
        {
            this.speed -= 100;
            this.defense += 100;

            return new ActionResult("Super Armor", "Power Up");
        }


        /// <summary>
        /// Takes Opponent Robot as an argument and calls the opponents TakeDamage Method using Robot's Attack stat as damage value
        /// </summary>
        /// <param name="opponent"></param>
        /// <returns></returns>
        public ActionResult Attack(IRobot opponent)
        {
            opponent.TakeDamage(this.attack);

            return new ActionResult("Attack", "Punch");
        }

        /// <summary>
        /// Constructor for Group 11 Robot to set all values prior to combat.
        /// </summary>
        public Group11Robot()
        {
            this.studentNames = new[] { "Adam Hooven", "Amalachukwu Emodi", "Larry Deskins" };
            this.name = "Optimus Prime";
            this.attack = 10.0;
            this.defense = 10.0;
            this.speed = 10.0;
            this.constitution = 10.0;
            this.health = this.constitution * 10.0;
            this.primaryColor = "#E81E27";
            this.secondaryColor = "#44BBFF";
        }
    }
}
