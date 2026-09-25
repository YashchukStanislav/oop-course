using ClinicApp;

PatientManager patientManager = new PatientManager();

patientManager.Add(new Patient("Іван", "Петренко", DateTime.Today.AddYears(-41), "A+", "0501234567"));
patientManager.Add(new Patient("Олена", "Коваль", DateTime.Today.AddYears(-33), "B-", "0672345678"));
patientManager.Add(new Patient("Максим", "Бойко", DateTime.Today.AddYears(-16), "O+", "0933456789"));
patientManager.Add(new Patient("Марія", "Ткач"));

RunPatientMenu(patientManager);

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
        Console.WriteLine("0. Вихід");
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