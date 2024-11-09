using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.Appointments;

namespace MedicalAPI.Service.Firebase;

public class AppointmentService : IAppointmentRepository
{
    private IAppointmentRepository _appointmentRepository;

    public AppointmentService(AppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }


    public Task<List<AppointmentModel>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        throw new NotImplementedException();
    }

    public Task<List<AppointmentModel>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {
        throw new NotImplementedException();
    }

    public Task CreateAppointmentAsync(AppointmentModel appointmentModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PatchAppointmentStatus(string appointmentId, AppointmentStatus appointmentStatus)
    {
        throw new NotImplementedException();
    }
}