using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class StudentProfessorRegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
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
        [RegularExpression("^[A-Z][a-z]*$", ErrorMessage ="This field is camal case , first letter is capatal and rest of letters is small.")]
        public string Role { get; set; } = string.Empty;
        public int Round { get; set; }
        [Required]
        [RegularExpression("^\\d+$", ErrorMessage = "This field is National ID that is accepts numbers only.")]
        public string SSN { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(?:[A-Za-z]+\\s+){3}(?:[A-Za-z]+\\s*)+$", ErrorMessage = "من فضلك ادخل اسمك رباعي")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^\\d+$", ErrorMessage = "This field is phone number that is accepts numbers only.")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; } = new DateTime();
    }
}
