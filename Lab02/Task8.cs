using System;

public class Task8
{
    public static void Run()
    {
        int d = int.Parse(Console.ReadLine()!);
        int w = int.Parse(Console.ReadLine()!);

        int[,,] data = new int[d, w, 2];

        for (int i = 0; i < d; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    data[i, j, k] = int.Parse(Console.ReadLine()!);
                }
            }
        }

        int[] deptTotals = new int[d];

        for (int i = 0; i < d; i++)
        {
            Console.WriteLine($"Відділення {i + 1}:");
            
            for (int j = 0; j < w; j++)
            {
                int morning = data[i, j, 0];
                int evening = data[i, j, 1];
                int weekTotal = morning + evening;
                
                deptTotals[i] += weekTotal;
                
                Console.WriteLine($"    Тиждень {j + 1}: ранок {morning}, вечір {evening} -> разом {weekTotal}");
            }
            
            Console.WriteLine($"    Разом: {deptTotals[i]} пацієнтів");
        }

        int maxDeptIdx = 0;
        for (int i = 1; i < d; i++)
        {
            if (deptTotals[i] > deptTotals[maxDeptIdx])
            {
                maxDeptIdx = i;
            }
        }

        Console.WriteLine($"Найзавантаженіше: Відділення {maxDeptIdx + 1} ({deptTotals[maxDeptIdx]} пацієнтів)");
    }
}