using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Appointments;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;
    
    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<AppointmentModel>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Where(p => p.Patient.Id == patientId)
            .ToListAsync();
    }

    public async Task<List<AppointmentModel>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {
        return await _context.Appointments
            .Include(a => a.Doctor )
            .Where(p => p.Doctor.Id == doctorId )
            .ToListAsync();
    }

    public async Task CreateAppointmentAsync(AppointmentModel appointmentModel)
    {
        await _context.Appointments.AddAsync(appointmentModel);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> PatchAppointmentStatus(string appointmentId, AppointmentStatus appointmentStatus)
    {
        var appointment = await _context.Appointments
            .Where(a => a.Id == appointmentId)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return false;

        appointment.AppointmentStatus = appointmentStatus;
        await _context.SaveChangesAsync();
        
        return true;
    }
}