using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, -3, 4, 5, -6, 7, -8, 9 };

        int? firstPositive = numbers.FirstOrDefault(x => x > 0);
        int? lastNegative = numbers.LastOrDefault(x => x < 0);

        Console.WriteLine($"Перший додатній елемент: {firstPositive}");
        Console.WriteLine($"Останній від'ємний елемент: {lastNegative}");
    }
}