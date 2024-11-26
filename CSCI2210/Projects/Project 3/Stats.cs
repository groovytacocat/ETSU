
namespace Project_3;

public class Stats : IStats
{
    /// <summary>
    /// Method to identify the <see cref="Person"/> who has been infected the most.
    /// In the event of a tie, the most recent <see cref="Person"/> will be selected
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="string"/> containing the <see cref="Person.Id"/> who has been infected the most and the number of times they have been infected</returns>
    public string MostInfected(List<Person> people)
    {
        int max = 0;
        string subject = String.Empty;
        foreach(Person p in people)
        {
            if(p.InfectionCount >= max)
            {
                max = p.InfectionCount;
                subject = $"{p.Id}: {max}";
            }
        }

        return subject;
    }

    /// <summary>
    /// Method to identify the <see cref="Person"/> who has spread the disease the most
    /// As above this will just select the most recent <see cref="Person"/> in the event of a tie
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="string"/> containing the <see cref="Person.Id"/> who has infected the most people and the number of times they have infected others </returns>
    public string GreatestSpreader(List<Person> people)
    {
        int max = 0;
        string subject = String.Empty;
        foreach(Person p in people)
        {
            if(p.InfectionSpreadCount >= max)
            {
                max = p.InfectionSpreadCount;
                subject = $"{p.Id}: {max}";
            }
        }

        return subject;
    }

    /// <summary>
    /// Method to calculate the greatest number of infections
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="int"/> representing the greatest number of people infected by an individual</returns>
    public int GreatestSpread(List<Person> people)
    {
        int max = 0;
        foreach(Person p in people)
        {
            if(p.InfectionSpreadCount >= max)
            {
                max = p.InfectionSpreadCount;
            }
        }

        return max;
    }

    /// <summary>
    /// Method to calculate the number of <see cref="Person"/>s currently alive in a list of <see cref="Person"/>
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="int"/> representing number of living <see cref="Person"/> in List</returns>
    public int NumberAlive(List<Person> people)
    {
        int numAlive = 0;
        foreach(Person p in people)
        {
            if(!p.IsDead)
            {
                numAlive++;
            }
        }

        return numAlive;
    }

    /// <summary>
    /// Method to determine number of dead <see cref="Person"/>s in a List
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="int"/> representing number of dead <see cref="Person"/>s in List</returns>
    public int DeathCount(List<Person> people)
    {
        return people.Count - NumberAlive(people);
    }

    /// <summary>
    /// Method to determine number of infected <see cref="Person"/>s in a List
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="int"/> representing number of infected <see cref="Person"/>s in List</returns>
    public int CurrentInfection(List<Person> people)
    {
        int numInfected = 0;
        foreach(Person p in people)
        {
            if(!p.IsDead && p.IsInfected)
            {
                numInfected++;
            }
        }

        return numInfected;
    }
    
    /// <summary>
    /// Method to determine number of quarantined <see cref="Person"/>s in a List
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="int"/> representing number of quarantined <see cref="Person"/>s in List</returns>
    public int CurrentQuarantine(List<Person> people)
    {
        int numQuar = 0;
        foreach(Person p in people)
        {
            if(!p.IsDead && p.IsQuarantined)
            {
                numQuar++;
            }
        }

        return numQuar;
    }

    /// <summary>
    /// Method to calculate the % (not as a decimal) of infected <see cref="Person"/>s
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="double"/> representing % infected <see cref="Person"/>s in List</returns>
    public double PercentInfected(List<Person> people)
    {
        double value = (double)CurrentInfection(people) / people.Count * 100;

        return value;
    }

    /// <summary>
    /// Method to calculate the % (not as a decimal) of dead <see cref="Person"/>s
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="double"/> representing % dead <see cref="Person"/>s in List</returns>
    public double PercentDead(List<Person> people)
    {
        double value = (double)DeathCount(people) / people.Count * 100;

        return value;
    }

    /// <summary>
    /// Method to calculate average spread of disease
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="double"/> representing average infection spread</returns>
    public double AverageSpread(List<Person> people)
    {
        double total = 0;
        foreach(Person p in people)
        {
            total += p.InfectionSpreadCount;
        }
        total /= people.Count;

        return total;
    }

    /// <summary>
    /// Method to calculate the statistics for the hourly csv report.
    /// Creates an empty <see cref="string"/> array and assigns values of prior methods 
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <returns><see cref="string"/> array containing statistics for the hourly report</returns>
    public string[] HourlyReportStats(List<Person> people)
    {
        string[] data =
        [
            MostInfected(people),
            GreatestSpreader(people),
            NumberAlive(people).ToString(),
            DeathCount(people).ToString(),
            CurrentInfection(people).ToString(),
            CurrentQuarantine(people).ToString(),
        ];

        return data;
    }

    /// <summary>
    /// Method to calculate average statistics for final report.
    /// </summary>
    /// <param name="city"><see cref="Location"/> List to search through</param>
    /// <returns><see cref="double"/> array containing the averaged stats for the final report</returns>
    public double[] AvgStats(Location city)
    {
        double[] avgStats = 
        [
            city.popPerTick.Average(),
            city.sickPerTick.Average(),
            city.quarPerTick.Average()
        ];

        return avgStats;
    }
}
