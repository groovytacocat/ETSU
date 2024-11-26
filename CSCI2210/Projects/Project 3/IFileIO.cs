namespace Project_3;

public interface IFileIO
{
    public List<Configuration> ReadFile(string filePath);
    public void GenerateReport(string filePath, int simTime, int totalInfect, int totalDead, double percentInfect, double percentDead, double avgSpread, int maxSpread, List<Location> places);
    public void LogEvents(string filePath);
    public void LogEvents(string filePath, string[] data);
}
