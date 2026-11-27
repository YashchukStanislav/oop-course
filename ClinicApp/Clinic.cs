using System;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

public class Clinic
{
    public string Name { get; }

    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }
    public AnalyticsManager Analytics { get; }
    public MedicalRecordManager MedicalRecords { get; }
    public BillingManager Billing { get; }
    public WaitingQueue<Patient> WaitingRoom { get; }

    public TreatmentPlanManager TreatmentPlans { get; }
    public ClinicLogger Logger { get; }
    public PatientPassportWriter Passport { get; }
    public ClinicExporter Exporter { get; }
    public SessionManager Session { get; }

    public Clinic(string name)
    {
        Name = name;

        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);

        Analytics = new AnalyticsManager(
            Appointments,
            Doctors,
            Patients
        );

        Billing = new BillingManager(Appointments);
        MedicalRecords = new MedicalRecordManager();
        WaitingRoom = new WaitingQueue<Patient>();

        TreatmentPlans = new TreatmentPlanManager();
        Logger = new ClinicLogger();
        Passport = new PatientPassportWriter(this);
        Exporter = new ClinicExporter(this);
        Session = new SessionManager();

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Patients.PatientAdded += Logger.OnPatientAdded;
        Patients.PatientAdded += Passport.OnPatientAdded;

        Appointments.AppointmentBooked += Logger.OnAppointmentBooked;
        Appointments.AppointmentCancelled += Logger.OnAppointmentCancelled;
        Appointments.AppointmentCompleted += Logger.OnAppointmentCompleted;
        Appointments.AppointmentCompleted += Passport.OnAppointmentCompleted;
        Appointments.UrgentAppointmentBooked += Logger.OnUrgentAppointmentBooked;

        Billing.PaymentReceived += Logger.OnPaymentReceived;
        TreatmentPlans.PlanCompleted += Logger.OnPlanCompleted;
        TreatmentPlans.PlanCompleted += Passport.OnPlanCompleted;
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"\n=== Розклад на {date:dd.MM.yyyy} ===");

        Appointment[] appsOnDate =
            Appointments.GetByDate(date);

        Appointments.DisplayList(appsOnDate);
    }

    public void GenerateReport()
    {
        Console.WriteLine(
            "╔════════════════════════════════════════════════════════╗"
        );

        Console.WriteLine(
            $"║ Звіт — {Name,-47} ║"
        );

        Console.WriteLine(
            "╠════════════════════════════════════════════════════════╣"
        );

        Console.WriteLine(
            $"║ Пацієнтів:          {Patients.Count,-34} ║"
        );

        Console.WriteLine(
            $"║ Лікарів:            {Doctors.Count,-34} ║"
        );

        Appointment[] upcoming =
            Appointments.GetUpcoming();

        Console.WriteLine(
            $"║ Майбутніх записів:  {upcoming.Length,-34} ║"
        );

        Console.WriteLine(
            "╠════════════════════════════════════════════════════════╣"
        );

        Console.WriteLine(
            "║ Навантаження лікарів (майбутні записи):                ║"
        );

        Doctor[] allDoctors =
            Doctors.GetAll();

        for (int i = 0; i < allDoctors.Length; i++)
        {
            int loadCount = 0;

            for (int j = 0; j < upcoming.Length; j++)
            {
                if (upcoming[j].DoctorId ==
                    allDoctors[i].Id)
                {
                    loadCount++;
                }
            }

            string docLine =
                $"  {allDoctors[i].FullName} " +
                $"({allDoctors[i].Speciality}): " +
                $"{loadCount} записів";

            Console.WriteLine(
                $"║ {docLine,-54} ║"
            );
        }

        Console.WriteLine(
            "╚════════════════════════════════════════════════════════╝"
        );
    }
}
