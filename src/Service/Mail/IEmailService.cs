using MedicalAPI.Domain.Entities;
using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.Service.Firebase.Mail;

public interface IEmailService
{
    Task SendEmailAsync(DoctorModel doctor, PatientModel patient, PrescriptionPdf prescriptionPdf);
}