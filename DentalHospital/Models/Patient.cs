﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DentalHospital.Models
{
    public class Patient
    {
        public string SSN { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty; 
        public string Clinic { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = new DateTime();
        public string Gender { get; set; } = string.Empty; 
        public bool IsPayed { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = new DateTime();
        public List<MedicalReport> MedicalReports { get; set; } = new List<MedicalReport>();
    }
}
