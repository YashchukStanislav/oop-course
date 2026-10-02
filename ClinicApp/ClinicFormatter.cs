namespace ClinicApp;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bt)
    {
        return bt switch
        {
            BloodType.APositive => "A+",
            BloodType.ANegative => "A-",
            BloodType.BPositive => "B+",
            BloodType.BNegative => "B-",
            BloodType.ABPositive => "AB+",
            BloodType.ABNegative => "AB-",
            BloodType.OPositive => "O+",
            BloodType.ONegative => "O-",
            BloodType.Unknown => "Невідомо",
            _ => "Невідомо"
        };
    }

    public static string FormatSpeciality(Speciality speciality)
    {
        return speciality switch
        {
            Speciality.General => "Терапевт",
            Speciality.Cardiology => "Кардіологія",
            Speciality.Neurology => "Неврологія",
            Speciality.Pediatrics => "Педіатрія",
            Speciality.Surgery => "Хірургія",
            Speciality.Orthopedics => "Ортопедія",
            Speciality.Dermatology => "Дерматологія",
            Speciality.Emergency => "Швидка допомога",
            _ => speciality.ToString()
        };
    }

    public static string FormatAge(int age)
    {
        int lastTwoDigits = age % 100;
        int lastDigit = age % 10;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
        {
            return $"{age} років";
        }

        return lastDigit switch
        {
            1 => $"{age} рік",
            2 or 3 or 4 => $"{age} роки",
            _ => $"{age} років"
        };
    }

    public static string FormatPhone(string phone)
    {
        if (phone.Length != 10)
        {
            return phone;
        }

        foreach (char c in phone)
        {
            if (!char.IsDigit(c))
            {
                return phone;
            }
        }

        return $"({phone.Substring(0, 3)}) " +
               $"{phone.Substring(3, 3)}-" +
               $"{phone.Substring(6, 4)}";
    }
}