using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;

namespace MedicalAPI.Domain.DTOs.Appointment;

public record AppointmentResponse(PatientResponse PatientResponse, DoctorResponse DoctorResponse, DateTime Date, TimeSpan Duration);