using System;
using System.IO;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionEventTracker
{
    private readonly Clinic _clinic;

    public int PatientsAdded { get; private set; }
    public int AppointmentsBooked { get; private set; }
    public int UrgentBooked { get; private set; }
    public int AppointmentsCancelled { get; private set; }
    public int AppointmentsCompleted { get; private set; }
    public int PaymentsReceived { get; private set; }
    public int PlansCompleted { get; private set; }

    public SessionEventTracker(Clinic clinic)
    {
        _clinic = clinic;
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        PatientsAdded++;
    }

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        AppointmentsBooked++;
    }

    public void OnUrgentAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        UrgentBooked++;
    }

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCancelled++;

        if (!_clinic.WaitingRoom.IsEmpty)
        {
            Patient next = _clinic.WaitingRoom.Peek();
            Console.WriteLine($"[ЧЕРГА] Слот звільнився. Наступний: {next.FullName}");
        }
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCompleted++;
    }

    public void OnPaymentReceived(object? sender, PaymentEventArgs e)
    {
        PaymentsReceived++;
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        PlansCompleted++;
    }

    private string BuildSummary()
    {
        return
            "=== ПІДСУМКИ СЕСІЇ КЛІНІКИ ===" + Environment.NewLine +
            $"Додано пацієнтів: {PatientsAdded}" + Environment.NewLine +
            $"Створено записів: {AppointmentsBooked}" + Environment.NewLine +
            $"Термінових записів: {UrgentBooked}" + Environment.NewLine +
            $"Скасовано записів: {AppointmentsCancelled}" + Environment.NewLine +
            $"Завершено прийомів: {AppointmentsCompleted}" + Environment.NewLine +
            $"Отримано оплат: {PaymentsReceived}" + Environment.NewLine +
            $"Завершено планів лікування: {PlansCompleted}";
    }

    public void PrintSummary()
    {
        Console.WriteLine();
        Console.WriteLine(BuildSummary());
    }

    public void SaveSummary(string path = "session_summary.txt")
    {
        string summary = BuildSummary() +
            Environment.NewLine +
            $"Дата та час формування: {DateTime.Now:dd.MM.yyyy HH:mm:ss}";

        File.WriteAllText(path, summary);
        Console.WriteLine($"Підсумок сесії збережено у файлі: {path}");
    }
}
