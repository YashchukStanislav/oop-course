using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxRecords = 1000;

    private MedicalRecord[] _records =
        new MedicalRecord[MaxRecords];

    private int _count = 0;

    public int Count => _count;

    public MedicalRecord? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _records[index];
        }
    }

    public void Add(MedicalRecord record)
    {
        if (_count >= MaxRecords)
        {
            Console.WriteLine(
                "Досягнуто ліміт кількості медичних записів."
            );

            return;
        }

        _records[_count] = record;
        _count++;

        Console.WriteLine(
            $"Запис [{record.Id}] " +
            $"{record.GetRecordType()} додано."
        );
    }

    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].Id == id)
            {
                return _records[i];
            }
        }

        return null;
    }

    public MedicalRecord[] GetByPatient(int patientId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                matchingCount++;
            }
        }

        MedicalRecord[] result =
            new MedicalRecord[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                result[index++] = _records[i];
            }
        }

        return result;
    }

    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                matchingCount++;
            }
        }

        MedicalRecord[] result =
            new MedicalRecord[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                result[index++] = _records[i];
            }
        }

        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Медичних записів немає.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_records[i]);
        }
    }

    public void DisplayList(MedicalRecord[] records)
    {
        if (records.Length == 0)
        {
            Console.WriteLine("Медичних записів не знайдено.");
            return;
        }

        for (int i = 0; i < records.Length; i++)
        {
            Console.WriteLine(records[i]);
        }
    }
}