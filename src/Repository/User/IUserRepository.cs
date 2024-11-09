namespace MedicalAPI.Repository.User;

public interface IUserRepository
{
    Task SaveDeviceTokenAsync(string userId, string token);
}