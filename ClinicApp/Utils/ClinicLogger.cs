using System;
using System.IO;
using System.Text;
using ClinicApp.Events;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        LogInfo($"Додано пацієнта [{e.PatientId}] {e.FullName}.");
    }

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Створено запис ID {e.AppointmentId}: пацієнт {e.PatientId}, лікар {e.DoctorId}, {e.ScheduledAt:dd.MM.yyyy HH:mm}.");
    }

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Скасовано запис ID {e.AppointmentId} пацієнта {e.PatientId}. Причина: {e.Notes}");
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Завершено запис ID {e.AppointmentId} пацієнта {e.PatientId}.");
    }

    public void OnUrgentAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        string message = $"ТЕРМІНОВИЙ запис ID {e.AppointmentId}: пацієнт {e.PatientId}, лікар {e.DoctorId}, {e.ScheduledAt:dd.MM.yyyy HH:mm}. Примітка: {e.Notes}";
        LogWarning(message);

        Directory.CreateDirectory("alerts");
        string alertPath = Path.Combine("alerts", $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");
        File.AppendAllText(
            alertPath,
            $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}",
            Encoding.UTF8
        );
    }

    public void OnPaymentReceived(object? sender, PaymentEventArgs e)
    {
        LogInfo($"Отримано оплату за запис ID {e.AppointmentId}: {e.Amount:F2} грн.");
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        LogInfo($"Завершено план лікування ID {e.PlanId} пацієнта {e.PatientId}. Діагноз: {e.Diagnosis}");
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
        if (!File.Exists(_logPath) || n <= 0)
            return Array.Empty<string>();

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
            File.Delete(_logPath);
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }
}
