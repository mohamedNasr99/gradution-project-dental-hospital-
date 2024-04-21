using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class TreatmentDTO
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Treatment { get; set; } = string.Empty;
        [Required]
        public string StudentSSN { get; set; } = string.Empty;
    }
}
