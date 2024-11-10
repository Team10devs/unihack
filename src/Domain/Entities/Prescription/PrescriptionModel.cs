using System.Runtime.InteropServices.JavaScript;
using MedicalAPI.Domain.Entities.Medicine;
using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class PrescriptionModel
{
    public PrescriptionModel() { }

    public PrescriptionModel(PatientModel patient, string doctorId, string diagnostic, List<MedicineModel> medicine)
    {
        Patient = patient;
        DoctorId = doctorId;
        Diagnostic = diagnostic;
        Medicine = medicine;
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public PatientModel Patient { get; set; }
    public string DoctorId { get; set; }
    public string Diagnostic { get; set; }
    public List<MedicineModel> Medicine { get; set; }
    public PrescriptionPdf? Pdf { get; set; }

    public void AddPdf(PrescriptionPdf pdf)
    {
        Pdf = pdf;
    }
}