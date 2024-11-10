using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Entities.Medicine;

namespace MedicalAPI.Domain.Entities.Prescription;

public class PrescriptionResponse
{
    public PrescriptionResponse(string prescriptionId, string patientId, string patientName, string diagnostic, List<MedicineResponse> medicine)
    {
        PrescriptionId = prescriptionId;
        PatientId = patientId;
        this.patientName = patientName;
        Diagnostic = diagnostic;
        Medicine = medicine;
    }

    public string PrescriptionId { get; set; }
    public string PatientId { get; set; }
    public string patientName { get; set; }
    public string Diagnostic { get; set; }
    public List<MedicineResponse> Medicine { get; set; }
}