using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.User;

public interface IUserRepository
{
    Task SaveDeviceTokenAsync(string userId, string token);
    Task<object> GetUserByEmailAsync(string email);
    Task<PatientModel> GetPatientByEmailAsync(string email);
    Task<DoctorModel> GetDoctorByEmailAsync(string email);
    
}