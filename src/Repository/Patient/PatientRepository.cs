using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using MedicalAPI.Repository.Doctor;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Patient;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    public async Task<List<PatientModel>> GetAllPatientsAsync()
    private readonly AppDbContext _context;
    private readonly IDoctorRepository _doctorRepository;

    public PatientRepository(AppDbContext context , IDoctorRepository doctorRepository)
    {
        _context = context;
        _doctorRepository = doctorRepository;
    }
    public async Task AddPatientAsync(PatientModel patient)
    {
        return await context.Patients
            .Include( d => d.Doctor )
            .Include( a => a.PatientAppointments)
            .ToListAsync();
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
        return await context.Patients
            .Where(patient => patient.Id == patientId)
            .OrderBy(patient => patient.Fullname)
            .FirstOrDefaultAsync();
    }

    public async Task<PatientModel?> GetPacientByIdAsync(string patientId)
    public async Task<PatientModel?> GetPatientByEmailAsync(string email)
    {
        return await context.Patients
            .Include( a=> a.PatientAppointments )
            .Include( d=> d.Doctor)
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task CreatePatientAsync(PatientModel patientModel)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);

        return patient;
        await context.Patients.AddAsync(patientModel);
        await context.SaveChangesAsync();
    }
    
    public async Task<DoctorModel> GetPacientByEmailAsync(string email)
    {
        var pacient = await _context.Doctors.FirstOrDefaultAsync(p => p.Email == email);

        if (pacient is null)
        {
            throw new Exception($"Pacient with email {email} does not exist");
        }

        return pacient;
    }
    
}