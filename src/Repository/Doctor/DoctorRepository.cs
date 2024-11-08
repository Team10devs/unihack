using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.Doctor;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddDoctorAsync(DoctorModel doctor)
    {
        try
        {
            _context.Doctors.Add(doctor);  
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding doctor to the database", ex);
        }
    }

    public async Task<DoctorModel?> GetDoctorByIdAsync(string doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
    }
}