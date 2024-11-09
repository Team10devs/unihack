using MedicalAPI.Controllers;
using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Service.Firebase.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[Route("api/[controller]/Appointment")]
[ApiController]

public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    
    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    } 
    
    [HttpGet("ByPatientId")]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAppointmentsByPatientId(string patientId)
    {
        try
        {
            var appointments = 
                await _appointmentService.GetPatientAppointments(patientId);
            
            return Ok(appointments.Select(DoctorController.MapAppointmentResponse));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("ByDoctorId")]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAppointmentsByDoctorId(string doctorId)
    {
        try
        {
            var appointments = 
                await _appointmentService.GetDoctorAppointments(doctorId);
            
            return Ok(appointments.Select(DoctorController.MapAppointmentResponse));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("CreateAppointment")]
    public async Task<ActionResult<AppointmentResponse>> CreateAppointment(
        [FromBody] AppointmentRequest appointmentRequest)
    {
        try
        {
            var createdAppointment =
                await _appointmentService.CreateAppointment(appointmentRequest);
            return Ok(DoctorController.MapAppointmentResponse(createdAppointment));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    
}