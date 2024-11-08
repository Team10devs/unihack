﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MedicalAPI.Domain.Entities.User;

public class PatientModel : UserModel
{
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public DoctorModel Doctor { get; set; }
    public string MedicalHistory { get; set; }

    public PatientModel()
    {
        
    }
}