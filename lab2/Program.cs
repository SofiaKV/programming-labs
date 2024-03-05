using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ConsoleApplication4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, double> dictionary = new Dictionary<string, double> {
                { "item1", 45.50 },
                { "item2", 35 },
                { "item3", 41.30 },
                { "item4", 55 },
                { "item5", 24 }
            };

            var sorted = dictionary.OrderByDescending(pair => pair.Value);
            var topThree = sorted.Take(3);

            foreach (var pair in topThree)
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }

            var json = JsonSerializer.Serialize(topThree.ToDictionary(pair => pair.Key, pair => pair.Value));
            File.WriteAllText("result.json", json);
        }
    }
}