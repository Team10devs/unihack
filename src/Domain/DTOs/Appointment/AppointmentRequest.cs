namespace MedicalAPI.Domain.DTOs.Appointment;

public record AppointmentRequest(string patientId, string doctorId,
    DateTime start, DateTime end, string notes);