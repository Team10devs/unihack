using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Entities.Prescription;

namespace MedicalAPI.Service.Firebase.Prescription;

public interface IPrescriptionService
{
    Task<PrescriptionModel> GeneratePrescription(PrescriptionRequest prescription);
    Task<IEnumerable<PrescriptionModel>> GetPatientPrescriptions(string patientId);
    Task<PrescriptionModel> GetById(string prescriptionId);
}