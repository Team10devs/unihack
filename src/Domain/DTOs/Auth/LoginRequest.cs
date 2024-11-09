namespace MedicalAPI.Domain.DTOs.Auth;

public record LoginRequest(string email, string password);