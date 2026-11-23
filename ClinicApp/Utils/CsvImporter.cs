
using System;
using System.IO;
using System.Text;
using System.Globalization;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Utils;

public class CsvImporter
{
    public ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (!File.Exists(filePath))
        {
            result.AddError(0, "Файл не знайдено.");
            return result;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            try
            {
                string[] parts = lines[i].Split(',');

                if (parts.Length != 5)
                {
                    throw new FormatException(
                        $"Рядок має {parts.Length} полів замість 5."
                    );
                }

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();
                
                if (string.IsNullOrWhiteSpace(firstName))
                {
                throw new ArgumentException("Ім'я не може бути порожнім.");
                }

                DateTime dateOfBirth = DateTime.ParseExact(
                    parts[2].Trim(),
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture
                );

                BloodType bloodType = (BloodType)Enum.Parse(
                    typeof(BloodType),
                    parts[3].Trim()
                );

                string phone = parts[4].Trim();

                Patient patient = new Patient(
                    firstName,
                    lastName,
                    dateOfBirth,
                    bloodType,
                    phone
                );

                clinic.Patients.Add(patient);
                result.AddSuccess();
            }
            catch (Exception e)
            {
                result.AddError(i + 1, e.Message);
            }
        }

        return result;
    }
}
