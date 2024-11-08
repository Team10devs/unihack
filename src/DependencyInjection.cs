using MedicalAPI.Repository.Database;
using MedicalAPI.Repository.Doctor;
using MedicalAPI.Service.Firebase;
using Microsoft.EntityFrameworkCore;

namespace MedicalAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<FirebaseService>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        
        return services;
    }
}