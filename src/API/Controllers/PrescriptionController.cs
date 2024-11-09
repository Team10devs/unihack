using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Entities.Medicine;
using MedicalAPI.Domain.Entities.Prescription;
using MedicalAPI.Service.Firebase.Prescription;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost("GeneratePrescription")]
    public async Task<ActionResult> GeneratePrescription(PrescriptionRequest prescriptionRequest)
    {
        try
        {
            var prescriptionModel = await _prescriptionService.GeneratePrescription(prescriptionRequest);
            return File(prescriptionModel.Pdf.Data, "application/pdf", prescriptionModel.Pdf.Name);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetByPatientId")]
    public async Task<ActionResult<IEnumerable<PrescriptionResponse>>> GetPrescriptionList(string patientId)
    {
        var prescriptionList = await _prescriptionService.GetPatientPrescriptions(patientId);

        return Ok(prescriptionList.Select(MapPrescriptionResponse));
    }

    [HttpGet("GetPdfByPrescriptionId")]
    public async Task<ActionResult> GetPdf(string prescriptionId)
    {
        try
        {
            var prescription = await _prescriptionService.GetById(prescriptionId);
            return File(prescription.Pdf.Data, "application/pdf", prescription.Pdf.Name);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    internal PrescriptionResponse MapPrescriptionResponse(PrescriptionModel prescriptionModel)
    {
        var medicines = new List<MedicineResponse>();
        foreach (var medicineRequest in prescriptionModel.Medicine)
        {
            medicines.Add(MedicineModel.MapMedicineResponse(medicineRequest));
        }
        
        return new PrescriptionResponse(prescriptionModel.Id, prescriptionModel.PatientId, prescriptionModel.Diagnostic, medicines);
    }
}