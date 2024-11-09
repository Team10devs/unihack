using MedicalAPI.Service.Firebase;
using MedicalAPI.Repository.Doctor;
using Microsoft.AspNetCore.Mvc;
using MedicalAPI.Domain.DTOs.Doctor;

namespace MedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(FirebaseService firebaseService, IDoctorRepository doctorRepository)
        {
            _firebaseService = firebaseService;
            _doctorRepository = doctorRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DoctorRequest doctorRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Înregistrează doctorul în Firebase și primește UID-ul acestuia
                string firebaseUid = await _firebaseService.RegisterDoctorAsync(doctorRequest);

                return Ok(new { Message = "Doctor registered successfully", FirebaseUid = firebaseUid });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error registering doctor", Error = ex.Message });
            }
        }
    }
}