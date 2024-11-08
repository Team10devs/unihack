using System.Runtime.InteropServices.JavaScript;
using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class PrescriptionModel : Entity
{
    public PatientModel Patient { get; set; }
    public DoctorModel Doctor { get; set; }
    public string Diagnostic { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<MedicineModel> Medicine { get; set; }

    public PrescriptionModel()
    {
        
    }
}