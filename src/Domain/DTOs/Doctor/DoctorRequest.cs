namespace MedicalAPI.Domain.DTOs.Doctor;

public record DoctorRequest(string email,string password,
    string fullname , string license ,
    string address , string specialization);