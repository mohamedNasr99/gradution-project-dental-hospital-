using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class SessionDTO
    {
        [Required]
        public string MedicalReportCode { get; set; } = string.Empty;
        [Required]
        public string Session { get; set; } = string.Empty;
        [Required]
        public string Treatment { get; set; } = string.Empty;
    }
}
