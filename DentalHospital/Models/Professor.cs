namespace DentalHospital.Models
{
    public class Professor
    {
        public string SSN { get; set; } = string.Empty; 
        public string AdminSSN { get; set; } = string.Empty;
        public string ClinicId { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; 
        public string PhoneNumber { get; set; } = string.Empty; 
        public DateTime BirthDate { get; set; } = new DateTime();
        public Clinic Clinic { get; set; } = new Clinic();
        public Admin Admin { get; set; } = new Admin();
    }
}
