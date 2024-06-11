using DentalHospital.Data;

namespace DentalHospital.Models
{
    public class Receptionist
    {
        public int Id { get; set; }
        public string userid { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty; 
        public DateTime BirthDate { get; set; } = new DateTime();
        public List<ApplicationUser> ApplicationUser { get; set; } = new List<ApplicationUser>(); 
    }
}
