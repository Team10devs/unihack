using MedicalAPI.API;
using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Service.Firebase;
using Microsoft.AspNetCore.Mvc;
using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.Entity.Documents;
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
        
        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();

            return Ok(doctors.Select(Mapping.MapDoctorResponse));
        }
        
        [HttpGet("GetDoctorById")]
        public async Task<ActionResult<DoctorResponse>> GetADoctorById(string id)
        {
            try
            {
                var doctor = await _doctorService.GetByIdAsync(id);
                
                return Ok(Mapping.MapDoctorResponse(doctor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetByDoctorEmail")]
        public async Task<ActionResult<DoctorResponse>> GetDoctorByEmail(string email)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByEmailAsync(email);
                
                return Ok(Mapping.MapDoctorResponse(doctor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}