using ClinicApp;

RunWorkScheduleTest();

RunClinicFormatterTest();

RunGrowableTest();

Clinic clinic = new Clinic("Медична Клініка");

Patient patient1 = new Patient("Іван", "Петренко", DateTime.Today.AddYears(-41), BloodType.APositive, "0501234567");

Patient patient2 = new Patient("Олена", "Коваль", DateTime.Today.AddYears(-33), BloodType.BNegative, "0672345678");

Patient patient3 = new Patient("Максим", "Бойко", DateTime.Today.AddYears(-16), BloodType.OPositive, "0933456789");

clinic.Patients.Add(patient1);

clinic.Patients.Add(patient2);

clinic.Patients.Add(patient3);

Doctor doctor1 = new Doctor("Олег", "Сидоренко", Speciality.Cardiology, "LIC-001", "0441234567");

doctor1.Schedule = new WorkSchedule(8, 16);

Doctor doctor2 = new Doctor("Наталія", "Мороз", Speciality.Neurology, "LIC-002", "0442345678");

doctor2.Schedule = new WorkSchedule(9, 17);

Doctor doctor3 = new Doctor("Андрій", "Власенко", Speciality.Pediatrics, "LIC-003", "0443456789");

doctor3.Schedule = new WorkSchedule(10, 18);

clinic.Doctors.Add(doctor1);

clinic.Doctors.Add(doctor2);

clinic.Doctors.Add(doctor3);

clinic.Appointments.Book(patient1.Id, doctor1.Id, new DateTime(2026, 5, 9, 10, 0, 0), 30);

clinic.Appointments.Book(patient2.Id, doctor2.Id, new DateTime(2026, 5, 9, 11, 0, 0), 45);

clinic.Appointments.Book(patient3.Id, doctor3.Id, new DateTime(2026, 5, 10, 9, 0, 0), 20);

RunIndexerTest(clinic);

RunTask04Test(clinic);

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

void RunTask04Test(Clinic clinic)
{
    Console.WriteLine("\n=== Тест Task 04 ===");

    Console.WriteLine("\n--- Перевантаження FindBySpeciality ---");

    Doctor[] cardiologists =
        clinic.Doctors.FindBySpeciality(Speciality.Cardiology);

    Console.WriteLine($"Кардіологів знайдено: {cardiologists.Length}");

    foreach (Doctor cardiologist in cardiologists)
    {
        Console.WriteLine(cardiologist);
    }

    Doctor[] foundByString =
        clinic.Doctors.FindBySpeciality("Neuro");

    Console.WriteLine(
        $"\nЗа рядком \"Neuro\" знайдено: {foundByString.Length}"
    );

    foreach (Doctor foundDoctor in foundByString)
    {
        Console.WriteLine(foundDoctor);
    }

    Console.WriteLine("\n--- Перевантаження GetByDate ---");

    Appointment[] appointments =
        clinic.Appointments.GetByDate(2026, 5, 9);

    Console.WriteLine(
        $"Записів на 09.05.2026: {appointments.Length}"
    );

    clinic.Appointments.DisplayList(appointments);

    Console.WriteLine("\n--- TryFindById Patient ---");

    Patient? firstPatient = clinic.Patients[0];

    if (firstPatient != null &&
        clinic.Patients.TryFindById(
            firstPatient.Id,
            out Patient foundPatient))
    {
        Console.WriteLine(
            $"Пацієнта знайдено: {foundPatient.FullName}"
        );
    }
    else
    {
        Console.WriteLine("Пацієнта не знайдено.");
    }

    Console.WriteLine("\n--- TryFindById Doctor ---");

    Doctor? firstDoctor = clinic.Doctors[0];

    if (firstDoctor != null &&
        clinic.Doctors.TryFindById(
            firstDoctor.Id,
            out Doctor foundDoctorById))
    {
        Console.WriteLine(
            $"Лікаря знайдено: {foundDoctorById.FullName}"
        );
    }
    else
    {
        Console.WriteLine("Лікаря не знайдено.");
    }

    Console.WriteLine("\n--- FindByBloodType ---");

    Patient[] bloodPatients =
        clinic.Patients.FindByBloodType(
            BloodType.APositive
        );

    Console.WriteLine(
        $"Пацієнтів з групою крові A+: {bloodPatients.Length}"
    );

    foreach (Patient bloodPatient in bloodPatients)
    {
        Console.WriteLine(bloodPatient);
    }

    Console.WriteLine("\n--- Оператори ?. та ?? ---");

    string patientName =
        clinic.Patients.FindById(999)?.FullName
        ?? "не знайдено";

    Console.WriteLine(
        $"Пацієнт з ID 999: {patientName}"
    );

    string doctorName =
        clinic.Doctors.FindById(999)?.FullName
        ?? "не знайдено";

    Console.WriteLine(
        $"Лікар з ID 999: {doctorName}"
    );

    Console.WriteLine();
}

void RunClinicFormatterTest()

{

    Console.WriteLine("=== Тест ClinicFormatter ===");

    Console.WriteLine($"Група крові: {ClinicFormatter.FormatBloodType(BloodType.APositive)}");

    Console.WriteLine($"Спеціальність: {ClinicFormatter.FormatSpeciality(Speciality.Cardiology)}");

    Console.WriteLine($"Вік 1: {ClinicFormatter.FormatAge(1)}");

    Console.WriteLine($"Вік 3: {ClinicFormatter.FormatAge(3)}");

    Console.WriteLine($"Вік 11: {ClinicFormatter.FormatAge(11)}");

    Console.WriteLine($"Вік 21: {ClinicFormatter.FormatAge(21)}");

    Console.WriteLine($"Телефон: {ClinicFormatter.FormatPhone("0501234567")}");

    Console.WriteLine();

}

void RunIndexerTest(Clinic clinic)

{

    Console.WriteLine("\n=== Тест індексаторів ===");

    Patient? firstPatient = clinic.Patients[0];

    Doctor? secondDoctor = clinic.Doctors[1];

    Appointment? firstAppointment = clinic.Appointments[0];

    Console.WriteLine($"Пацієнт [0]: {(firstPatient != null ? firstPatient.ToString() : "null")}");

    Console.WriteLine($"Лікар [1]: {(secondDoctor != null ? secondDoctor.ToString() : "null")}");

    if (firstAppointment != null)

    {

        Console.Write("Запис [0]: ");

        clinic.Appointments.DisplayAppointment(firstAppointment);

    }

    else

    {

        Console.WriteLine("Запис [0]: null");

    }

    Console.WriteLine($"Некоректний індекс пацієнта [999]: {(clinic.Patients[999] == null ? "null" : "знайдено")}");

    Console.WriteLine();

}

void RunWorkScheduleTest()

{

    Console.WriteLine("=== Тест WorkSchedule ===");

    WorkSchedule morning = new WorkSchedule(8, 16);

    WorkSchedule evening = new WorkSchedule(14, 22);

    Console.WriteLine($"Ранкова зміна: {morning}");

    Console.WriteLine($"Вечірня зміна: {evening}");

    Console.WriteLine($"Годин у ранковій зміні: {morning.HoursPerDay}");

    Console.WriteLine($"Display: {morning.Display}");

    Console.WriteLine($"10:00 входить у ранкову зміну: {morning.Contains(10)}");

    Console.WriteLine($"18:00 входить у ранкову зміну: {morning.Contains(18)}");

    Console.WriteLine($"Лікар зараз на зміні: {morning.IsNow}");

    Console.WriteLine("\n=== Value type ===");

    WorkSchedule copy = morning;

    Console.WriteLine($"Оригінал: {morning}");

    Console.WriteLine($"Копія:    {copy}");

    Console.WriteLine();

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

                Console.Write("Спеціальність (General, Cardiology, Neurology, Pediatrics): ");

                string specInput = Console.ReadLine() ?? "";

                if (!Enum.TryParse<Speciality>(specInput, true, out var speciality))

                {

                    speciality = Speciality.General;

                }

                Doctor newDoctor = new Doctor(firstName, lastName, speciality);

                Console.Write("Початок робочого дня (година): ");

                if (int.TryParse(Console.ReadLine(), out int startHour))

                {

                    Console.Write("Кінець робочого дня (година): ");

                    if (int.TryParse(Console.ReadLine(), out int endHour))

                    {

                        newDoctor.Schedule = new WorkSchedule(startHour, endHour);

                    }

                }

                clinic.Doctors.Add(newDoctor);

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
