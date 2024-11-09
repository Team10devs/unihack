using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Repository.Patient;

public interface IPatientRepository
{
    Task AddPatientAsync(PatientModel patient);
    Task AddPatientToDoctorAsync(string doctorName, PatientModel patient);
    Task<PatientModel> GetPacientByIdAsync(string patientId);
}