using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.Patient;

public interface IPatientRepository
{
    public Task<List<PatientModel>> GetAllPatientsAsync();
    public Task<PatientModel?> GetPatientByIdAsync(string patientId);
    public Task<PatientModel?> GetPatientByEmailAsync(string email);
    public Task CreatePatientAsync(PatientModel patientModel);
}