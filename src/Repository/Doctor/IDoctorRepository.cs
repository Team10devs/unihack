using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.Doctor;

public interface IDoctorRepository
{
    Task AddDoctorAsync(DoctorModel doctor);
    Task<DoctorModel> GetDoctorByIdAsync(string doctorId);
}