namespace MedicalAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}