using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    public void LogInfo(string message)
    {
        Write("INFO", message);
    }

    public void LogWarning(string message)
    {
        Write("WARN", message);
    }

    public void LogError(string message)
    {
        Write("ERROR", message);
    }

    private void Write(string level, string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level,-5}] {message}{Environment.NewLine}";

        File.AppendAllText(_logPath, logEntry, Encoding.UTF8);
    }

    public string[] GetLastLines(int n)
    {
        if (!File.Exists(_logPath))
        {
            return Array.Empty<string>();
        }

        if (n <= 0)
        {
            return Array.Empty<string>();
        }

        string[] lines = File.ReadAllLines(_logPath, Encoding.UTF8);

        int startIndex = Math.Max(0, lines.Length - n);
        int count = lines.Length - startIndex;

        string[] result = new string[count];

        Array.Copy(lines, startIndex, result, 0, count);

        return result;
    }

    public void Clear()
    {
        if (File.Exists(_logPath))
        {
            File.Delete(_logPath);
        }
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }
}