using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Service.Firebase.Patient;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController(IPatientService patientService) : ControllerBase
{
    private readonly IPatientService _patientService = patientService;

    [HttpGet("PatientsByDoctorId")]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatientsByDoctorId(string doctorId)
    {
        try
        {
            var patientsByDoctorId =
                await _patientService.GetPatientsByDoctorIdAsync(doctorId);
            
            return Ok(patientsByDoctorId);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}