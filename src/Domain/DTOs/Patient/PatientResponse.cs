using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Doctor;

namespace MedicalAPI.Domain.DTOs.Patient;

public record PatientResponse(string pacientId, string pacientNamem, string pacientEmail, string gender, DateTime birthDate, string medicalHistory, List<AppointmentResponse>? Appointments, string? doctorId);