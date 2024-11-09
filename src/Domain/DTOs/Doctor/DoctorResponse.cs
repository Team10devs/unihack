using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Patient;

namespace MedicalAPI.Domain.DTOs.Doctor;

public class DoctorResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public List<AppointmentResponse> Appointments { get; set; } = new List<AppointmentResponse>();
    public List<PatientResponse> Pacients { get; set; } = new List<PatientResponse>();
    
    public DoctorResponse(string id,string email, string fullname, List<AppointmentResponse> appointments, List<PatientResponse> pacients)
    {
        Id = id;
        Email = email;
        Fullname = fullname;
        Appointments = appointments;
        Pacients = pacients;
    }
}

