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

            return Ok(doctors.Select(MapDoctorResponse));
        }

        internal DoctorResponse MapDoctorResponse(DoctorModel doctorModel)
        {
            var patientResponses = new List<PatientResponse>();
            foreach (var patient in doctorModel.Patients)
            {
                patientResponses.Add(MapPatientResponse(patient));
            }

            var appointmentResponses = new List<AppointmentResponse>();
            foreach (var appointment in doctorModel.DoctorAppointments)
            {
                appointmentResponses.Add(MapAppointmentResponse(appointment));
            }
            
            return new DoctorResponse(doctorModel.Email, doctorModel.Fullname, appointmentResponses, patientResponses);
        }

        internal PatientResponse MapPatientResponse(PatientModel patientModel)
        {
            return new PatientResponse(patientModel.Id, patientModel.Fullname, patientModel.Email,
                patientModel.Doctor.Id);
        }

        internal AppointmentResponse MapAppointmentResponse(AppointmentModel appointmentModel)
        {
            var doctorResponse = MapDoctorResponse(appointmentModel.Doctor);
            var patientResponse = MapPatientResponse(appointmentModel.Patient);

            return new AppointmentResponse(patientResponse, doctorResponse, appointmentModel.AppointmentDate,
                appointmentModel.AppointmentDuration);
        }
    }
}