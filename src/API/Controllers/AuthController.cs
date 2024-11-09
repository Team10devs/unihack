using MedicalAPI.Domain.DTOs.Auth;
using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Service.Firebase;
using MedicalAPI.Service.Firebase.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly FirebaseService _firebaseService;

    public AuthController(FirebaseService firebaseService,IDoctorService doctorService)
    {
        _firebaseService = firebaseService;
    }
    
    [HttpPost("registerDoctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] DoctorRequest doctorRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string firebaseUid = await _firebaseService.RegisterDoctorAsync(doctorRequest);

            return Ok(new { Message = "Doctor registered successfully", FirebaseUid = firebaseUid });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error registering doctor", Error = ex.Message });
        }
    }

    [HttpPost("registerPatient")]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientRequest patientRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string firebaseUid = await _firebaseService.RegisterPatientAsync(patientRequest);
            
            return Ok(new { Message = "Patient registered successfully", FirebaseUid = firebaseUid });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error registering patient", Error = ex.Message });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            //get uid firebase and authentication token
            var customToken = await _firebaseService.LoginAsync(loginRequest.email);

            // verify type of user
            var userType = await _firebaseService.GetUserTypeAsync(loginRequest.email);

            return Ok(new { Token = customToken, UserType = userType });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = "Login failed", Error = ex.Message });
        }
    }

}