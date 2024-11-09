using MedicalAPI.Domain.DTOs.Auth;
using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Repository.Doctor;
using MedicalAPI.Repository.Patient;
using MedicalAPI.Repository.User;
using MedicalAPI.Service.Firebase;
using MedicalAPI.Service.Firebase.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly FirebaseService _firebaseService;
    private readonly IUserRepository _userRepository;

    public AuthController(FirebaseService firebaseService
        ,IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IUserRepository userRepository)
    {
        _firebaseService = firebaseService;
        _userRepository = userRepository;
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
            // Obține ID-ul Firebase și tokenul de autentificare
            var customToken = await _firebaseService.LoginAsync(loginRequest.email);
            
            var userType = await _firebaseService.GetUserTypeAsync(loginRequest.email);
            Console.WriteLine(userType);

            if (userType == "Patient")
            {
                var patient = await _userRepository.GetPatientByEmailAsync(loginRequest.email);
                if (patient == null)
                {
                    return Unauthorized(new { Message = "Login failed" + userType, Error = "Patient does not exist in the database" });
                }
                
                await _firebaseService.SaveDeviceTokenAsync(patient.Id, customToken);
            }
            else if (userType == "Doctor")
            {
                var doctor = await _userRepository.GetDoctorByEmailAsync(loginRequest.email);
                if (doctor == null)
                {
                    return Unauthorized(new { Message = "Login failed" + userType, Error = "Doctor does not exist in the database" });
                }
                
                await _firebaseService.SaveDeviceTokenAsync(doctor.Id, customToken);
            }
            else
            {
                return Unauthorized(new { Message = "Login failed", Error = "User type is invalid" });
            }

            return Ok(new { Token = customToken, UserType = userType });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = "Login failed", Error = ex.Message });
        }
    }




}