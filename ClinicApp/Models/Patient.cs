using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private DateTime _dateOfBirth;
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

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateTime.Today || value.Year < 1900)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(DateOfBirth),
                    "Дата народження має бути не раніше 1900 року і не пізніше сьогодні."
                );
            }

            _dateOfBirth = value;
        }
    }

    public BloodType BloodType { get; set; }

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

    public string Email { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public int Age
    {
        get
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

    public bool IsAdult => Age >= 18;

    public Patient(
        string firstName,
        string lastName,
        DateTime dob,
        BloodType bloodType,
        string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        BloodType = bloodType;
        Phone = phone;
        Email = "";

        Id = _nextId++;
    }

    public Patient(string firstName, string lastName)
        : this(
            firstName,
            lastName,
            DateTime.Today.AddYears(-26),
            BloodType.Unknown,
            "0000000000")
    {
    }

    public Patient()
        : this("Невідомий", "Пацієнт")
    {
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "дитина";
        }

        if (Age < 60)
        {
            return "дорослий";
        }

        return "літній";
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | " +
               $"Вік: {ClinicFormatter.FormatAge(Age)} ({GetAgeCategory()}) | " +
               $"Кров: {ClinicFormatter.FormatBloodType(BloodType)} | " +
               $"Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }
}