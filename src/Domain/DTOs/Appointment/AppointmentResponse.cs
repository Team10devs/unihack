using MedicalAPI.Domain.DTOs.Pacient;

namespace MedicalAPI.Domain.DTOs.Appointment;

public record AppointmentResponse(PatientResponse PatientResponse, DateTime Date, TimeSpan Duration);