using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Doctor;

public class DoctorRepository(AppDbContext context) : IDoctorRepository
{
    public async Task AddDoctorAsync(DoctorModel doctor)
    {
        try
        {
            context.Doctors.Add(doctor);  
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding doctor to the database", ex);
        }
    }

    public async Task<DoctorModel?> GetDoctorByIdAsync(string doctorId)
    {
        var doctor = await context.Doctors
            .Include( a=> a.DoctorAppointments)
            .Include( p => p.Patients )
            .FirstOrDefaultAsync(d => d.Id == doctorId);
        

        return doctor;
    }

    public async Task<DoctorModel> GetDoctorByEmailAsync(string email)
    {
        var doctor = await context.Doctors
            .Include( a=> a.DoctorAppointments)
            .Include( p => p.Patients )
            .FirstOrDefaultAsync(d => d.Email == email);

        if (doctor is null)
        {
            throw new Exception($"Doctor with email {email} does not exist");
        }

        return doctor;
    }

    public async Task<IEnumerable<DoctorModel>> GetAllAsync()
    {
        return await context.Doctors
            .Include(d => d.Patients)
            .Include(d=>d.DoctorAppointments)
            .ToListAsync();
    }
    
    public async Task<DoctorModel> GetDoctorByCodeAsync(string code)
    {
        try
        {
            if (code.Length != 6)
            {
                throw new ArgumentException("Code must be 6 characters long.");
            }
            
            return await context.Doctors.FirstOrDefaultAsync(d => d.Code == code);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching doctor by code: {ex.Message}");
            return null;
        }
    }
    
    public async Task UpdateDoctorAsync(DoctorModel doctor)
    {
        try
        {
            context.Doctors.Update(doctor);
            
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating doctor: {ex.Message}");
            throw; 
        }
    }
    
}