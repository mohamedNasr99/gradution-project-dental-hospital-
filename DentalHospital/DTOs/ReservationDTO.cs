using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DentalHospital.DTOs
{
    public class ReservationDTO
    {
        [Required]
        [RegularExpression("^(?:[A-Za-z]+\\s+){3}(?:[A-Za-z]+\\s*)+$", ErrorMessage ="من فضلك ادخل اسمك رباعي")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Patient SSN")]
        public string PatientSSN { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; } = new DateTime();
        [Required]
        [Display(Name ="Patient number")]
        public string PatientNumber { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
    }
}
