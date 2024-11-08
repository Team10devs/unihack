namespace MedicalAPI.Domain.Entities.Entity;

public class Entity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}