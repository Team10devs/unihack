using System.Net.Mail;
using System.Text;
using System.Web;
using MailKit;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MedicalAPI.Domain.Entities;
using MedicalAPI.Domain.Entities.User;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MedicalAPI.Service.Firebase.Mail;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly string _from;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;

    public EmailService(IOptions<EmailConfig> configuration)
    {
        _smtpServer = configuration.Value.EmailHost;
        _from = configuration.Value.From;
        _port = configuration.Value.Port;
        _username = configuration.Value.EmailUserName;
        _password = configuration.Value.EmailPassword;
    }

   public async Task SendEmailAsync(DoctorModel doctor, PatientModel patient, PrescriptionPdf prescriptionPdf)
{
    // Debug logging
    Console.WriteLine($"Starting email send process...");
    Console.WriteLine($"SMTP Server: {_smtpServer}");
    Console.WriteLine($"Port: {_port}");
    Console.WriteLine($"From Address: {_from}");
    Console.WriteLine($"Username: {_username}");
    Console.WriteLine($"Patient Email: {patient?.Email}");
    Console.WriteLine($"Patient Name: {patient?.Fullname}");
    Console.WriteLine($"Doctor Name: {doctor?.Fullname}");
    Console.WriteLine($"PDF Name: {prescriptionPdf?.Name}");
    Console.WriteLine($"PDF Data Length: {prescriptionPdf?.Data?.Length}");

    // Validate inputs
    if (doctor == null)
        throw new ArgumentNullException(nameof(doctor));
    if (patient == null)
        throw new ArgumentNullException(nameof(patient));
    if (prescriptionPdf == null)
        throw new ArgumentNullException(nameof(prescriptionPdf));
    if (string.IsNullOrEmpty(_from))
        throw new ArgumentException("From email address is not configured");
    if (string.IsNullOrEmpty(_smtpServer))
        throw new ArgumentException("SMTP server is not configured");
    if (string.IsNullOrEmpty(_username))
        throw new ArgumentException("SMTP username is not configured");
    if (string.IsNullOrEmpty(_password))
        throw new ArgumentException("SMTP password is not configured");
    if (string.IsNullOrEmpty(patient.Email))
        throw new ArgumentException("Patient email is required", nameof(patient));
    if (string.IsNullOrEmpty(patient.Fullname))
        throw new ArgumentException("Patient fullname is required", nameof(patient));
    if (string.IsNullOrEmpty(doctor.Fullname))
        throw new ArgumentException("Doctor fullname is required", nameof(doctor));
    if (prescriptionPdf.Data == null || prescriptionPdf.Data.Length == 0)
        throw new ArgumentException("Prescription PDF data is required", nameof(prescriptionPdf));
    if (string.IsNullOrEmpty(prescriptionPdf.Name))
        throw new ArgumentException("Prescription PDF name is required", nameof(prescriptionPdf));

    try
    {
        Console.WriteLine("Creating email message...");
        
        var message = new MimeMessage();
        
        // Set From address
        Console.WriteLine($"Setting From address: {_from}");
        message.From.Add(new MailboxAddress("Health Help", _from));
        
        // Set To address
        Console.WriteLine($"Setting To address: {patient.Email}");
        message.To.Add(new MailboxAddress(patient.Fullname, patient.Email));
        
        // Set Subject
        message.Subject = "Prescripție Medicală";

        // Create the HTML body
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                <h1 style='color: #333;'>Salut, {HttpUtility.HtmlEncode(patient.Fullname)}!</h1>
                <p style='line-height: 1.6;'>Doctorul tău a emis o nouă prescripție pentru tine. Găsești documentul atașat la acest email.</p>
                <p style='line-height: 1.6;'>Te rugăm să o consulți și să urmezi instrucțiunile indicate.</p>
                <p style='line-height: 1.6;'>Cu stimă,<br>Dr. {HttpUtility.HtmlEncode(doctor.Fullname)}</p>
            </div>";

        // Attach the PDF
        Console.WriteLine("Attaching PDF...");
        bodyBuilder.Attachments.Add(prescriptionPdf.Name, prescriptionPdf.Data);

        // Set the message body
        message.Body = bodyBuilder.ToMessageBody();

        Console.WriteLine("Connecting to SMTP server...");
        using (var client = new SmtpClient())
        {
            // Connect
            Console.WriteLine($"Connecting to {_smtpServer}:{_port}...");
            await client.ConnectAsync(_smtpServer, _port, SecureSocketOptions.StartTls);

            // Authenticate
            Console.WriteLine("Authenticating...");
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_username, _password);

            // Send the email
            Console.WriteLine("Sending email...");
            await client.SendAsync(message);

            // Disconnect
            Console.WriteLine("Disconnecting...");
            await client.DisconnectAsync(true);
        }

        Console.WriteLine("Email sent successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending email: {ex.GetType().Name}");
        Console.WriteLine($"Error message: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
        }
        
        throw new Exception($"Failed to send prescription email to {patient.Email}: {ex.Message}", ex);
    }
}

}
