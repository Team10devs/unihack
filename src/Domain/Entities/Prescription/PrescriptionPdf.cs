using MedicalAPI.Domain.Entities.Entity.Documents;

namespace MedicalAPI.Domain.Entities;

public class PrescriptionPdf
{
    public PrescriptionPdf(string name, byte[] data, string prescriptionId)
    {
        Name = name;
        Data = data;
        PrescriptionId = prescriptionId;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public byte[] Data { get; set; }
    public string PrescriptionId { get; set; }
}