using System;

public class Task1
{
    public static void Run()
    {
        double weight = double.Parse(Console.ReadLine()!);
        
        double height = double.Parse(Console.ReadLine()!);
        
        double imt = weight / (height * height);
        
        Console.WriteLine($"IMT: {imt:F2}");
    }
}