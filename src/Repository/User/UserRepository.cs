using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI.Repository.User;

public class UserRepository : IUserRepository
{
    protected readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SaveDeviceTokenAsync(string userId, string token)
    {
        var pacient = await _appDbContext.Patients.FirstOrDefaultAsync(p => p.Id == userId);

        if (pacient != null)
        {
            pacient.DeviceToken = token;
            await _appDbContext.SaveChangesAsync();

            return;
        }

        var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == userId);

        if (doctor != null)
        {
            doctor.DeviceToken = token;
            await _appDbContext.SaveChangesAsync();
            
            return;
        }

        throw new Exception($"User with id {userId} does not exist");
    }
    
    public async Task<object> GetUserByEmailAsync(string email)
    {
        var pacient = await _appDbContext.Patients.FirstOrDefaultAsync(p => p.Email == email);

        if (pacient != null)
        {
            return pacient; 
        }

        var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(d => d.Email == email);

        if (doctor != null)
        {
            return doctor; 
        }

        throw new Exception($"User with email {email} does not exist");
    }
    
    public async Task<PatientModel> GetPatientByEmailAsync(string email)
    {
        return await _appDbContext.Patients
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<DoctorModel> GetDoctorByEmailAsync(string email)
    {
        return await _appDbContext.Doctors
            .FirstOrDefaultAsync(d => d.Email == email);
    }

}