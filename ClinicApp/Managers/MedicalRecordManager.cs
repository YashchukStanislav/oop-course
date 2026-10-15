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
            $"Запис [{record.Id}] {record.GetRecordType()} додано."
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

    public Diagnosis[] GetDiagnoses(int patientId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis)
            {
                matchingCount++;
            }
        }

        Diagnosis[] result =
            new Diagnosis[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis diagnosis)
            {
                result[index++] = diagnosis;
            }
        }

        return result;
    }

    public LabResult[] GetLabResults(int patientId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is LabResult)
            {
                matchingCount++;
            }
        }

        LabResult[] result =
            new LabResult[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is LabResult labResult)
            {
                result[index++] = labResult;
            }
        }

        return result;
    }

    public Prescription[] GetPrescriptions(int patientId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription)
            {
                matchingCount++;
            }
        }

        Prescription[] result =
            new Prescription[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId != patientId)
            {
                continue;
            }

            Prescription? prescription =
                _records[i] as Prescription;

            if (prescription != null)
            {
                result[index++] = prescription;
            }
        }

        return result;
    }

    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis diagnosis &&
                diagnosis.IsChronic)
            {
                matchingCount++;
            }
        }

        Diagnosis[] result =
            new Diagnosis[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis diagnosis &&
                diagnosis.IsChronic)
            {
                result[index++] = diagnosis;
            }
        }

        return result;
    }

    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription prescription &&
                prescription.IsActive())
            {
                matchingCount++;
            }
        }

        Prescription[] result =
            new Prescription[matchingCount];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription prescription &&
                prescription.IsActive())
            {
                result[index++] = prescription;
            }
        }

        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        MedicalRecord[] patientRecords =
            GetByPatient(patientId);

        if (patientRecords.Length == 0)
        {
            Console.WriteLine(
                $"Для пацієнта #{patientId} медичних записів немає."
            );

            return;
        }

        int diagnosisCount = 0;
        int labResultCount = 0;
        int prescriptionCount = 0;

        for (int i = 0; i < patientRecords.Length; i++)
        {
            if (patientRecords[i] is Diagnosis)
            {
                diagnosisCount++;
            }
            else if (patientRecords[i] is LabResult)
            {
                labResultCount++;
            }
            else if (patientRecords[i] is Prescription)
            {
                prescriptionCount++;
            }
        }

        Console.WriteLine(
            $"=== Медична картка пацієнта #{patientId} ==="
        );

        Console.WriteLine(
            $"Всього записів: {patientRecords.Length} " +
            $"(діагнозів: {diagnosisCount}, " +
            $"аналізів: {labResultCount}, " +
            $"рецептів: {prescriptionCount})"
        );

        Diagnosis[] chronicDiagnoses =
            GetChronicDiagnoses(patientId);

        if (chronicDiagnoses.Length > 0)
        {
            Console.WriteLine(
                $"Хронічні діагнози ({chronicDiagnoses.Length}):"
            );

            for (int i = 0; i < chronicDiagnoses.Length; i++)
            {
                Console.WriteLine(chronicDiagnoses[i]);
            }
        }

        Prescription[] activePrescriptions =
            GetActivePrescriptions(patientId);

        if (activePrescriptions.Length > 0)
        {
            Console.WriteLine(
                $"Активні рецепти ({activePrescriptions.Length}):"
            );

            for (int i = 0; i < activePrescriptions.Length; i++)
            {
                Console.WriteLine(activePrescriptions[i]);
            }
        }
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
            Console.WriteLine(
                "Медичних записів не знайдено."
            );

            return;
        }

        for (int i = 0; i < records.Length; i++)
        {
            Console.WriteLine(records[i]);
        }
    }
}