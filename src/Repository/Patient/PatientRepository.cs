using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Patient;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<PatientModel>> GetAllPatientsAsync()
    {
        return await _context.Patients
            .Include( a => a.PatientAppointments)
            .ToListAsync();
    }

    public async Task<PatientModel?> GetPatientByIdAsync(string patientId)
    {
        return await _context.Patients
            .Where(patient => patient.Id == patientId)
            .OrderBy(patient => patient.Fullname)
            .FirstOrDefaultAsync();
    }

    public async Task CreatePatientAsync(PatientModel patientModel)
    {
        _context.Patients.Add(patientModel);
        await _context.SaveChangesAsync();
    }
    
}