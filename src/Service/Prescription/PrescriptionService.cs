using MedicalAPI.Domain.Entities;
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Entities.Medicine;
using MedicalAPI.Domain.Entities.Prescription;
using MedicalAPI.Repository.Database;
using MedicalAPI.Service.Firebase.Mail;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MedicalAPI.Service.Firebase.Prescription;

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;

    public PrescriptionService(AppDbContext context,IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<PrescriptionModel> GeneratePrescription(PrescriptionRequest prescription)
    {
        try
        {
            var patient = await _context.Patients
                .Include(patientModel => patientModel.Doctor)
                .FirstOrDefaultAsync(d => d.Id == prescription.PatientId);
        
            if (patient is null)
            {
                throw new Exception($"Patient with id {prescription.PatientId} does not exist");
            }

            if (patient.Doctor is null)
            {
                throw new Exception($"Patient with id {prescription.PatientId} does not have a doctor");
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == patient.Doctor.Id);

            if (doctor is null)
            {
                throw new Exception($"Patient with id {prescription.PatientId} does not have a doctor");
            }
            
            var medicines = new List<MedicineModel>();
            foreach (var medicineRequest in prescription.Medicine)
            {
                medicines.Add(MedicineModel.MapMedicineModel(medicineRequest));
            }
            
            var prescriptionModel =
                new PrescriptionModel(patient, doctor.Id, prescription.Diagnostic, medicines);
            
            QuestPDF.Settings.License = LicenseType.Community;
            using MemoryStream memoryStream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1, Unit.Centimetre);
                    page.Size(PageSizes.A4);

                    // Header
                    page.Header()
                        .Row(row =>
                        {
                            row.Spacing(1, Unit.Centimetre);

                            // Doctor Info Section
                            row.RelativeItem()
                                .AlignLeft()
                                .Stack(stack =>
                                {
                                    stack.Item().Text("Doctor Information").FontSize(14).Bold();
                                    stack.Item().Text($"Name: {doctor.Fullname}").FontSize(12);
                                    stack.Item().Text($"Address: {doctor.Address}").FontSize(12);
                                });

                            // Patient Info Section
                            row.RelativeItem()
                                .AlignRight()
                                .Stack(stack =>
                                {
                                    stack.Item().Text("Patient Information").FontSize(14).Bold();
                                    stack.Item().Text($"Name: {patient.Fullname}").FontSize(12);
                                    stack.Item().Text($"Birthdate: {patient.BirthDate:dd/MM/yyyy}").FontSize(12);
                                    stack.Item().Text($"Gender: {patient.Gender}").FontSize(12);
                                });
                        });

                    // Content Section (Consolidated into one Content block)
                    page.Content()
                        .Stack(stack =>
                        {
                            // Diagnostic Section
                            stack.Item().Height(1f, Unit.Centimetre);
                            stack.Item().Text("Diagnostic").FontSize(16).Bold().AlignLeft();//.PaddingBottom(0.5f, Unit.Centimetre);
                            stack.Item().Height(0.5f, Unit.Centimetre);
                            stack.Item().Text(prescriptionModel.Diagnostic).FontSize(12).AlignLeft();//.PaddingBottom(1, Unit.Centimetre);

                            // Medicines Table
                            stack.Item().Height(1f, Unit.Centimetre);
                            stack.Item().Text("Prescribed Medicines").FontSize(16).Bold().AlignLeft();//.PaddingBottom(0.5f, Unit.Centimetre);
                            stack.Item().Height(0.5f, Unit.Centimetre);
                            stack.Item().Table(table =>
                            {
                                // Table Header
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Medicine Name").FontSize(12).Bold();
                                    header.Cell().Text("Dosage").FontSize(12).Bold();
                                    header.Cell().Text("Frequency").FontSize(12).Bold();
                                    header.Cell().Text("Start Date").FontSize(12).Bold();
                                    header.Cell().Text("End Date").FontSize(12).Bold();
                                });
                                // Table Rows
                                foreach (var medicine in prescriptionModel.Medicine)
                                {
                                    table.Cell().Text(medicine.Name).FontSize(10);
                                    table.Cell().Text(medicine.Dosage.ToString()).FontSize(10);
                                    table.Cell().Text(medicine.FrequencyPerDay.ToString()).FontSize(10);
                                    table.Cell().Text(medicine.StartDate.ToString("dd/MM/yyyy")).FontSize(10);
                                    table.Cell().Text(medicine.EndDate.ToString("dd/MM/yyyy")).FontSize(10);
                                }
                            });
                        });
                });
            });

            document.GeneratePdf(memoryStream);

            memoryStream.Position = 0;

            var prescriptionPdf = new PrescriptionPdf($"{patient.Fullname}_{DateTime.UtcNow:dd/MM/yyyy}.pdf", memoryStream.ToArray(), prescriptionModel.Id);
            prescriptionModel.Pdf = prescriptionPdf;

            _context.Prescriptions.Add(prescriptionModel);
            await _context.SaveChangesAsync();
            
            await _emailService.SendEmailAsync(doctor, patient, prescriptionPdf);
            
            return prescriptionModel;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<PrescriptionModel>> GetPatientPrescriptions(string patientId)
    {
        return await _context.Prescriptions
            .Include(p => p.Medicine)
            .Include(p=>p.Patient)
            .Where(p => p.Patient.Id == patientId)
            .ToListAsync();
    }

    public async Task<PrescriptionModel> GetById(string prescriptionId)
    {
        var prescription = await _context.Prescriptions.Include(p => p.Pdf)
            .FirstOrDefaultAsync(p => p.Id == prescriptionId);

        if (prescription is null)
        {
            throw new Exception($"Prescription with id {prescriptionId} does not exist");
        }

        if (prescription.Pdf is null)
        {
            throw new Exception($"Prescription with id {prescriptionId} does not have a pdf");
        }
        
        return prescription;
    }
}