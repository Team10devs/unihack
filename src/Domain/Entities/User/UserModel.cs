
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.Entities.User;

public class UserModel : Entity.Entity
{
    public string Email { get; set; }
    public string Fullname { get; set; }
    public UserRole Role { get; set; }
    public string DeviceToken { get; set; }

    public UserModel()
    {
        
    }
}