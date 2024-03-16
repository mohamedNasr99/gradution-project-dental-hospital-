using System.ComponentModel.DataAnnotations;

namespace DentalHospital.DTOs
{
    public class RoleDTO
    {
        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; } = string.Empty;
    }
}
