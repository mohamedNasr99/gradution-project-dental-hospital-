using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalHospital.DTOs
{
    public class ReceptionistRegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
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
        [RegularExpression("^\\d+$", ErrorMessage = "This field is National ID that is accepts numbers only.")]
        public string SSN { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.{1,40}$)(\p{L}[\p{L}\p{M}\s'’.-]*\s){3}\p{L}[\p{L}\p{M}\s'’.-]*$", ErrorMessage = "ادخل الاسم رباعي")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^\\d+$", ErrorMessage = "This field is phone number that is accepts numbers only.")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = new DateTime();
    }
}
