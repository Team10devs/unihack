using System.Runtime.InteropServices.JavaScript;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class PrescriptionModel : Entity
{
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public string Diagnostic { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<MedicineModel> Medicine { get; set; }

    public PrescriptionModel()
    {
        
    }
}