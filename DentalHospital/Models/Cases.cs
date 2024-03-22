using System.ComponentModel.DataAnnotations;

namespace DentalHospital.Models
{
    public class Cases
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PermissibleCases { get; set; }
    }
}
