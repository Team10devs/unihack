using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Patient;

namespace MedicalAPI.Domain.DTOs.Doctor;

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

