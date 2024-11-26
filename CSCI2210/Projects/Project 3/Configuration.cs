using System.Runtime.CompilerServices;

namespace Project_3;

public class Configuration
{
    public int MeanPopulation {get; private set;}
    public double StdDev {get; private set;}
    public double InfectChance {get; private set;}
    public double DeathChance {get; private set;}
    public int DiseaseLifetime {get; private set;}
    public int QuarantineLifeTime {get; private set;}
    public double MeanChanceQuarantine {get; private set;}
    public double StdDevQuarantineRate {get; private set;}
    public int ExpectedSimulationTime {get; private set;}
    public double TravelChance {get; private set;}  
    
    /// <summary>
    /// Constructor to create a <see cref="Configuration"/> with given values
    /// </summary>
    /// <param name="inMeanPop"><see cref="int"/> representing Mean Population of a <see cref="Location"/></param>
    /// <param name="inStdDev"><see cref="double"/> representing Standard Deviation of the population of a <see cref="Location"/></param>
    /// <param name="inInfect"><see cref="double"/> representing % chance (as decimal) of infection</param>
    /// <param name="inDeath"><see cref="double "/> representing % chance (as decimal) of mortality if infected</param>
    /// <param name="inDiseaseTime"><see cref="int"/> representing length of infection (in hours)</param>
    /// <param name="inQuarantineTime"><see cref="int"/> representing length of quarantine (in hours)</param>
    /// <param name="inMeanQuar"><see cref="double"/> representing Mean % chance (as decimal) of Quarantining</param>
    /// <param name="inStdDevQuar"><see cref="double"/> representing Standard Deviation of % chance (as decimal) of Quarantining</param>
    /// <param name="inExpected"><see cref="int"/> representing expected length the simulation to run (in hours)</param>
    /// <param name="inTravel"><see cref="double"/> representing % chance (as decimal) of a <see cref="Person"/> to travel </param>
    public Configuration(int inMeanPop, double inStdDev, double inInfect, double inDeath, int inDiseaseTime, int inQuarantineTime, double inMeanQuar, double inStdDevQuar, int inExpected, double inTravel)
    {
        this.MeanPopulation = inMeanPop;
        this.StdDev = inStdDev;
        this.InfectChance = inInfect;
        this.DeathChance = inDeath;
        this.DiseaseLifetime = inDiseaseTime;
        this.QuarantineLifeTime = inQuarantineTime;
        this.MeanChanceQuarantine = inMeanQuar;
        this.StdDevQuarantineRate = inStdDevQuar;
        this.ExpectedSimulationTime = inExpected;
        this.TravelChance = inTravel;
    }  
}