namespace MedicalAPI.Domain.Entities;

public class MessageRequest
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
}