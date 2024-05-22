using Microsoft.AspNetCore.Identity;

namespace DentalHospital.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string SSN { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Clinic { get; set; } 
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int Round { get; set; }
        public DateTime BirthDate { get; set; } = new DateTime();
    }
}
