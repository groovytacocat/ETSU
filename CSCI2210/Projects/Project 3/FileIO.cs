using System.Net;

namespace Project_3;

public class FileIO : IFileIO
{
    /// <summary>
    /// Method to write the final report upon simulation completion.
    /// Uses a <see cref="StreamWriter"/> to format and output both the questions and calculated answers to a file.
    /// Catches any <see cref="Exception"/> thrown and outputs message to console.
    /// </summary>
    /// <param name="filePath"><see cref="string"/> representing the file path to the output file</param>
    /// <param name="simTime"><see cref="int"/> representing total runtime of the simulation in hours</param>
    /// <param name="totalInfect"><see cref="int"/> representing total number of people infected</param>
    /// <param name="totalDead"><see cref="int"/> representing total number of people who died</param>
    /// <param name="percentInfect"><see cref="double"/> representing % of people infected</param>
    /// <param name="percentDead"><see cref="double"/> representing % of people dead</param>
    /// <param name="avgSpread"><see cref="double"/> representing average spread of infection</param>
    /// <param name="maxSpread"><see cref="int"/> representing greatest number of infections by a single individual</param>
    /// <param name="places"><see cref="Location"/> graph of simulation world</param>
    public void GenerateReport(string filePath, int simTime, int totalInfect, int totalDead, double percentInfect, double percentDead, double avgSpread, int maxSpread, List<Location> places)
    {
        Stats simStats = new Stats();
        using(StreamWriter sw = new StreamWriter(filePath))
        {
            try
            {
                sw.WriteLine($"How long did the simulation run?\n\t{simTime} hours");
                sw.WriteLine($"How many people were infected over the course of the simulation?\n\t{totalInfect} people");
                sw.WriteLine($"How many people died over the course of the simulation?\n\t{totalDead} people");
                sw.WriteLine($"What percentage of people were infected at the end of the simulation?\n\t{percentInfect} % of people");    
                sw.WriteLine($"What percent of people were dead at the end of the simulation?\n\t{percentDead} % of people");
                sw.WriteLine($"What percentage of people were infected on average?\n\t{percentInfect} % of people");                      
                sw.WriteLine($"What was the average number of people an infected person spread the disease to\n\t{avgSpread}");
                sw.WriteLine($"What was the maximum number of people an infected person spread the disease to\n\t{maxSpread}");

                foreach(Location city in places)
                {
                    double[] averageStats = simStats.AvgStats(city);
                    sw.WriteLine($"City: {city.id} Stats:");
                    sw.WriteLine($"\tAvg Population: {averageStats[0]}");
                    sw.WriteLine($"\tAvg Infected: {averageStats[1]}");
                    sw.WriteLine($"\tAvg Quarantine: {averageStats[2]}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Caught\n\tMessage: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Method to create hourly csv log and supply headers
    /// Catches any <see cref="Exception"/> thrown and outputs to console
    /// </summary>
    /// <param name="filePath"><see cref="string"/> representing output file path</param>
    public void LogEvents(string filePath)
    {
        using(StreamWriter sw = new StreamWriter(filePath))
        {
            try
            {
                sw.WriteLine("Most Infected,Greatest Spreader,Number Alive,Number Dead,Currently Infected,Number Quarantined");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Caught\n\tMessage: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Method to log hourly report stastistics to csv file
    /// </summary>
    /// <param name="filePath"><see cref="string"/> representing file path of csv output file</param>
    /// <param name="data"><see cref="string"/> array of data to be written to file</param>
    public void LogEvents(string filePath, string[] data)
    {
        using(StreamWriter sw = new StreamWriter(filePath, true))
        {
            try
            {
                for(int i = 0; i < data.Length - 1; i++)
                {
                    sw.Write($"{data[i]},");
                }
                sw.WriteLine($"{data[^1]}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception Caught\n\tMessage: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Method to create a <see cref="Configuration"/> List after reading in values from a csv to create 1+ <see cref="Configuration"/>s
    /// Catches <see cref="Exception"/>s thrown and outputs to console
    /// </summary>
    /// <param name="filePath"><see cref="string"/> of input file</param>
    /// <returns><see cref="Configuration"/> List containing the configurations of a csv file</returns>
    public List<Configuration> ReadFile(string filePath)
    {
        List<Configuration> configs = new();
        using(StreamReader sr = new StreamReader(filePath))
        {
            string line;

            while((line = sr.ReadLine()) != null)
            {
                if(line.StartsWith("#"))
                {
                    continue;
                }

                try
                {
                    string[] configVals = line.Trim().Split(',');
                    Configuration config = new Configuration(int.Parse(configVals[0]), double.Parse(configVals[1]), 
                        double.Parse(configVals[2]), double.Parse(configVals[3]), int.Parse(configVals[4]), int.Parse(configVals[5]),
                        double.Parse(configVals[6]), double.Parse(configVals[7]), int.Parse(configVals[8]), double.Parse(configVals[9]));
                    configs.Add(config);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e}\n\tMessage: {e.Message}");
                }
            }
        }

        return configs;
    }
}
