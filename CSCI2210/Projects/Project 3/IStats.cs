namespace Project_3;

public interface IStats
{
    public string MostInfected(List<Person> people);
    public string GreatestSpreader(List<Person> people);
    public int NumberAlive(List<Person> people);
    public int DeathCount(List<Person> people);
    public int CurrentInfection(List<Person> people);
    public int CurrentQuarantine(List<Person> people);
}
