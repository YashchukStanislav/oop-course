using ClinicApp;

Patient p1 = new Patient("Іван", "Петренко", DateTime.Today.AddYears(-41), "A+", "0501234567");
Patient p2 = new Patient("Олена", "Коваль", DateTime.Today.AddYears(-33), "B-", "0672345678");
Patient p3 = new Patient("Максим", "Бойко", DateTime.Today.AddYears(-16), "O+", "0933456789");
Patient p4 = new Patient("Невідомий", "Пацієнт");
Patient p5 = new Patient();

Console.WriteLine(p1);
Console.WriteLine(p2);
Console.WriteLine(p3);
Console.WriteLine(p4);
Console.WriteLine(p5);