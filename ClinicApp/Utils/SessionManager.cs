
using System;
using System.IO;
using System.Text;
using System.Globalization;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Utils;

public class SessionManager
{
    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        Patient[] patients = clinic.Patients.GetAll();

        using StreamWriter writer = new StreamWriter(
            _sessionPath, false, Encoding.UTF8
        );

        writer.WriteLine("[PATIENTS]");

        for (int i = 0; i < patients.Length; i++)
        {
            Patient p = patients[i];

            writer.WriteLine(
                $"{p.FirstName},{p.LastName}," +
                $"{p.DateOfBirth:dd.MM.yyyy}," +
                $"{p.BloodType},{p.Phone}"
            );
        }

        writer.WriteLine("[END]");
    }

    public int Load(Clinic clinic)
    {
        if (!Exists())
        {
            return 0;
        }

        string[] lines = File.ReadAllLines(
            _sessionPath, Encoding.UTF8
        );

        int loaded = 0;
        bool inPatients = false;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith("["))
            {
                inPatients = line == "[PATIENTS]";
                continue;
            }

            if (!inPatients)
            {
                continue;
            }

            try
            {
                string[] parts = line.Split(',');

                if (parts.Length != 5)
                {
                    continue;
                }

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();

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

                Patient[] existing = clinic.Patients.GetAll();
                bool duplicate = false;

                for (int j = 0; j < existing.Length; j++)
                {
                    if (existing[j].FirstName == firstName &&
                        existing[j].LastName == lastName &&
                        existing[j].DateOfBirth == dateOfBirth)
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (duplicate)
                {
                    continue;
                }

                Patient patient = new Patient(
                    firstName,
                    lastName,
                    dateOfBirth,
                    bloodType,
                    phone
                );

                clinic.Patients.Add(patient);
                loaded++;
            }
            catch (Exception)
            {
                continue;
            }
        }

        return loaded;
    }
}
