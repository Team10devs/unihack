using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MedicalAPI.Domain.Entities;
using MedicalAPI.Domain.Enums;
using MedicalAPI.Repository.User;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAPI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly FirebaseMessaging _firebaseMessaging;
    private readonly IUserRepository _userRepository;
    public MessageController(FirebaseMessaging firebaseMessaging, IUserRepository userRepository)
    {
        _firebaseMessaging = firebaseMessaging;
        _userRepository = userRepository;
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
        // Implement logic to retrieve the device token for the given user ID
        // This could involve querying a database or other storage mechanism
        // to get the token associated with the user
        return "device_token_for_user_" + userId;
    }
}