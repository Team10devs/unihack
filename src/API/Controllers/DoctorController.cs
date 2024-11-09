using MedicalAPI.Service.Firebase;
using MedicalAPI.Repository.Doctor;
using Microsoft.AspNetCore.Mvc;
using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.Entities.User;
using MedicalAPI.Service.Firebase.Doctor;

namespace MedicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;
        private readonly IDoctorService _doctorService;
        public DoctorController(FirebaseService firebaseService, IDoctorService doctorService)
        {
            _firebaseService = firebaseService;
            _doctorService = doctorService;
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
                string firebaseUid = await _firebaseService.RegisterDoctorAsync(doctorRequest);

                return Ok(new { Message = "Doctor registered successfully", FirebaseUid = firebaseUid });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error registering doctor", Error = ex.Message });
            }
        }
        
        [HttpGet(Name = "GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();

            return Ok(doctors.Select(Map));
        }

        private DoctorResponse Map(DoctorModel doctorModel)
        {
            return new DoctorResponse(doctorModel.Email, doctorModel.Fullname, [], []);
        }
    }
}