using System;
using System.IO;
using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _directory;

    public PatientPassportWriter(Clinic clinic, string directory = "patients")
    {
        _clinic = clinic;
        _directory = directory;
        Directory.CreateDirectory(_directory);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    private void WritePassport(int patientId)
    {
        Patient? patient = _clinic.Patients.FindById(patientId);

        if (patient == null)
        {
            return;
        }

        StringBuilder content = new StringBuilder();

        content.AppendLine("========================================");
        content.AppendLine("          МЕДИЧНИЙ ПАСПОРТ");
        content.AppendLine("========================================");
        content.AppendLine();

        content.AppendLine("1. ОСОБИСТІ ДАНІ");
        content.AppendLine($"ID: {patient.Id}");
        content.AppendLine($"ПІБ: {patient.FullName}");
        content.AppendLine($"Дата народження: {patient.DateOfBirth:dd.MM.yyyy}");
        content.AppendLine($"Вік: {patient.Age}");
        content.AppendLine($"Група крові: {ClinicFormatter.FormatBloodType(patient.BloodType)}");
        content.AppendLine($"Телефон: {patient.Phone}");
        content.AppendLine();

        content.AppendLine("2. МЕДИЧНІ ЗАПИСИ");
        content.AppendLine("Діагнози:");
        Diagnosis[] diagnoses = _clinic.MedicalRecords.GetDiagnoses(patientId);
        AppendRecords(content, diagnoses);

        content.AppendLine("Аналізи:");
        LabResult[] labResults = _clinic.MedicalRecords.GetLabResults(patientId);
        AppendRecords(content, labResults);

        content.AppendLine("Рецепти:");
        Prescription[] prescriptions = _clinic.MedicalRecords.GetPrescriptions(patientId);
        AppendRecords(content, prescriptions);
        content.AppendLine();

        content.AppendLine("3. ПРИЙОМИ");
        Appointment[] appointments = _clinic.Appointments.GetByPatient(patientId);
        if (appointments.Length == 0)
        {
            content.AppendLine("Немає записів.");
        }
        else
        {
            for (int i = 0; i < appointments.Length; i++)
            {
                content.AppendLine(appointments[i].ToString());
            }
        }
        content.AppendLine();

        content.AppendLine("4. ПЛАНИ ЛІКУВАННЯ");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetByPatient(patientId);
        if (plans.Length == 0)
        {
            content.AppendLine("Немає планів лікування.");
        }
        else
        {
            for (int i = 0; i < plans.Length; i++)
            {
                content.AppendLine(
                    $"[{plans[i].Id}] {plans[i].Diagnosis} | " +
                    $"Тривалість: {plans[i].DurationDays} дн. | " +
                    $"Статус: {plans[i].Status}"
                );
            }
        }
        content.AppendLine();

        content.AppendLine("5. ФІНАНСОВА ІНФОРМАЦІЯ");
        content.AppendLine($"Поточний борг: {_clinic.Billing.GetPatientDebt(patientId):F2} грн");
        content.AppendLine();

        content.AppendLine("========================================");
        content.AppendLine($"Дата та час формування: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");

        string filePath = Path.Combine(_directory, $"passport_{patientId}.txt");
        File.WriteAllText(filePath, content.ToString(), Encoding.UTF8);
    }

    private static void AppendRecords<T>(StringBuilder content, T[] records)
    {
        if (records.Length == 0)
        {
            content.AppendLine("  Немає записів.");
            return;
        }

        for (int i = 0; i < records.Length; i++)
        {
            content.AppendLine($"  {records[i]}");
        }
    }
}
