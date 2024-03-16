using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class StudentProfessorRegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Clinic { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        public int Round { get; set; }
    }
}
