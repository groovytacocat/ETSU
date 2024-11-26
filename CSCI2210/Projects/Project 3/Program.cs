using System.Diagnostics;

namespace Project_3;

class Program
{
    /// <summary>
    /// Main method. Initializes all necessary variables for simulation to run, collect data, and output data.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        FileIO fileIO = new FileIO();
        List<Person> worldPopulation = new();
        List<Location> world = InitializeLocations();
        List<Configuration> simulationConfigs = new List<Configuration>();

        /*
        Prompt a user for an input file if one wasn't given on the command line, otherwise will use command-line arg provided and attempt to populate the simulationConfigs List
        */
        try
        {
            if(args.Length == 0)
            {
                Console.Write("Enter a filepath for the simulation configuration(s): ");
                string filePath = Console.ReadLine();
                simulationConfigs = fileIO.ReadFile(filePath);
            }
            else
            {
                simulationConfigs = fileIO.ReadFile(args[0]);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine($"Exception Caught\n\tMessage: {e.Message}");
        }

        int simNum = 1;

        /*
        Main logic for executing the simulation for each provided Configuration
        Creates a Stats object for data collection
        Creates/Initializes variables to track total run time and number of infected people, as well as output files for hourly csv logs and final report for a given simulation
        Initializes each Location with a variable amount of people based on the Configuration's specifications
        Do-While Loop to run a simulation until all individuals are dead/not infected, or if expected run time is reached
        Foreach loop to process each hour of the simulation in each city 
        */
        foreach(Configuration scenario in simulationConfigs)
        {
            Stats simulationStats = new Stats();
            string logFile = $@"./simulation_{simNum}_log.csv";
            string reportFile = $@"./simulation_{simNum}_report.txt";
            int runTime = 0;
            int totalInfected = 0;
            InitializePeople(world, worldPopulation, scenario);
            fileIO.LogEvents(logFile);
            Console.WriteLine($"Sim {simNum} Running now");
            do
            {
                foreach(Location city in world)
                {
                    QuarantinePeople(city.people, scenario.QuarantineLifeTime);
                    DiseaseMortality(city.people, scenario.DeathChance);
                    InfectPeople(city.people, scenario.InfectChance, scenario.DiseaseLifetime);
                    ManageDiseaseAndQuarantine(city.people);
                    city.LocationStats();
                    PeopleTravel(city.neighbors, runTime % 24, scenario.TravelChance);
                    ResetTravel(city.people);
                }
                totalInfected += simulationStats.CurrentInfection(worldPopulation);
                string[] hourlyData = simulationStats.HourlyReportStats(worldPopulation);
                fileIO.LogEvents(logFile, hourlyData);
                runTime++;
            }
            while(!SimulationDone(worldPopulation) && runTime < scenario.ExpectedSimulationTime);

            Console.WriteLine($"simStats Percent Infected: {simulationStats.PercentInfected(worldPopulation)}\nPercent Dead: {simulationStats.PercentDead(worldPopulation)}");
            fileIO.GenerateReport(reportFile, runTime, totalInfected, simulationStats.DeathCount(worldPopulation), 
                                    simulationStats.PercentInfected(worldPopulation), simulationStats.PercentDead(worldPopulation), simulationStats.AverageSpread(worldPopulation),
                                    simulationStats.GreatestSpread(worldPopulation), world);
            simNum++;
        }
    }

    /// <summary>
    /// Method to create the graph of <see cref="Location"/> objects in the simulation
    /// and add all their associated neighbors
    /// </summary>
    /// <returns>Graph that represents list of <see cref="Location"/> objects for simulation</returns>
    public static List<Location> InitializeLocations()
    {
        List<Location> places = new();
        Location kpt = new Location("Kingsport");
        Location jc = new Location("Johnson City");
        Location bristol = new Location("Bristol");
        Location blountville = new Location("Blountville");
        Location pineyFlats = new Location("Piney Flats");
        Location elizabethton = new Location("Elizabethton");
        kpt.AddNeighbor(jc);
        kpt.AddNeighbor(bristol);
        kpt.AddNeighbor(blountville);
        jc.AddNeighbor(pineyFlats);
        jc.AddNeighbor(bristol);
        jc.AddNeighbor(elizabethton);
        pineyFlats.AddNeighbor(blountville);
        pineyFlats.AddNeighbor(elizabethton);
        bristol.AddNeighbor(blountville);
        places.Add(kpt);
        places.Add(jc);
        places.Add(bristol);
        places.Add(pineyFlats);
        places.Add(blountville);
        places.Add(elizabethton);

        return places;
    }

    /// <summary>
    /// Method to generate 2 random values along a normal distribution when provided with a mean and standard deviation using the Box-Muller transform 
    ///         (credit: StackOverflow https://stackoverflow.com/questions/9951883/generating-values-from-normal-distribution-using-box-muller-method)
    /// Randomly determines which of the 2 random values to return once they have been generated since only one is needed.
    /// </summary>
    /// <param name="mu"><see cref="double"/> representing the mean of a normal distribution</param>
    /// <param name="sigma"><see cref="double"/> representing standard deviation of normal distribution</param>
    /// <returns></returns>
    public static double GenerateRandom(double mu, double sigma)
    {
        Random rand = new Random();
        double u = rand.NextDouble();
        double v = rand.NextDouble();

        double z1 = Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v);
        double z2 = Math.Sqrt(-2 * Math.Log(u)) * Math.Sin(2 * Math.PI * v);

        double x1 = sigma * z1 + mu;
        double x2 = sigma * z2 + mu;

        return rand.Next() % 2 == 0 ? x1 : x2;
    }

    /// <summary>
    /// Method to initialize the population of a each <see cref="Location"/> node in a graph according to the specifications provided by the <see cref="Configuration"/>
    /// Uses foreach loop to iterate through each node of the graph and generate random values to determine if a <see cref="Person"/> will be infected or not 
    ///     and their likelihood of quarantining within specified <see cref="Configuration"/> guidelines
    /// </summary>
    /// <param name="places"><see cref="Location"/> list representing the graph</param>
    /// <param name="population"><see cref="Person"/> list to store all created <see cref="Person"/>s</param>
    /// <param name="config"><see cref="Configuration"/> for the given simulation scenario</param>
    public static void InitializePeople(List<Location> places, List<Person> population, Configuration config)
    {
        Random rand = new();

        foreach(Location city in places)
        {
            double populationNormalDistribution = GenerateRandom(config.MeanPopulation, config.StdDev);

            for(int i = 0; i < (int)populationNormalDistribution; i++)
            {
                double infection = rand.NextDouble() * 100;
                bool infected = false;
                double quarantineNormalDistribution = GenerateRandom(config.MeanChanceQuarantine, config.StdDevQuarantineRate);
                if(infection <= config.InfectChance)
                {
                    infected = true;
                }
                Person citizen = new Person($"{city.id}_Person_{i}", infected, quarantineNormalDistribution);
                city.people.Add(citizen);
                population.Add(citizen);
            }
        }
    }

    /// <summary>
    /// Method to iterate through a list of <see cref="Person"/>s and randomly generate a value to determine if that individual will enter quarantine or not.
    /// Quarantine is only entered if a person is alive, infected, not immune, and not already in quarantine
    /// </summary>
    /// <param name="people"><see cref="Person"/> list to search through</param>
    /// <param name="quarLength"><see cref="int"/> representing length of time to quarantine for</param>
    public static void QuarantinePeople(List<Person> people, int quarLength)
    {
        Random rand = new();

        foreach(Person person in people)
        {
            if(person.IsInfected && !person.IsDead && !person.IsQuarantined && !person.IsImmune)
            {
                double chance = rand.NextDouble();
                if(chance <= person.QuarantineChance)
                {
                    person.EnterQuarantine(quarLength);
                }
            }
        }
    }

    /// <summary>
    /// Method to allow for infection to spread between non-quarantined infected people and non-quarantined non-infected people
    /// Determines eligible infecteds and non-infecteds then iterates through both lists to allow for each infected person to have a chance of infecting all non-infected individuals
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to search through</param>
    /// <param name="contagionRate"><see cref="double"/> representing the % chance of infection</param>
    /// <param name="infectionLength"><see cref="int"/> representing length of infection (in hours)</param>
    public static void InfectPeople(List<Person> people, double contagionRate, int infectionLength)
    {
        Random rand = new();
        List<Person> infected = new();
        List<Person> clean = new();

        foreach(Person person in people)
        {
            if(person.IsInfected && !person.IsDead && !person.IsQuarantined)
            {
                infected.Add(person);
            }
            if(!person.IsInfected && !person.IsQuarantined)
            {
                clean.Add(person);
            }
        }

        for(int i = 0; i < infected.Count; i++)
        {
            for(int j = 0; j < clean.Count; j++)
            {
                double infection = rand.NextDouble() * 100;
                if(infection <= contagionRate && !clean[j].IsInfected && !clean[j].IsImmune)
                {
                    clean[j].BecomeInfected(infected[i], infectionLength);
                }
            }
        }
    }

    /// <summary>
    /// Method to simulate mortality of disease on infected individuals
    /// Iterates through list of <see cref="Person"/>s and any that are infected generates a random value to determine if they will die to the disease
    /// </summary>
    /// <param name="people"><see cref="Person"/> list to search through</param>
    /// <param name="mortalityChance"><see cref="double"/> representing % chance of mortality from disease</param>
    public static void DiseaseMortality(List<Person> people, double mortalityChance)
    {
        foreach(Person person in people)
        {
            Random rand = new();
            double chance = rand.NextDouble() * 100;

            if(chance <= mortalityChance && person.IsInfected)
            {
                person.Dies();
            }
        }
    }

    /// <summary>
    /// Method to simulate travel of <see cref="Person"/>s between <see cref="Location"/> nodes.
    /// Iterates through each <see cref="Location"/> and its population to determine if a person who is not quarantined and alive to travel if able to and between the hours they are eligible to travel during
    /// Generates a random value to compare against their travel chance to determine if they will actually travel.
    /// Generates a random value to determine which neighbor node to visit a <see cref="Person"/> will travel to.
    /// Uses a <see cref="Dictionary"/> that maps <see cref="Person"/> keys to a <see cref="Location"/> tuple representing where they will travel from and where they will travel to.
    /// Once map is created disables <see cref="Person"/>'s ability to travel to prevent movement between multiple nodes in a single tick
    /// Iterates through map to remove <see cref="Person"/> from their current location to the location
    /// </summary>
    /// <param name="places"></param>
    /// <param name="time"></param>
    /// <param name="travelChance"></param>
    public static void PeopleTravel(List<Location> places, int time, double travelChance)
    {
        Dictionary<Person, (Location from, Location to)> moveMap = new Dictionary<Person, (Location from, Location to)>();
        foreach(Location city in places)
        {
            foreach(Person person in city.people)
            {
                if(!person.IsQuarantined && !person.IsDead && time >= person.TravelStartTime && time <= person.TravelEndTime && person.TravelAbility)
                {
                    Random rand = new();
                    double personTravel = rand.NextDouble() * 100;
                    int newCity = rand.Next(0, city.neighbors.Count);
                    if(personTravel <= travelChance)
                    {
                        moveMap[person] = (city, city.neighbors[newCity]);
                        person.DisableTravel();
                    }
                }
            }
        }

        foreach(KeyValuePair<Person, (Location from, Location to)> kvp in moveMap)
        {
            kvp.Value.from.RemoveCitizen(kvp.Key);
            kvp.Value.to.AddCitizen(kvp.Key);
        }
    }

    /// <summary>
    /// Method to re-enable travel for all <see cref="Person"/>s in list for next tick
    /// </summary>
    /// <param name="people"><see cref="Person"/> List to iterate through</param>
    public static void ResetTravel(List<Person> people)
    {
        // After everyone has moved resets Travel Ability flag to true to allow travel for next hour
        foreach(Person person in people)
        {
            person.CanTravel();
        }
    }

    /// <summary>
    /// Method to simulate disease action and quarantine for <see cref="Person"/>s. If a <see cref="Person"/> is infected or quarantined it will decrement their corresponding infection/quarantine timers
    /// </summary>
    /// <param name="people"><see cref="Person"/> list to iterate through</param>
    public static void ManageDiseaseAndQuarantine(List<Person> people)
    {
        foreach(Person p in people)
        {
            if(p.IsInfected && !p.IsDead)
            {
                p.ProcessIllness();
            }
            if(p.IsQuarantined && !p.IsDead)
            {
                p.InQuarantine();
            }
        }
    }

    /// <summary>
    /// Method to determine if the simluation is completed based on living population/infected rate.
    /// Takes a list of <see cref="Person"/> and iterates through list to decrement lives for each dead <see cref="Person"/> and track infections.
    /// If either result is 0 returns true to exit simulation
    /// </summary>
    /// <param name="population"><see cref="Person"/> list to iterate through</param>
    /// <returns><see cref="bool"/> representing if the simulation is done due to total death or disease eradication</returns>
    public static bool SimulationDone(List<Person> population)
    {
        int lives = population.Count;
        int infectedCount = 0;

        foreach(Person person in population)
        {
            if(person.IsDead)
            {
                lives--;
            }
            else if(person.IsInfected)
            {
                infectedCount++;
            }
        }

        return lives == 0 || infectedCount == 0;
    }
}

/*
Simulating People as they move from Location 0 to Location X etc
*/