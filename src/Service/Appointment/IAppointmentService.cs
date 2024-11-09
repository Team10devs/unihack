using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Service.Firebase.Appointment;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentModel>> GetPatientAppointments(string patientId);
    Task<IEnumerable<AppointmentModel>> GetDoctorAppointments(string doctorId);
    Task<AppointmentModel> CreateAppointment(AppointmentRequest appointmentRequest);
    Task<bool> UpdateAppointmentStatus(string appointmentId, AppointmentStatus appointmentStatus);
}