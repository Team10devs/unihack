namespace MedicalAPI.Domain.DTOs.Patient;

public record PatientResponse(string pacientId, string pacientNamem, string pacientEmail, string doctorId);