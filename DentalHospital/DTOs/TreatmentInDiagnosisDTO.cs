using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class TreatmentInDiagnosisDTO
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string MedicalHistory { get; set; } = string.Empty;
        [Required]
        public string DentalHistory { get; set; } = string.Empty;
        [Required]
        public string Diagnosis { get; set; } = string.Empty;
    }
}
