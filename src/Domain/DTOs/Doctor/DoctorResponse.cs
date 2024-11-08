namespace MedicalAPI.Domain.DTOs.Doctor;

public record DoctorResponse(string email, string fullname , string license , string address , string specialization);