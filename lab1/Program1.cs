using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int numElements = 100000;

        TestCollectionPerformance(new LinkedList<int>(), numElements, "LinkedList");
        TestCollectionPerformance(new List<int>(), numElements, "List (ArrayList)");
        TestCollectionPerformance(new SortedSet<int>(), numElements, "SortedSet (TreeSet)");
        TestCollectionPerformance(new HashSet<int>(), numElements, "HashSet");
    }

    static void TestCollectionPerformance(ICollection<int> collection, int numElements, string collectionName)
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < numElements; i++)
        {
            collection.Add(i);
        }
        stopwatch.Stop();
        Console.WriteLine($"{collectionName} Add: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");

        stopwatch.Restart();
        collection.Contains(numElements / 2);
        stopwatch.Stop();
        Console.WriteLine($"{collectionName} Search: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");

        stopwatch.Restart();
        collection.Clear();
        stopwatch.Stop();
        Console.WriteLine($"{collectionName} Clear: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");
    }
}