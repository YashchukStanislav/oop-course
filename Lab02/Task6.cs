using System;
using System.Globalization;

public class Task6
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        
        int[][] costs = new int[n][];
        
        int maxIncome = -1;
        int bestDoctor = -1;

        for (int i = 0; i < n; i++)
        {
            int k = int.Parse(Console.ReadLine()!);
            
            costs[i] = new int[k];
            
            int sum = 0;
            
            for (int j = 0; j < k; j++)
            {
                costs[i][j] = int.Parse(Console.ReadLine()!);
                sum += costs[i][j];
            }
            
            double avg = (double)sum / k;
            
            Console.WriteLine($"Лікар {i + 1}: {k} прийоми, сума={sum} грн, середня={avg.ToString("F2", CultureInfo.InvariantCulture)} грн");
            
            if (sum > maxIncome)
            {
                maxIncome = sum;
                bestDoctor = i + 1;
            }
        }
        
        Console.WriteLine($"Найбільший дохід: Лікар {bestDoctor} ({maxIncome} грн)");
    }
}