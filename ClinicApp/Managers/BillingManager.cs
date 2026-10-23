using System;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointmentManager;

    public BillingManager(AppointmentManager appointmentManager)
    {
        _appointmentManager = appointmentManager;
    }

    public IPayable[] GetAllUnpaid()
    {
        Appointment[] appointments = _appointmentManager.GetAll();
        return FilterUnpaid(appointments);
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        Appointment[] appointments =
            _appointmentManager.GetByPatient(patientId);

        return FilterUnpaid(appointments);
    }

    public decimal GetTotalDebt()
    {
        IPayable[] unpaid = GetAllUnpaid();
        decimal total = 0m;

        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }

        return total;
    }

    public decimal GetPatientDebt(int patientId)
    {
        IPayable[] unpaid = GetUnpaidByPatient(patientId);
        decimal total = 0m;

        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }

        return total;
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment[] appointments = _appointmentManager.GetAll();

        for (int i = 0; i < appointments.Length; i++)
        {
            Appointment appointment = appointments[i];

            if (appointment.Id == appointmentId)
            {
                if (appointment.IsPaid || appointment.IsCancelled)
                {
                    return false;
                }

                appointment.MarkPaid();
                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] is Appointment appointment)
            {
                Console.WriteLine(
                    $"{appointment} | Сума: {items[i].GetCost():F2} грн"
                );
            }
            else
            {
                Console.WriteLine(
                    $"[{i + 1}] Сума: {items[i].GetCost():F2} грн"
                );
            }
        }
    }

    private IPayable[] FilterUnpaid(Appointment[] appointments)
    {
        int count = 0;

        for (int i = 0; i < appointments.Length; i++)
        {
            if (!appointments[i].IsPaid &&
                !appointments[i].IsCancelled)
            {
                count++;
            }
        }

        IPayable[] result = new IPayable[count];
        int index = 0;

        for (int i = 0; i < appointments.Length; i++)
        {
            if (!appointments[i].IsPaid &&
                !appointments[i].IsCancelled)
            {
                result[index++] = appointments[i];
            }
        }

        return result;
    }
}