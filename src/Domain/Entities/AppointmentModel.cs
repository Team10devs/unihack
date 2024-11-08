using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class AppointmentModel : Entity
{
    public PatientModel Patient { get; set; }
    public DoctorModel Doctor { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }
    public string Notes { get; set; }

    public AppointmentModel()
    {
        
    }
}