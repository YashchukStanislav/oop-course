using System;

public class Task4
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        int m = int.Parse(Console.ReadLine()!);

        int[,] matrix = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = int.Parse(parts[j]);
            }
        }

        int max = matrix[0, 0];
        int maxRow = 1;
        int maxCol = 1;

        for (int i = 0; i < n; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < m; j++)
            {
                int val = matrix[i, j];
                rowSum += val;

                if (val > max)
                {
                    max = val;
                    maxRow = i + 1;
                    maxCol = j + 1;
                }
            }
            Console.WriteLine($"Лікар {i + 1}: {rowSum} прийомів");
        }

        int[] colSums = new int[m];
        for (int j = 0; j < m; j++)
        {
            int colSum = 0;
            for (int i = 0; i < n; i++)
            {
                colSum += matrix[i, j];
            }
            colSums[j] = colSum;
        }

        Console.WriteLine($"По днях: {string.Join(", ", colSums)}");
        Console.WriteLine($"Максимум: {max} (Лікар {maxRow}, День {maxCol})");
    }
}