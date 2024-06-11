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
        public string Gender { get; set; } = string.Empty;
        public List<MedicalReport> MedicalReports { get; set; } = new List<MedicalReport>();
        public Clinic Clinic { get; set; } = new Clinic();
        public Admin Admin { get; set; } = new Admin();
        public ApplicationUser  ApplicationUser { get; set; } = new ApplicationUser();
    }
}
