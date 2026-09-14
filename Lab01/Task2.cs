using System;

public class Task2
{
    public static void Run()
    {
        double price = double.Parse(Console.ReadLine()!);
        
        int quantity = int.Parse(Console.ReadLine()!);
        
        int discount = int.Parse(Console.ReadLine()!);
        
        double total = price * quantity * (1 - discount / 100.0);
        
        Console.WriteLine($"Сума: {total:F2} грн");
    }
}