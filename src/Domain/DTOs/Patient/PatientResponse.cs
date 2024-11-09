using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Doctor;

namespace MedicalAPI.Domain.DTOs.Patient;

public record PatientResponse(string pacientId, string pacientNamem, string pacientEmail, List<AppointmentResponse>? Appointments, string? doctorId);