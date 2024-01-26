using System;
namespace RobotCaveFightTestArena
{
    public class Fighter : IRobot
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

        public ActionResult PerformAction(IRobot opponent)
        {
            ActionResult action = new ActionResult("", "");

            Random random = new Random();

            int actionChoice = random.Next(1, 5);

            if(actionChoice == 4 && this.health >= 90)
            {
                actionChoice = random.Next(1, 4);
            }
            else if(actionChoice != 4 && this.health <= 20)
            {
                actionChoice = 4;
            }

            switch(actionChoice)
            {
                case 1:
                    action = Attack(opponent);
                    break;
                case 2:
                    action = AntiVirusScan(opponent);
                    break;
                case 3:
                    action = BrainDive(opponent);
                    break;
                case 4:
                    action = NaniteInjection(opponent);
                    break;
            }

            return action;
        }

        public void Reset()
        {
            this.attack = 12.0;
            this.defense = 13.0;
            this.speed = 5.0;
            this.constitution = 10.0;
            this.health = constitution * 10.0;
        }

        public void TakeDamage(double damage)
        {
            damage -= (this.defense / 100 * damage);

            this.health -= damage;
        }
        
        public ActionResult NaniteInjection()
        {
            double hpPercent = this.health / this.GetMaxHealth();

            if(hpPercent < .5)
            {
                this.speed -= 5.0;
                this.health += 15.0;
            }
            else
            {
                this.speed -= 2.5;
                this.health += 7.5;
            }

            return new ActionResult("Nanite Injection", "Heal");
        }

        public ActionResult BrainDive(IRobot opponent)
        {
            double maxSacrifice;
            int buffPoints;

            if((this.health / this.GetMaxHealth()) > .75)
            {
                this.health -= 30;
                this.attack += 9.5;
                this.defense += 2.5;
            }
            else if ((this.health / this.GetMaxHealth()) > .50)
            {
                this.health -= 15;
                this.attack += 3.5;
                this.defense += 1.5;
            }
            else
            {
                maxSacrifice = this.health - 1;
                this.health = 1;
                buffPoints = (int)maxSacrifice / 3;
                this.attack += buffPoints;
            }

            return new ActionResult("Brain Dive", "Power Up");
        }

        public ActionResult AntiVirusScan()
        {
            if( (this.health / this.GetMaxHealth()) >= .9)
            {
                this.speed--;
                this.defense++;
            }
            else if ((this.health / this.GetMaxHealth()) >= .5)
            {
                this.speed -= 2.5;
                this.defense += 2.5;
            }
            else
            {
                this.speed -= 5.0;
                this.defense += 5.0;
            }

                return new ActionResult("Anti-Virus Scan", "Power Up");
        }

        public ActionResult Attack(IRobot opponent)
        {
            opponent.TakeDamage(this.attack);

            return new ActionResult("Attack", "Punch");
        }

        public Fighter()
        {
            this.attack = 12.0;
            this.defense = 13.0;
            this.speed = 5.0;
            this.constitution = 10.0;
            this.health = constitution * 10.0;
            this.studentNames = new[] { "Adam Hooven" };
            this.name = "Tachikoma";
            this.primaryColor = "#849ce7";
            this.secondaryColor = "#ffffff";
        }
    }
}

