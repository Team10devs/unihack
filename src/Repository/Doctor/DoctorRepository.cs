using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using MedicalAPI.Repository.Doctor;
using Microsoft.EntityFrameworkCore;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddDoctorAsync(DoctorModel doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<DoctorModel> GetDoctorByIdAsync(string doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
    }
}