namespace MedicalAPI.Domain.DTOs.Pacient;

public record PatientResponse(string pacientId, string pacientNamem, string pacientEmail, string doctorId);