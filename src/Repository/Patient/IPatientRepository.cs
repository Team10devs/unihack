using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.Patient;

public interface IPatientRepository
{
    public Task<List<PatientModel>> GetAllPatients();
    public Task<PatientModel> GetByNameAsync(string patientName);
    public Task CreatePatientAsync(PatientModel patientModel);
}