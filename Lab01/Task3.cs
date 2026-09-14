using System;

public class Task3
{
    public static void Run()
    {
        int birthYear = int.Parse(Console.ReadLine()!);
        
        int age = 2026 - birthYear;
        
        Console.WriteLine($"Вік: {age} р.");
        
        if (age <= 17)
        {
            Console.WriteLine("Категорія: дитина");
        }
        else if (age <= 59)
        {
            Console.WriteLine("Категорія: дорослий");
        }
        else
        {
            Console.WriteLine("Категорія: пенсіонер");
        }
    }
}