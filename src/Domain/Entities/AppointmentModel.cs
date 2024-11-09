using System.ComponentModel.DataAnnotations;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class AppointmentModel : Entity
{
    public PatientModel Patient { get; set; }
    public DoctorModel Doctor { get; set; }
    public DateTime AppointmentStartTime { get; set; }
    public DateTime AppointmentEndTime { get; set; }   
    public AppointmentStatus AppointmentStatus { get; set; }
    public string Notes { get; set; }

    public AppointmentModel()
    {
        
    }
    
    public static async Task<AppointmentModel> CreateAppointmentAsync(
        PatientModel patientModel,
        DoctorModel doctorModel,
        DateTime appointmentStart, 
        DateTime appointmentEnd,
        string notes
    )
    {
        return new AppointmentModel
        {
            Patient = patientModel,
            Doctor = doctorModel,
            AppointmentStartTime = appointmentStart,
            AppointmentEndTime = appointmentEnd,
            Notes = notes,
            AppointmentStatus = AppointmentStatus.Scheduled
        };

    }
}