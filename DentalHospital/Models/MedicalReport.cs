using System.Diagnostics.CodeAnalysis;

namespace DentalHospital.Models
{
    public class MedicalReport
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; 
        public int PatientId { get; set; }
        public string PatientSSN { get; set; } = string.Empty;
        public string StudentSSN { get; set; } = string.Empty;
        [AllowNull]
        public int? StudentId { get; set; } 
        public string? Clinic { get; set; } 
        public bool IsPayed { get; set; }
        public string MedicalHistory { get; set; } = string.Empty;   
        public string DentalHistory { get; set; } = string.Empty; 
        public string Diagnosis { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public string Treatment { get; set; } = string.Empty;
        public DateTime dateTime { get; set; }
        public Patient? Patient { get; set; } 
        public Student? Student { get; set; } 
        public List<Session>? Sessions { get; set; }

    }
}
