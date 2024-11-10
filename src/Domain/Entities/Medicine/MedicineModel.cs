namespace MedicalAPI.Domain.Entities.Medicine;

public class MedicineModel
{
    public MedicineModel(string name, int dosage, int frequencyPerDay, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Dosage = dosage;
        FrequencyPerDay = frequencyPerDay;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public int Dosage { get; set; }
    public int FrequencyPerDay { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public MedicineModel()
    {
        
    }

    public static MedicineModel MapMedicineModel(MedicineRequest medicineRequest)
    {
        return new MedicineModel(medicineRequest.Name, medicineRequest.Dosage, medicineRequest.FrequencyPerDay,
            medicineRequest.StartDate, medicineRequest.EndDate);
    }

    public static MedicineResponse MapMedicineResponse(MedicineModel medicineModel)
    {
        return new MedicineResponse(medicineModel.Id, medicineModel.Name, medicineModel.Dosage,
            medicineModel.FrequencyPerDay);
    }
}