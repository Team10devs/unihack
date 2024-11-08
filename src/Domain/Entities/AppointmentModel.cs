using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class AppointmentModel : Entity
{
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }
    public string Notes { get; set; }

    public AppointmentModel()
    {
        
    }
}