using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Domain.DTOs.Patient;

public record PatientRequest(string email, string password,
    string fullname, DateTime birthDate,
    string? gender,string? doctorCode);