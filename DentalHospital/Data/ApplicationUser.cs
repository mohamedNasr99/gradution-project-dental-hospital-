using Microsoft.AspNetCore.Identity;

namespace DentalHospital.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Clinic { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int Round { get; set; }
        public DateTime BirthDate { get; set; } = new DateTime();
    }
}
