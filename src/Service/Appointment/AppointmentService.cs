using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.Entities.Entity.Documents;

namespace MedicalAPI.Service.Appointment;

public interface IAppointmentService
{
    Task<List<AppointmentModel>> GetPatientAppointments(string patientId);
    Task<List<AppointmentModel>> GetDoctorAppointments(string doctorId);
    Task<AppointmentModel> CreateAppointment(AppointmentRequest appointmentRequest);
}