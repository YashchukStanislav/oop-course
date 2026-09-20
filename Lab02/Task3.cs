using System;

public class Task3
{
    public static void Run()
    {
        string[] days = { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
        int[] counts = new int[7];

        for (int i = 0; i < 7; i++)
        {
            counts[i] = int.Parse(Console.ReadLine()!);
        }

        int sum = 0;
        int maxIdx = 0;
        int minIdx = 0;

        for (int i = 0; i < 7; i++)
        {
            sum += counts[i];
            
            if (counts[i] > counts[maxIdx]) maxIdx = i;
            if (counts[i] < counts[minIdx]) minIdx = i;
        }

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"{days[i],-10} : {counts[i]} пацієнтів");
        }

        Console.WriteLine($"Разом:      {sum}");
        Console.WriteLine($"Найбільше:  {days[maxIdx]} ({counts[maxIdx]})");
        Console.WriteLine($"Найменше:   {days[minIdx]} ({counts[minIdx]})");
    }
}