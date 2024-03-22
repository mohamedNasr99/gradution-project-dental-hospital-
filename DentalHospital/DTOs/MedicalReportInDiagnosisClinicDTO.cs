using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class MedicalReportInDiagnosisClinicDTO
    {
        [Required]
        [Display(Name = "Student SSN")]
        public string StudentSSN { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Dental History")]
        public string DentalHistory { get; set; } = string.Empty;
        [Required]
        public string Diagnosis { get; set; } = string.Empty;
    }
}
