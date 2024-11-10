using MedicalAPI.Controllers;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Service.Firebase.Patient;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController(IPatientService _patientService) : ControllerBase
{
    [HttpGet("PatientsByDoctorId")]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatientsByDoctorId(string doctorId)
    {
        try
        {
            var patientsByDoctorId =
                await _patientService.GetPatientsByDoctorIdAsync(doctorId);
            
            return Ok(patientsByDoctorId.Select(Mapping.MapPatientResponse));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("PatientByEmail")]
    public async Task<ActionResult<PatientResponse>> GetPatientByEmail(string email)
    {
        try
        {
            var patient = await _patientService.GetPatientByEmailAsync(email);
            return Ok(Mapping.MapPatientResponse(patient));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("PatientById")]
    public async Task<ActionResult<PatientResponse>> GetPatientById(string id)
    {
        try
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            
            return Ok(Mapping.MapPatientResponse(patient));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}