using System;
using System.Diagnostics;
using SortClasses;

namespace Proj2;

class Program
{
    static void Main(string[] args)
    {
        List<int> test = new();
        List<string> files = new();

        files = LoadTestFiles(args[0]);

        foreach(string s in files)
        {
            RecursiveSort recSort = new RecursiveSort();
            IterSort iterSort = new IterSort();

            Console.WriteLine($"File Being Loaded: {s}");
            long iterTime = 0;
            long recurseTime = 0;

            for(int i = 0; i < 5; i++)
            {
                Stopwatch watch = new Stopwatch();
                test = LoadIntegerTestData($"TestData/Ints/{s}");
                watch.Start();
                iterSort.Sort<int>(test);
                watch.Stop();

                iterTime += watch.ElapsedMilliseconds;
            }

            long iterAvg = iterTime / 5;

            Console.WriteLine($"Average Time Elapsed for Iterative Sorting\n{iterAvg}ms");

            for(int i = 0; i < 5; i++)
            {
                test = LoadIntegerTestData($"TestData/Ints/{s}");
                Stopwatch watch = new Stopwatch();
                watch.Start();
                recSort.QuickSort<int>(test, 0, test.Count - 1);
                watch.Stop();

                recurseTime += watch.ElapsedMilliseconds;
            }

            long recurseAvg = recurseTime / 5;

            Console.WriteLine($"Average Time Elapsed for Recursive Sorting\n{recurseAvg}ms\n");
        }
    }

    public static List<int> LoadIntegerTestData(string filePath)
    {
        List<int> vals = new List<int>();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    vals.Add(int.Parse(line.Trim()));
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e} had message: {e.Message}");
                }
            }
        }

        return vals;
    }

    public static List<string> LoadTestFiles(string filePath)
    {
        List<string> files = new();

        using(StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                try{
                    files.Add(line);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e} has message - {e.Message}");
                }
            }
        }

        return files;
    }
}
