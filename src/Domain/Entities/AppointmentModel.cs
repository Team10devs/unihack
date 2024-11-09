using System.ComponentModel.DataAnnotations;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class AppointmentModel : Entity
{
    public PatientModel Patient { get; set; }
    public DoctorModel Doctor { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentDuration { get; set; }   
    public AppointmentStatus AppointmentStatus { get; set; }
    public string Notes { get; set; }

    public AppointmentModel()
    {
        
    }
    
    public static async Task<AppointmentModel> CreateAppointmentAsync(
        PatientModel patientModel,
        DoctorModel doctorModel,
        DateTime appointmentStart, 
        TimeSpan appointmentDuration,
        string notes,
        AppointmentStatus appointmentStatus = AppointmentStatus.Scheduled
    )
    {
        return new AppointmentModel
        {
            Patient = patientModel,
            Doctor = doctorModel,
            AppointmentDate = appointmentStart,
            AppointmentDuration = appointmentStart.Add(appointmentDuration)
        };

    }
}