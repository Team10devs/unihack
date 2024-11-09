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

    public static PatientResponse MapPatientResponse(PatientModel patientModel)
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

    public static AppointmentResponse MapAppointmentResponse(AppointmentModel appointmentModel)
    {
        var doctorResponse = MapDoctorResponse(appointmentModel.Doctor);
        var patientResponse = MapPatientResponse(appointmentModel.Patient);

        return new AppointmentResponse(patientResponse, doctorResponse, appointmentModel.AppointmentStartTime,
            appointmentModel.AppointmentEndTime);
    }
}