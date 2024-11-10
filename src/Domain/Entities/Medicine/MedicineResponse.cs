namespace MedicalAPI.Domain.Entities.Medicine;

public class MedicineResponse
{
    public MedicineResponse(string id, string name, int dosage, int frequencyPerDay)
    {
        Id = id;
        Name = name;
        Dosage = dosage;
        FrequencyPerDay = frequencyPerDay;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public int Dosage { get; set; }
    public int FrequencyPerDay { get; set; }
}