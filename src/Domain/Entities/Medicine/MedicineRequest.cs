namespace MedicalAPI.Domain.Entities.Medicine;

public class MedicineRequest
{
    public MedicineRequest(string name, int dosage, int frequencyPerDay, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Dosage = dosage;
        FrequencyPerDay = frequencyPerDay;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Name { get; set; }
    public int Dosage { get; set; }
    public int FrequencyPerDay { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}