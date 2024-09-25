using System;
using System.Diagnostics;
using SortClasses;
using BookClass;

namespace Proj2;

class Program
{
    static void Main(string[] args)
    {
        List<string> intFiles = new List<string>();
        List<string> bookFiles = new List<string>();
        List<string> allFiles = new List<string>();

        allFiles = LoadTestFiles(args[0]);

        foreach (string file in allFiles)
        {
            if (file.Contains("Ints"))
            {
                intFiles.Add(file);
            }
            else
            {
                bookFiles.Add(file);
            }
        }

        RunTests<int>(intFiles);

        RunTests<Book>(bookFiles);
    }

    public static void RunTests<T>(List<string> files) where T : IComparable<T>, IParsable<T>
    {
        foreach (string file in files)
        {
            RecursiveSort recSort = new RecursiveSort();
            IterSort iterSort = new IterSort();

            Console.WriteLine($"File Being Loaded: {file}");

            long iterTime = Timer<T>(iterSort, file);
            long iterAvg = iterTime / 5;
            Console.WriteLine($"Average Time Elapsed for Iterative Sorting\n{iterAvg}ms");

            long recurseTime = Timer<T>(recSort, file);
            long recurseAvg = recurseTime / 5;
            Console.WriteLine($"Average Time Elapsed for Recursive Sorting\n{recurseAvg}ms\n");
        }

    }

    public static long Timer<T>(ISort algo, string path) where T : IComparable<T>, IParsable<T>
    {
        List<T> values = new List<T>();

        long total = 0;

        for (int i = 0; i < 5; i++)
        {
            values = LoadData<T>(path);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            algo.Sort<T>(values);
            watch.Stop();

            total += watch.ElapsedMilliseconds;
        }

        return total;
    }

    public static List<string> LoadTestFiles(string filePath)
    {
        List<string> files = new();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    files.Add(line);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e} has message - {e.Message}");
                }
            }
        }

        return files;
    }

    public static List<T> LoadData<T>(string path) where T : IParsable<T>
    {
        List<T> values = new List<T>();

        using (StreamReader sr = new StreamReader(path))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                T item = default(T);
                if (T.TryParse(line, null, out item))
                {
                    values.Add(item);
                }
            }
        }

        return values;
    }
}
