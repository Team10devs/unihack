using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.Doctor;

public interface IDoctorRepository
{
    Task AddDoctorAsync(DoctorModel doctor);
    Task<DoctorModel> GetDoctorByIdAsync(string doctorId);
    Task<DoctorModel> GetDoctorByEmailAsync(string email);
    Task<DoctorModel> GetDoctorByCodeAsync(string code);
    Task<IEnumerable<DoctorModel>> GetAllAsync();
    Task UpdateDoctorAsync(DoctorModel doctor);
}