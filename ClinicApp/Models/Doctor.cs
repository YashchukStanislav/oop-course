using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private string _licenseNumber = "";
    private string _phone = "";

    public int Id { get; }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException(
                    "Ім'я не може бути порожнім і не може бути довшим за 50 символів.",
                    nameof(FirstName)
                );
            }

            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException(
                    "Прізвище не може бути порожнім і не може бути довшим за 50 символів.",
                    nameof(LastName)
                );
            }

            _lastName = value;
        }
    }

    public Speciality Speciality { get; set; }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Номер ліцензії не може бути порожнім.",
                    nameof(LicenseNumber)
                );
            }

            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            if (value.Length != 10)
            {
                throw new ArgumentException(
                    "Телефон повинен містити рівно 10 цифр.",
                    nameof(Phone)
                );
            }

            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    throw new ArgumentException(
                        "Телефон повинен містити лише цифри.",
                        nameof(Phone)
                    );
                }
            }

            _phone = value;
        }
    }

    public WorkSchedule Schedule { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public int WorkingHoursPerDay => Schedule.HoursPerDay;

    public string WorkSchedule => Schedule.Display;

    public bool IsAvailableNow => Schedule.IsNow;

    public Doctor(
        string firstName,
        string lastName,
        Speciality speciality,
        string licenseNumber,
        string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;

        Schedule = new WorkSchedule(8, 17);

        Id = _nextId++;
    }

    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "Невідомо", "0000000000")
    {
    }

    public Doctor()
        : this("Невідомий", "Лікар", Speciality.General)
    {
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }

    public override string ToString()
    {
        string status = IsAvailableNow
            ? "доступний"
            : "не в робочий час";

        return $"[{Id}] {FullName} | " +
               $"{ClinicFormatter.FormatSpeciality(Speciality)} | " +
               $"{LicenseNumber} | " +
               $"Тел: {ClinicFormatter.FormatPhone(Phone)} | " +
               $"{Schedule} - {status}";
    }
}