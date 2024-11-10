using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Entities.Medicine;

namespace MedicalAPI.Domain.Entities.Prescription;

public class PrescriptionRequest
{
    public PrescriptionRequest(string patientId, string diagnostic, List<MedicineRequest> medicine)
    {
        PatientId = patientId;
        Diagnostic = diagnostic;
        Medicine = medicine;
    }

    public string PatientId { get; set; }
    public string Diagnostic { get; set; }
    public List<MedicineRequest> Medicine { get; set; }
}