using MedicalAPI.Domain.Entities.Entity.Documents;

namespace MedicalAPI.Domain.Entities.User;

public class DoctorModel : UserModel
{
    public string License { get; set; }
    public string Address { get; set; }
    public string Specialization { get; set; }
    public List<PatientModel> Patients { get; set; }
    public List<AppointmentModel> DoctorAppointments { get; set; }
    public string Code { get; set; } 

    public DoctorModel()
    {
    }
    
}