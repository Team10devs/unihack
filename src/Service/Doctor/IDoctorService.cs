using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Service.Firebase.Doctor;

public interface IDoctorService
{
    Task<IEnumerable<DoctorModel>> GetAllAsync();
    Task<DoctorModel> GetDoctorByEmailAsync(string email);
    Task<DoctorModel> GetByIdAsync(string id);
}