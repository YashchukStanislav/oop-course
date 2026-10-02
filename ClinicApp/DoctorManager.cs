namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count => _count;

    public Doctor? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _doctors[index];
        }
    }

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Досягнуто ліміт кількості лікарів.");
            return;
        }

        _doctors[_count] = doctor;
        _count++;

        Console.WriteLine(
            $"Лікаря [{doctor.Id}] {doctor.FullName} додано."
        );
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }

        return null;
    }

    public bool TryFindById(int id, out Doctor doctor)
    {
        Doctor? found = FindById(id);

        if (found != null)
        {
            doctor = found;
            return true;
        }

        doctor = null!;
        return false;
    }

    public Doctor[] FindBySpeciality(string speciality)
    {
        string search = speciality.ToLower();
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i]
                .Speciality
                .ToString()
                .ToLower()
                .Contains(search))
            {
                matchingCount++;
            }
        }

        Doctor[] result = new Doctor[matchingCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i]
                .Speciality
                .ToString()
                .ToLower()
                .Contains(search))
            {
                result[index++] = _doctors[i];
            }
        }

        return result;
    }

    public Doctor[] FindBySpeciality(Speciality speciality)
    {
        int matchingCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                matchingCount++;
            }
        }

        Doctor[] result = new Doctor[matchingCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                result[index++] = _doctors[i];
            }
        }

        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] copy = new Doctor[_count];

        Array.Copy(
            _doctors,
            copy,
            _count
        );

        return copy;
    }

    public bool Remove(int id)
    {
        int indexToRemove = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1)
        {
            return false;
        }

        for (int i = indexToRemove; i < _count - 1; i++)
        {
            _doctors[i] = _doctors[i + 1];
        }

        _doctors[_count - 1] = null!;
        _count--;

        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Порожній список");
            return;
        }

        Console.WriteLine(
            $"=== Лікарі ({_count} / {MaxDoctors}) ==="
        );

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i]);
        }

        Console.WriteLine(new string('=', 40));
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine(
                "Немає даних для статистики."
            );

            return;
        }

        int availableCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine(
            "=== Статистика лікарів ==="
        );

        Console.WriteLine(
            $"Всього:           {_count}"
        );

        Console.WriteLine(
            $"Доступні зараз:   {availableCount}"
        );

        Console.WriteLine(
            "По спеціальностях:"
        );

        for (int i = 0; i < _count; i++)
        {
            bool alreadyProcessed = false;

            for (int j = 0; j < i; j++)
            {
                if (_doctors[j].Speciality ==
                    _doctors[i].Speciality)
                {
                    alreadyProcessed = true;
                    break;
                }
            }

            if (!alreadyProcessed)
            {
                int specCount = 0;

                for (int k = 0; k < _count; k++)
                {
                    if (_doctors[k].Speciality ==
                        _doctors[i].Speciality)
                    {
                        specCount++;
                    }
                }

                Console.WriteLine(
                    $"  {_doctors[i].Speciality}: {specCount}"
                );
            }
        }

        Console.WriteLine(
            new string('=', 30)
        );
    }
}