namespace MedicalAPI.Domain.Entities.Entity.Documents;

public class MedicineModel : Entity
{
    public string Name { get; set; }
    public int Dosage { get; set; }
    public int FrequencyPerDay { get; set; }
    public int NumberOfDays { get; set; }

    public MedicineModel()
    {
        
    }
    
}