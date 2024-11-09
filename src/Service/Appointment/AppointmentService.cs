using MedicalAPI.Repository.Appointments;

namespace MedicalAPI.Service.Firebase;

public class AppointmentService : IAppointmentRepository
{
    private IAppointmentRepository _appointmentRepository;

    public AppointmentService(AppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    
    
}