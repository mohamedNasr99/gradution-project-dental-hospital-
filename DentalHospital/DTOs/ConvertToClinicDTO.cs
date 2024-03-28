using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class ConvertToClinicDTO
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string ClinicName { get; set; } = string.Empty;
    }
}
