using ClinicApp;

RunGrowableTest();

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient("Іван", "Петренко", DateTime.Today.AddYears(-41), "A+", "0501234567"));
clinic.Patients.Add(new Patient("Олена", "Коваль", DateTime.Today.AddYears(-33), "B-", "0672345678"));
clinic.Patients.Add(new Patient("Максим", "Бойко", DateTime.Today.AddYears(-16), "O+", "0933456789"));

clinic.Doctors.Add(new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567"));
clinic.Doctors.Add(new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678"));
clinic.Doctors.Add(new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789"));

clinic.Appointments.Book(1, 1, new DateTime(2026, 5, 9, 10, 0, 0), 30);
clinic.Appointments.Book(2, 2, new DateTime(2026, 5, 9, 11, 0, 0), 45);
clinic.Appointments.Book(3, 3, new DateTime(2026, 5, 10, 9, 0, 0), 20);

bool mainExit = false;
while (!mainExit)
{
    Console.WriteLine($"\n=== ГОЛОВНЕ МЕНЮ: {clinic.Name.ToUpper()} ===");
    Console.WriteLine("1. Керування пацієнтами");
    Console.WriteLine("2. Керування лікарями");
    Console.WriteLine("3. Керування записами");
    Console.WriteLine("4. Переглянути розклад на дату");
    Console.WriteLine("5. Згенерувати звіт клініки");
    Console.WriteLine("0. Вихід");
    Console.Write("Оберіть розділ: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            RunPatientMenu(clinic);
            break;
        case "2":
            RunDoctorMenu(clinic);
            break;
        case "3":
            RunAppointmentMenu(clinic);
            break;
        case "4":
            Console.Write("Введіть дату (рррр-мм-дд): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                clinic.DisplaySchedule(date);
            }
            else
            {
                Console.WriteLine("Невірний формат дати.");
            }
            break;
        case "5":
            clinic.GenerateReport();
            break;
        case "0":
            mainExit = true;
            break;
        default:
            Console.WriteLine("Невірний вибір.");
            break;
    }
}

void RunGrowableTest()
{
    Console.WriteLine("=== Тест GrowablePatientManager ===");
    Console.WriteLine("Додаємо пацієнтів одного за одним...");

    GrowablePatientManager growableManager = new GrowablePatientManager();

    for (int i = 1; i <= 20; i++)
    {
        growableManager.Add(new Patient($"ТестПацієнт{i}", $"Прізвище{i}"));
    }

    Console.WriteLine("\nТест пошуку:");
    Patient? found = growableManager.FindById(10);
    Console.WriteLine($"FindById(10) -> {(found != null ? found.FullName : "не знайдено")}");

    Patient? notFound = growableManager.FindById(99);
    Console.WriteLine($"FindById(99) -> {(notFound != null ? notFound.FullName : "не знайдено")}");

    Console.WriteLine("\nПорівняння:");
    Console.WriteLine($"PatientManager:          100 місць (фіксовано)");
    Console.WriteLine($"GrowablePatientManager:  {growableManager.Capacity} місця (зросте при потребі)\n");
}

void RunPatientMenu(Clinic clinic)
{
    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("\n--- Підменю: Пацієнти ---");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати пацієнта");
        Console.WriteLine("3. Знайти за ім'ям");
        Console.WriteLine("4. Видалити за ID");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Назад");
        Console.Write("Оберіть опцію: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                break;
            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine() ?? "";
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine() ?? "";
                clinic.Patients.Add(new Patient(firstName, lastName));
                break;
            case "3":
                Console.Write("Введіть ім'я або прізвище: ");
                string searchName = Console.ReadLine() ?? "";
                Patient[] found = clinic.Patients.FindByName(searchName);
                Console.WriteLine($"Знайдено ({found.Length}):");
                foreach (var p in found)
                {
                    Console.WriteLine(p);
                }
                break;
            case "4":
                Console.Write("Введіть ID для видалення: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (clinic.Patients.Remove(id))
                        Console.WriteLine("Пацієнта успішно видалено.");
                    else
                        Console.WriteLine("Пацієнта не знайдено.");
                }
                break;
            case "5":
                clinic.Patients.DisplayStats();
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}

void RunDoctorMenu(Clinic clinic)
{
    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("\n--- Підменю: Лікарі ---");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати лікаря");
        Console.WriteLine("3. Знайти за спеціальністю");
        Console.WriteLine("4. Видалити за ID");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Назад");
        Console.Write("Оберіть опцію: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                break;
            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine() ?? "";
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine() ?? "";
                Console.Write("Спеціальність: ");
                string speciality = Console.ReadLine() ?? "";
                clinic.Doctors.Add(new Doctor(firstName, lastName, speciality));
                break;
            case "3":
                Console.Write("Введіть спеціальність: ");
                string searchSpec = Console.ReadLine() ?? "";
                Doctor[] found = clinic.Doctors.FindBySpeciality(searchSpec);
                Console.WriteLine($"Знайдено ({found.Length}):");
                foreach (var d in found)
                {
                    Console.WriteLine(d);
                }
                break;
            case "4":
                Console.Write("Введіть ID для видалення: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (clinic.Doctors.Remove(id))
                        Console.WriteLine("Лікаря успішно видалено.");
                    else
                        Console.WriteLine("Лікаря не знайдено.");
                }
                break;
            case "5":
                clinic.Doctors.DisplayStats();
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}

void RunAppointmentMenu(Clinic clinic)
{
    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("\n--- Підменю: Записи ---");
        Console.WriteLine("1. Переглянути майбутні записи");
        Console.WriteLine("2. Записи пацієнта");
        Console.WriteLine("3. Створити запис");
        Console.WriteLine("4. Скасувати запис");
        Console.WriteLine("5. Завершити запис");
        Console.WriteLine("0. Назад");
        Console.Write("Оберіть опцію: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Майбутні записи:");
                clinic.Appointments.DisplayList(clinic.Appointments.GetUpcoming());
                break;
            case "2":
                Console.Write("Введіть ID пацієнта: ");
                if (int.TryParse(Console.ReadLine(), out int pId))
                {
                    Console.WriteLine($"Записи пацієнта #{pId}:");
                    clinic.Appointments.DisplayList(clinic.Appointments.GetByPatient(pId));
                }
                break;
            case "3":
                clinic.Patients.DisplayAll();
                clinic.Doctors.DisplayAll();

                Console.Write("ID пацієнта: ");
                int.TryParse(Console.ReadLine(), out int patientId);
                Console.Write("ID лікаря: ");
                int.TryParse(Console.ReadLine(), out int doctorId);

                clinic.Appointments.Book(patientId, doctorId, DateTime.Now.AddDays(1), 30);
                break;
            case "4":
                Console.Write("Введіть ID запису для скасування: ");
                if (int.TryParse(Console.ReadLine(), out int appIdToCancel))
                {
                    Console.Write("Причина скасування: ");
                    string reason = Console.ReadLine() ?? "";
                    clinic.Appointments.Cancel(appIdToCancel, reason);
                }
                break;
            case "5":
                Console.Write("Введіть ID запису для завершення: ");
                if (int.TryParse(Console.ReadLine(), out int appIdToComplete))
                {
                    clinic.Appointments.Complete(appIdToComplete);
                }
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}