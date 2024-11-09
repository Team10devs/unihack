using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.Appointments;
using MedicalAPI.Repository.Doctor;
using MedicalAPI.Repository.Patient;

namespace MedicalAPI.Service.Firebase.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }
    
    public async Task<IEnumerable<AppointmentModel>> GetPatientAppointments(string patientId)
    {
        var patientAppointments = 
            await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);

        if (patientAppointments.Count == 0)
            throw new Exception($"Patient with id: {patientId} has no appointments");

        return patientAppointments;
    }

    public async Task<IEnumerable<AppointmentModel>> GetDoctorAppointments(string doctorId)
    {
        var doctorAppointments = 
            await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);
        
        if(doctorAppointments.Count == 0 )  
            throw new Exception($"Doctor with id: {doctorId} has no appointments");

        return doctorAppointments;
    }

    public async Task<AppointmentModel> CreateAppointment(AppointmentRequest appointmentRequest)
    {
        var appointmentPatient = 
           await  _patientRepository.GetPatientByIdAsync(appointmentRequest.patientId);
        
        if (appointmentPatient is null)
            throw new Exception($"Patient with id:{appointmentRequest.patientId} not found!");
        
        var appointmentDoctor =
            await _doctorRepository.GetDoctorByIdAsync(appointmentRequest.doctorId);

        if (appointmentDoctor is null)
            throw new Exception($"Doctor with id:{appointmentRequest.doctorId} not found!");

        var createdAppointment = await AppointmentModel.CreateAppointmentAsync(
            appointmentPatient,
            appointmentDoctor,
            appointmentRequest.start,
            appointmentRequest.end,
            appointmentRequest.notes
        );

        await _appointmentRepository.CreateAppointmentAsync(createdAppointment);
        
        return createdAppointment;
    }

    public async Task<bool> UpdateAppointmentStatus(string appointmentId, AppointmentStatus appointmentStatus)
    {
        var updateSucces =
            await _appointmentRepository.PatchAppointmentStatus(appointmentId, appointmentStatus);

        if (updateSucces is false)
            throw new Exception($"Changing appointment status with id:{appointmentId} failed.");
        
        return updateSucces;
    }
}