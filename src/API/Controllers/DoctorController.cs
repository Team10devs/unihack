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
        
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        
        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();

            return Ok(doctors.Select(MapDoctorResponse));
        }
        
        [HttpGet("GetDoctorById")]
        public async Task<ActionResult<DoctorResponse>> GetADoctorById(string id)
        {
            try
            {
                var doctor = await _doctorService.GetByIdAsync(id);
                
                return Ok(MapDoctorResponse(doctor));
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
                
                return Ok(MapDoctorResponse(doctor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            var doctor = MapDoctorResponse(patientModel.Doctor);
            var appointmentResponses = new List<AppointmentResponse>();
            
            foreach (var appointment in patientModel.PatientAppointments)
            {
                appointmentResponses.Add(MapAppointmentResponse(appointment));
            }
            
            return new PatientResponse(patientModel.Id, patientModel.Fullname, patientModel.Email,
                appointmentResponses, doctor);
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