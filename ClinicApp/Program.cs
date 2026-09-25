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

Console.WriteLine("\n=== Лікарі ===");

Doctor doc1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567");
doc1.WorkEndHour = 16; 

Doctor doc2 = new Doctor("Наталія", "Мороз", "Неврологія");
doc2.LicenseNumber = "LIC-002";
doc2.Phone = "0442345678";
doc2.WorkStartHour = 9;
doc2.WorkEndHour = 18;

Doctor doc3 = new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789");

Doctor doc4 = new Doctor();

Console.WriteLine(doc1);
Console.WriteLine(doc2);
Console.WriteLine(doc3);
Console.WriteLine(doc4);