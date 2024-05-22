using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalHospital.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string SSN { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        public DateTime BirthDate { get; set; } = new DateTime();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Professor> Professors { get; set; } = new List<Professor>();

    }
}
