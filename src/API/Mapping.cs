using MedicalAPI.Domain.DTOs.Appointment;
using MedicalAPI.Domain.DTOs.Doctor;
using MedicalAPI.Domain.DTOs.Patient;
using MedicalAPI.Domain.Entities.Entity.Documents;
using MedicalAPI.Domain.Entities.User;

namespace MedicalAPI.API;

public static class Mapping
{
    public static DoctorResponse MapDoctorResponse(DoctorModel doctorModel)
    {
        var patientResponses = new List<PatientResponse>();

        if (doctorModel.Patients is not null)
        {
            foreach (var patient in doctorModel.Patients)
            {
                patientResponses.Add(MapPatientResponse(patient));
            }
        }
        else throw new Exception("Mapping Doctor Patients");

        var appointmentResponses = new List<AppointmentResponse>();
        if (doctorModel.DoctorAppointments is not null)
        {
            foreach (var appointment in doctorModel.DoctorAppointments)
            {
                appointmentResponses.Add(MapAppointmentResponse(appointment));
            }
        }
        else throw new Exception("Mapping Doctor Appointments");
            
        return new DoctorResponse(doctorModel.Id,doctorModel.Email, doctorModel.Fullname, appointmentResponses, patientResponses);
    }

    public static PatientResponse MapPatientResponse(PatientModel patientModel)
    {
        
        var appointmentResponses = new List<AppointmentResponse>();

        if (patientModel.PatientAppointments is not null)
        {
            foreach (var appointment in patientModel.PatientAppointments)
            {
                appointmentResponses.Add(MapAppointmentResponse(appointment));
            }
        }
        //else throw new Exception("Mapping Patient Appointments");
            
        return new PatientResponse(patientModel.Id, patientModel.Fullname, patientModel.Email,
            appointmentResponses, patientModel.Doctor != null ? patientModel.Doctor.Id : "");
    }   

    public static AppointmentResponse MapAppointmentResponse(AppointmentModel appointmentModel)
    {
        return new AppointmentResponse(appointmentModel.Id, 
            appointmentModel.Patient.Id,
            appointmentModel.Doctor.Id,
            appointmentModel.AppointmentStartTime,
            appointmentModel.AppointmentEndTime);
    }
}