using ClinicApp;

PatientManager patientManager = new PatientManager();
DoctorManager doctorManager = new DoctorManager();

patientManager.Add(new Patient("Іван", "Петренко", DateTime.Today.AddYears(-41), "A+", "0501234567"));
patientManager.Add(new Patient("Олена", "Коваль", DateTime.Today.AddYears(-33), "B-", "0672345678"));
patientManager.Add(new Patient("Максим", "Бойко", DateTime.Today.AddYears(-16), "O+", "0933456789"));
patientManager.Add(new Patient("Марія", "Ткач"));

Doctor doc1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567");
doc1.WorkEndHour = 16;
doctorManager.Add(doc1);

Doctor doc2 = new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678");
doc2.WorkStartHour = 9;
doc2.WorkEndHour = 18;
doctorManager.Add(doc2);

Doctor doc3 = new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789");
doctorManager.Add(doc3);

bool mainExit = false;
while (!mainExit)
{
    Console.WriteLine("\n=== МЕНЕДЖЕР КЛІНІКИ ===");
    Console.WriteLine("1. Керування пацієнтами");
    Console.WriteLine("2. Керування лікарями");
    Console.WriteLine("0. Вихід");
    Console.Write("Оберіть розділ: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            RunPatientMenu(patientManager);
            break;
        case "2":
            RunDoctorMenu(doctorManager);
            break;
        case "0":
            mainExit = true;
            break;
        default:
            Console.WriteLine("Невірний вибір.");
            break;
    }
}

void RunPatientMenu(PatientManager manager)
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
                manager.DisplayAll();
                break;
            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine() ?? "";
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine() ?? "";
                manager.Add(new Patient(firstName, lastName));
                break;
            case "3":
                Console.Write("Введіть ім'я або прізвище для пошуку: ");
                string searchName = Console.ReadLine() ?? "";
                Patient[] found = manager.FindByName(searchName);
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
                    if (manager.Remove(id))
                        Console.WriteLine("Пацієнта успішно видалено.");
                    else
                        Console.WriteLine("Пацієнта з таким ID не знайдено.");
                }
                break;
            case "5":
                manager.DisplayStats();
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

void RunDoctorMenu(DoctorManager manager)
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
                manager.DisplayAll();
                break;
            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine() ?? "";
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine() ?? "";
                Console.Write("Спеціальність: ");
                string speciality = Console.ReadLine() ?? "";
                manager.Add(new Doctor(firstName, lastName, speciality));
                break;
            case "3":
                Console.Write("Введіть спеціальність для пошуку: ");
                string searchSpec = Console.ReadLine() ?? "";
                Doctor[] found = manager.FindBySpeciality(searchSpec);
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
                    if (manager.Remove(id))
                        Console.WriteLine("Лікаря успішно видалено.");
                    else
                        Console.WriteLine("Лікаря з таким ID не знайдено.");
                }
                break;
            case "5":
                manager.DisplayStats();
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