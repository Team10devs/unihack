using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Doctor;

namespace MedicalAPI.Service.Firebase.Doctor;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<DoctorModel>> GetAllAsync()
    {
        return await _doctorRepository.GetAllAsync();
    }

    public async Task<DoctorModel> GetDoctorByEmailAsync(string email)
    {
        try
        {
            var doctor = await _doctorRepository.GetDoctorByEmailAsync(email);
            return doctor;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<DoctorModel> GetByIdAsync(string id)
    {
        try
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            
            return doctor;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}