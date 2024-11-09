using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Patient;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    public async Task<List<PatientModel>> GetAllPatientsAsync()
    {
        return await context.Patients
            .Include( d => d.Doctor )
            .Include( a => a.PatientAppointments)
            .ToListAsync();
    }

    public async Task<PatientModel?> GetPatientByIdAsync(string patientId)
    {
        return await context.Patients
            .Where(patient => patient.Id == patientId)
            .OrderBy(patient => patient.Fullname)
            .FirstOrDefaultAsync();
    }

    public async Task<PatientModel?> GetPatientByEmailAsync(string email)
    {
        return await context.Patients
            .Include( a=> a.PatientAppointments )
            .Include( d=> d.Doctor)
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task CreatePatientAsync(PatientModel patientModel)
    {
        await context.Patients.AddAsync(patientModel);
        await context.SaveChangesAsync();
    }
    
}