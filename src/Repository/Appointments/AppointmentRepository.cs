using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Appointments;

public class AppointmentRepository(AppDbContext context) : IAppointmentRepository
{
    public async Task<List<AppointmentModel>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        return await context.Appointments
            .Include(a => a.Patient)
            .Where(p => p.Patient.Id == patientId)
            .ToListAsync();
    }

    public async Task<List<AppointmentModel>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {
        return await context.Appointments
            .Include(a => a.Doctor )
            .Where(p => p.Doctor.Id == doctorId )
            .ToListAsync();
    }

    public async Task CreateAppointmentAsync(AppointmentModel appointmentModel)
    {
        await context.Appointments.AddAsync(appointmentModel);
        await context.SaveChangesAsync();
    }

    public async Task<bool> PatchAppointmentStatus(string appointmentId, AppointmentStatus appointmentStatus)
    {
        var appointment = await context.Appointments
            .Where(a => a.Id == appointmentId)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return false;

        appointment.AppointmentStatus = appointmentStatus;
        await context.SaveChangesAsync();
        
        return true;
    }
}