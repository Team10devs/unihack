
using MedicalAPI.Domain.Enums;

namespace MedicalAPI.Domain.Entities.User;

public class UserModel : Entity.Entity
{
    public string? Email { get; set; }
    public string Fullname { get; set; }
    public UserRole Role { get; set; }

    public UserModel()
    {
        
    }
}