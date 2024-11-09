using Google.Protobuf.WellKnownTypes;
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.Database;

namespace MedicalAPI.Repository.Appointments;

public interface IAppointmentRepository
{
    Task<List<AppointmentModel>> GetAppointmentsByPatientIdAsync(string patientId);
    Task<List<AppointmentModel>> GetAppointmentsByDoctorIdAsync(string doctorId);
    Task CreateAppointmentAsync(AppointmentModel appointmentModel);
    Task<bool> PatchAppointmentStatus(string appointmentId,AppointmentStatus appointmentStatus);
}