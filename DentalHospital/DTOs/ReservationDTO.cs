using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DentalHospital.DTOs
{
    public class ReservationDTO
    {
        [Required]
        [RegularExpression(@"^(?=.{1,40}$)(\p{L}[\p{L}\p{M}\s'’.-]*\s){3}\p{L}[\p{L}\p{M}\s'’.-]*$", ErrorMessage = "ادخل الاسم رباعي")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Patient SSN")]
        public string PatientSSN { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = new DateTime();
        [Required]
        [Display(Name ="Patient number")]
        public string PatientNumber { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
    }
}
