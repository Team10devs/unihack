using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Patient;

namespace MedicalAPI.Service.Firebase.Patient;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<IEnumerable<PatientModel>> GetPatientsByDoctorIdAsync(string doctorId)
    {
        var allPatients = await patientRepository.GetAllPatientsAsync();

        var doctorPatients = allPatients.Where(p => p.Doctor.Id == doctorId);

        if (!doctorPatients.Any())
            throw new Exception($"No patients found for doctor with id{doctorId}");

        return doctorPatients;
    }

    public async Task<PatientModel> GetPatientByEmailAsync(string email)
    {
        var patient = await patientRepository.GetPatientByEmailAsync(email);

        if (patient is null)
            throw new Exception($"Patient with email{email} does not exist.");

        return patient;
    }

    public async Task<PatientModel> GetPatientByIdAsync(string id)
    {
        var patient = await patientRepository.GetPacientByIdAsync(id);

        if (patient is null)
            throw new Exception($"Patient with id {id} does not exist.");

        return patient;
    }
}