using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MedicalAPI.Domain.Entities;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.Doctor;
using MedicalAPI.Repository.Patient;
using MedicalAPI.Repository.User;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly FirebaseMessaging _firebaseMessaging;
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    public MessageController(FirebaseMessaging firebaseMessaging, 
        IUserRepository userRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _firebaseMessaging = firebaseMessaging;
        _userRepository = userRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage(MessageRequest request)
    {
        try
        {
            var message = new Message
            {
                Notification = new Notification
                {
                    Title = request.Title,
                    Body = request.Body
                },
                Data = new Dictionary<string, string>
                {
                    { "senderId", request.SenderId },
                    { "receiverId", request.ReceiverId }
                },
                Token = await GetDeviceTokenAsync(request.ReceiverId)
            };

            var response = await _firebaseMessaging.SendAsync(message);
            return Ok(new { MessageId = response });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("save-token")]
    public async Task<IActionResult> SaveToken([FromBody] DeviceTokenRequest request)
    {
        try
        {
            // Store the token in your database associated with the user ID
            await _userRepository.SaveDeviceTokenAsync(request.UserId, request.DeviceToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task<string> GetDeviceTokenAsync(string userId)
    {
        var patient = await _patientRepository.GetPacientByIdAsync(userId);
        if (patient != null && !string.IsNullOrEmpty(patient.DeviceToken))
        {
            return patient.DeviceToken;
        }
        
        var doctor = await _doctorRepository.GetDoctorByIdAsync(userId);
        if (doctor != null && !string.IsNullOrEmpty(doctor.DeviceToken))
        {
            return doctor.DeviceToken;
        }
        
        throw new Exception("User does not exist or device token is missing");
    }

}