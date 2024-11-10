using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Service.Firebase.Patient;

public interface IPatientService
{
    Task<IEnumerable<PatientModel>> GetPatientsByDoctorIdAsync(string doctorId);
    Task<PatientModel> GetPatientByEmailAsync(string email);
    Task<PatientModel> GetPatientByIdAsync(string id);
}