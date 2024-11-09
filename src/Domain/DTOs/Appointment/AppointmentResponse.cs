using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;

namespace MedicalAPI.Domain.DTOs.Appointment;

public record AppointmentResponse(string appointmentId,string patientId, string doctorId, DateTime StartDate, DateTime EndDate);