namespace MedicalAPI.Domain.Entities;

public class DeviceTokenRequest
{
    public string UserId { get; set; }
    public string DeviceToken { get; set; }
}