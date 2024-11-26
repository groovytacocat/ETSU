using System.Collections;
using System.Security.Authentication;

namespace Project_3;

public class Location
{
    public string id {get; private set;}
    public List<Person> people {get; private set;}
    public List<Location> neighbors {get; private set;}
    public List<int> popPerTick {get; private set;}
    public List<int> sickPerTick {get; private set;}
    public List<int> quarPerTick {get; private set;}

    /// <summary>
    /// Method to add a neighbor <see cref="Location"/> node (connections are bidirectional)
    /// </summary>
    /// <param name="adjacent"><see cref="Location"/> to add as neighbor node</param>
    public void AddNeighbor(Location adjacent)
    {
        this.neighbors.Add(adjacent);
        adjacent.neighbors.Add(this);
    }

    /// <summary>
    /// Method to add a <see cref="Person"/> from the <see cref="Location"/>
    /// </summary>
    /// <param name="person"><see cref="Person"/> to be added</param>
    public void AddCitizen(Person person)
    {
        this.people.Add(person);
    }

    /// <summary>
    /// Method to remove a <see cref="Person"/> from the <see cref="Location"/>
    /// </summary>
    /// <param name="person"><see cref="Person"/> to be removed</param>
    public void RemoveCitizen(Person person)
    {
        this.people.Remove(person);
    }

    /// <summary>
    /// Method to calculate total population, infected population, and quarantined population in the <see cref="Location"/> at a particular tick
    /// </summary>
    public void LocationStats()
    {
        int sickTotal = 0;
        int quarTotal = 0;
        foreach(Person citizen in people)
        {
            if(citizen.IsInfected)
            {
                sickTotal++;
            }
            if(citizen.IsQuarantined)
            {
                quarTotal++;
            }
        }

        this.popPerTick.Add(this.people.Count);
        this.sickPerTick.Add(sickTotal);
        this.quarPerTick.Add(quarTotal);
    }

    /// <summary>
    /// Constructor for a Location
    /// </summary>
    /// <param name="inName">Name of City</param>
    /// <param name="inPeople">List of <see cref="Person"/> that are in the city</param>
    /// <param name="inNeighbors">List of <see cref="Location"/> that represent neighbor nodes in the graph</param>
    public Location(string inName, List<Person> inPeople, List<Location> inNeighbors)
    {
        this.id = inName;
        this.people = inPeople;
        this.neighbors = inNeighbors;
        this.popPerTick = new List<int>();
        this.sickPerTick = new List<int>();
        this.quarPerTick = new List<int>();
    }
    public Location(string inName) : this(inName, new List<Person>(), new List<Location>()){}
}
