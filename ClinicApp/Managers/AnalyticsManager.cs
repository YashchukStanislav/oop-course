
using System;
using System.Collections.Generic;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private AppointmentManager _appointments;
    private DoctorManager _doctors;
    private PatientManager _patients;

    public AnalyticsManager(
        AppointmentManager appointments,
        DoctorManager doctors,
        PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Doctor[] doctors = _doctors.GetAll();
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < doctors.Length; i++)
        {
            int appointmentCount = 0;
            decimal totalRevenue = 0;
            DateTime lastAppointmentDate = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].DoctorId == doctors[i].Id)
                {
                    appointmentCount++;
                    totalRevenue += appointments[j].GetCost();

                    if (appointments[j].ScheduledAt > lastAppointmentDate)
                    {
                        lastAppointmentDate = appointments[j].ScheduledAt;
                    }
                }
            }

            yield return new DoctorStats(
                doctors[i].Id,
                doctors[i].FullName,
                appointmentCount,
                totalRevenue,
                lastAppointmentDate
            );
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Patient[] patients = _patients.GetAll();
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            int visitCount = 0;
            decimal totalSpent = 0;
            DateTime lastVisitDate = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].PatientId == patients[i].Id)
                {
                    visitCount++;
                    totalSpent += appointments[j].GetCost();

                    if (appointments[j].ScheduledAt > lastVisitDate)
                    {
                        lastVisitDate = appointments[j].ScheduledAt;
                    }
                }
            }

            yield return new PatientStats(
                patients[i].Id,
                patients[i].FullName,
                visitCount,
                totalSpent,
                lastVisitDate
            );
        }
    }
}
