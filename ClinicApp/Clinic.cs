namespace ClinicApp;

public class Clinic
{
    public string Name { get; }
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"=== Розклад на {date:dd.MM.yyyy} ===");
        Appointments.DisplayList(Appointments.GetByDate(date));
    }

    public void GenerateReport()
    {
        Appointment[] upcoming = Appointments.GetUpcoming();
        Doctor[] allDoctors = Doctors.GetAll();

        Console.WriteLine("╔═════════════════════════════════════════════════╗");
        Console.WriteLine($"║ Звіт — {Name,-40} ║");
        Console.WriteLine("╠═════════════════════════════════════════════════╣");
        Console.WriteLine($"║ Пацієнтів:          {Patients.Count,-27} ║");
        Console.WriteLine($"║ Лікарів:            {Doctors.Count,-27} ║");
        Console.WriteLine($"║ Майбутніх записів:  {upcoming.Length,-27} ║");
        Console.WriteLine("╠═════════════════════════════════════════════════╣");
        Console.WriteLine("║ Навантаження лікарів (майбутні записи):         ║");

        foreach (var doc in allDoctors)
        {
            int load = 0;
            for (int i = 0; i < upcoming.Length; i++)
            {
                if (upcoming[i].DoctorId == doc.Id)
                {
                    load++;
                }
            }

            string docInfo = $"{doc.FullName} ({doc.Speciality}): {load} записів";
            Console.WriteLine($"║   {docInfo,-45} ║");
        }

        Console.WriteLine("╚═════════════════════════════════════════════════╝");
    }
}