using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.Doctor;

public interface IDoctorRepository
{
    public Task<List<DoctorModel>> GetAllDoctors();
    public Task<DoctorModel> GetByNameAsync(string doctorName);
    public Task CreateDoctorAsync(DoctorModel doctorModel);
}