using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.DTOs.Appointment;

public record AppointmentResponse(string appointmentId,string patientId, string doctorId,
    DateTime StartDate, DateTime EndDate,AppointmentStatus AppointmentStatus);