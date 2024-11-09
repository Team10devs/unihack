using MedicalAPI.Repository.Database;
using MedicalAPI.Repository.Doctor;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MedicalAPI.Repository.Patient;
using MedicalAPI.Repository.User;
using MedicalAPI.Service.Firebase;
using MedicalAPI.Service.Firebase.Doctor;
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

        var firebaseApp = FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "secret.json"))
        });
        
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<FirebaseService>();
        services.AddSingleton(FirebaseMessaging.GetMessaging(firebaseApp));
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}