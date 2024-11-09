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
        return await context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
    }
}