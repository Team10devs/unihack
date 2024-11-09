using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using MedicalAPI.Repository.Doctor;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Patient;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;
    private readonly IDoctorRepository _doctorRepository;

    public PatientRepository(AppDbContext context , IDoctorRepository doctorRepository)
    {
        _context = context;
        _doctorRepository = doctorRepository;
    }
    public async Task AddPatientAsync(PatientModel patient)
    {
        try
        {
            _context.Patients.Add(patient);  
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding patient to the database", ex);
        }
    }
    
    public async Task AddPatientToDoctorAsync(string code, PatientModel patient)
    {
        var doctor = await _doctorRepository.GetDoctorByCodeAsync(code);
        if (doctor == null)
        {
            throw new Exception("Doctor not found.");
        }
        
        doctor.Patients.Add(patient);
        
        await _doctorRepository.UpdateDoctorAsync(doctor);
    }

    public async Task<PatientModel> GetPacientByIdAsync(string patientId)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);

        if (patient is null)
        {
            throw new Exception("Patient not found.");
        }

        return patient;
    }
    
}