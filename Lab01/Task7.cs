using System;
using System.Globalization;

public class Task7
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        decimal[] costs = new decimal[n];

        for (int i = 0; i < n; i++)
        {
            costs[i] = decimal.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
        }

        decimal total = 0;
        decimal min = costs[0];
        decimal max = costs[0];

        foreach (decimal cost in costs)
        {
            total += cost;
            if (cost < min) min = cost;
            if (cost > max) max = cost;
        }

        decimal average = total / n;

        int aboveAverageCount = 0;
        for (int i = 0; i < n; i++)
        {
            if (costs[i] > average)
            {
                aboveAverageCount++;
            }
        }

        int firstHighIndex = -1;
        int index = 0;

        while (index < n)
        {
            if (costs[index] > 1000)
            {
                firstHighIndex = index;
                break;
            }
            index++;
        }

        string firstHighText = firstHighIndex != -1
            ? $"#{firstHighIndex + 1} - {costs[firstHighIndex].ToString("F2", CultureInfo.InvariantCulture)} грн"
            : "немає";

        Console.WriteLine("=== Звіт по прийомах ===");
        Console.WriteLine($"{"Кількість:",-20}{n}");
        Console.WriteLine($"{"Загальна сума:",-20}{total.ToString("F2", CultureInfo.InvariantCulture)} грн");
        Console.WriteLine($"{"Середня:",-20}{average.ToString("F2", CultureInfo.InvariantCulture)} грн");
        Console.WriteLine($"{"Мін / Макс:",-20}{min.ToString("F2", CultureInfo.InvariantCulture)} / {max.ToString("F2", CultureInfo.InvariantCulture)} грн");
        Console.WriteLine($"{"Вище середнього:",-20}{aboveAverageCount} з {n}");
        Console.WriteLine($"{"Перший > 1000:",-20}{firstHighText}");
        Console.WriteLine("======================");
    }
}