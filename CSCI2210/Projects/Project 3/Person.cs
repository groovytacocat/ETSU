using System.Collections;

namespace Project_3;

public class Person
{
    public string Id {get; private set;}
    public int TravelStartTime {get; private set;}
    public int TravelEndTime {get; private set;}
    public bool IsInfected {get; private set;}
    public int InfectionCount {get; private set;}
    public int InfectionSpreadCount {get; private set;}
    public bool IsDead {get; private set;}
    public bool IsQuarantined {get; private set;}
    public bool IsImmune {get; private set;}
    public bool TravelAbility {get; private set;}
    public int InfectionTime {get; private set;}
    public int QuarantineTime {get; private set;}
    public double QuarantineChance {get; private set;}

    /// <summary>
    /// Method to infect a particular <see cref="Person"/>, sets the <see cref="IsInfected"/> property to true, increments infection count, increments the infector's spread count,
    /// and sets length of time to be infected
    /// </summary>
    /// <param name="infector"><see cref="Person"/> who spread the disease to the <see cref="Person"/> being infected</param>
    /// <param name="diseaseLength"><see cref="int"/> representing number of hours individual will be sick</param>
    public void BecomeInfected(Person infector, int diseaseLength)
    {
        if(!this.IsQuarantined && !this.IsDead && !this.IsInfected && !this.IsImmune)
        {
            this.IsInfected = true;
            this.InfectionCount++;
            infector.InfectionSpreadCount++;
            this.InfectionTime = diseaseLength;
        }
    }

    /// <summary>
    /// Method to decrement <see cref="InfectionTime"/> property. Will return before decrementing if the <see cref="Person"/> is dead, immune, or not infected.
    /// If <see cref="InfectionTime"/> is equal to 0 resets <see cref="IsInfected"/> to 0
    /// </summary>
    public void ProcessIllness()
    {
        if(this.IsDead || this.IsImmune || !this.IsInfected)
        {
            return;
        }
        
        this.InfectionTime--;
        if(this.InfectionTime == 0)
        {
            this.IsInfected = false;
        }
    }

    /// <summary>
    /// Method to set <see cref="IsQuarantined"/> to true to quarantine a <see cref="Person"/>
    /// </summary>
    /// <param name="quarantineLength"><see cref="int"/> representing length of time spent in quarantine (in hours)</param>
    public void EnterQuarantine(int quarantineLength)
    {
        this.IsQuarantined = true;
        this.QuarantineTime = quarantineLength;
    }

    /// <summary>
    /// Similar to <see cref="ProcessIllness"/> will decrement <see cref="QuarantineTime"/> and remove from Quarantine if equal to 0.
    /// </summary>
    public void InQuarantine()
    {
        if(this.IsDead || !this.IsQuarantined)
        {
            return;
        }

        this.QuarantineTime--;
        if(this.QuarantineTime == 0)
        {
            this.IsQuarantined = false;
        }
    }

    /// <summary>
    /// Method to allow a <see cref="Person"/> to travel
    /// </summary>
    public void CanTravel()
    {
        this.TravelAbility = true;
    }

    /// <summary>
    /// Method to prevent a <see cref="Person"/> from travelling
    /// </summary>
    public void DisableTravel()
    {
        this.TravelAbility = false;
    }

    /// <summary>
    /// Kills a <see cref="Person"/>
    /// </summary>
    public void Dies()
    {
        this.IsDead = true;
    }

    /// <summary>
    /// Constructor for a person.
    /// Will randomize for each person the hours that they are allowed to travel during
    /// Initializes all values to be a generic <see cref="Person"/>
    /// Will generate a random value between 0 and 1 to determine if that individual is given immunity to the disease
    /// </summary>
    public Person()
    {
        Random rand = new();
        this.TravelEndTime = rand.Next(0, 25);
        this.TravelStartTime = rand.Next(this.TravelEndTime);
        this.InfectionCount = 0;
        this.InfectionSpreadCount = 0;
        this.IsDead = false;
        this.IsQuarantined = false;
        this.IsInfected = false;
        this.Id = String.Empty;
        this.TravelAbility = true;
        this.InfectionTime = 0;
        double immuneChance = rand.NextDouble();
        if(immuneChance <= .125)
        {
            this.IsImmune = true;
        }
        else
        {
            this.IsImmune = false;
        }
        this.QuarantineChance = 0;
    }

    /// <summary>
    /// Constructor to set a <see cref="Person"/>'s name, infection status, and chance of quarantining
    /// </summary>
    /// <param name="inName"></param>
    /// <param name="inInfect"></param>
    /// <param name="inQuarChance"></param>
    public Person(string inName, bool inInfect, double inQuarChance) : this()
    {
        this.Id = inName;
        this.IsInfected = inInfect;
        this.InfectionCount = inInfect == true ? 1 : 0;
        this.QuarantineChance = inQuarChance;
    }
}
