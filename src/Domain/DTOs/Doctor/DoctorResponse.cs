using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Pacient;

namespace MedicalAPI.Domain.DTOs.Doctor;

using System;
using System.Collections.Generic;

public class DoctorResponse
{
    public string Email { get; set; }
    public string Fullname { get; set; }
    public List<AppointmentResponse> Appointments { get; set; }
    public List<PatientResponse> Pacients { get; set; }
    
    public DoctorResponse(string email, string fullname, List<AppointmentResponse> appointments, List<PatientResponse> pacients)
    {
        Email = email;
        Fullname = fullname;
        Appointments = appointments;
        Pacients = pacients;
    }
}

