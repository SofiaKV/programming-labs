using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DynamicArray
{
    [JsonInclude]
    public int[] items;
    private int count;
    private Dictionary<int, int> duplicates;

    public int Count => count;
    public Dictionary<int, int> Duplicates => duplicates;

    public DynamicArray(int initialCapacity)
    {
        items = new int[initialCapacity];
        count = 0;
    }

    public DynamicArray()
    {
        items = Array.Empty<int>();
        count = 0;
        duplicates = new Dictionary<int, int>();
    }

    public void SaveToJsonFile(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, jsonString);
    }

    public static DynamicArray LoadFromJsonFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        var options = new JsonSerializerOptions { IncludeFields = true };
        DynamicArray dynamicArray = JsonSerializer.Deserialize<DynamicArray>(jsonString, options);
        dynamicArray.CalculateDuplicates(); 
        return dynamicArray;
    }

    public void FillWithRandomNumbers()
    {
        Random rnd = new Random();
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = rnd.Next(1, 101);
            count++;
        }
        CalculateDuplicates(); 
    }

    public void Shuffle()
    {
        Random rnd = new Random();
        for (int i = count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            int temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
        CalculateDuplicates();
    }

    public void CalculateDuplicates()
    {
        duplicates = items.GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .ToDictionary(x => x.Key, x => x.Count());
    }

    public void PrintArray()
    {
        for (int i = 0; i < count; i++)
        {
            Console.Write(items[i] + (i < count - 1 ? ", " : ""));
        }
        Console.WriteLine();
    }

    public bool IsValid()
    {
        return count > 0;
    }
}

class Program
{
    static void Main()
    {
        DynamicArray myArray = new DynamicArray(10);
        myArray.FillWithRandomNumbers();
        myArray.PrintArray(); 

        string filePath = "dynamicArray.json";

        myArray.SaveToJsonFile(filePath);
 
        DynamicArray loadedArray = DynamicArray.LoadFromJsonFile(filePath);
        loadedArray.PrintArray(); 
       
        PrintDuplicatesIfExists(loadedArray);
    }

         private static void PrintDuplicatesIfExists(DynamicArray array)
    {
        if (array.Duplicates.Any())
        {
            Console.WriteLine("Знайдено повторювані елементи:");
            foreach (var pair in array.Duplicates)
            {
                Console.WriteLine($"Елемент {pair.Key} повторюється {pair.Value} разів");
            }
        }
        else
        {
            Console.WriteLine("Дублікатів не знайдено.");
        }
    }
}