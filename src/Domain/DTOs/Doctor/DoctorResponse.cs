using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Patient;

namespace MedicalAPI.Domain.DTOs.Doctor;

public class DoctorResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Code { get; set; }
    public List<AppointmentResponse> Appointments { get; set; } = new List<AppointmentResponse>();
    public List<PatientResponse> Pacients { get; set; } = new List<PatientResponse>();
    
    public DoctorResponse(string id,string email, string fullname, string code, List<AppointmentResponse> appointments, List<PatientResponse> pacients)
    {
        Id = id;
        Email = email;
        Fullname = fullname;
        Code = code;
        Appointments = appointments;
        Pacients = pacients;
    }
}

