
using System;
using System.IO;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Interfaces;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    private string GetReportPath(string fileName)
    {
        string dir = Path.Combine(
            _baseDir,
            DateTime.Now.ToString("yyyy-MM-dd")
        );

        Directory.CreateDirectory(dir);
        return Path.Combine(dir, fileName);
    }

    public string ExportPatients()
    {
        string path = GetReportPath("patients.txt");
        Patient[] patients = _clinic.Patients.GetAll();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        writer.WriteLine("=== Пацієнти — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < patients.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {patients[i]}");
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього: {patients.Length}");

        return path;
    }

    public string ExportAppointments()
    {
        string path = GetReportPath("appointments.txt");
        Appointment[] appointments = _clinic.Appointments.GetAll();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        writer.WriteLine("=== Записи на прийом — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < appointments.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {appointments[i]}");
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього: {appointments.Length}");

        return path;
    }

    public string ExportBilling()
    {
        string path = GetReportPath("billing.txt");
        IPayable[] unpaid = _clinic.Billing.GetAllUnpaid();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        writer.WriteLine("=== Неоплачені записи — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < unpaid.Length; i++)
        {
            writer.WriteLine(
                $"{i + 1,3}. {unpaid[i]} | Сума: {unpaid[i].GetCost():F2} грн"
            );
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього неоплачених: {unpaid.Length}");
        writer.WriteLine($"Загальний борг: {_clinic.Billing.GetTotalDebt():F2} грн");

        return path;
    }

    public string ExportTreatmentPlans()
    {
        string path = GetReportPath("treatment_plans.txt");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        writer.WriteLine("=== Плани лікування — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < plans.Length; i++)
        {
            writer.WriteLine(
                $"{i + 1,3}. ID: {plans[i].Id} | " +
                $"Пацієнт: {plans[i].PatientId} | " +
                $"Діагноз: {plans[i].Diagnosis} | " +
                $"Тривалість: {plans[i].DurationDays} днів | " +
                $"Статус: {plans[i].Status}"
            );
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього: {plans.Length}");

        return path;
    }

    public string[] ExportAll()
    {
        string[] paths = new string[4];

        paths[0] = ExportPatients();
        paths[1] = ExportAppointments();
        paths[2] = ExportBilling();
        paths[3] = ExportTreatmentPlans();

        return paths;
    }
}
