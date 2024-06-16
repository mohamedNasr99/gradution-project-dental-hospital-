using DentalHospital.Data;

namespace DentalHospital.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty; 
        public string AdminSSN { get; set; } = string.Empty;
        public int ClinicId { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty; 
        public DateTime BirthDate { get; set; } = new DateTime();
        public List<MedicalReport>? MedicalReports { get; set; } 
        public string Gender { get; set; } = string.Empty;
        public Clinic? Clinic { get; set; } 
        public Admin? Admin { get; set; }
        public ApplicationUser?  ApplicationUser { get; set; }
    }
}
