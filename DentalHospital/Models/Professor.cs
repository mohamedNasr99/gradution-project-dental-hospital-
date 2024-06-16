namespace DentalHospital.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;
        public string AdminSSN { get; set; } = string.Empty;
        public int ClinicId { get; set; } 
        public string Gender { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; 
        public string PhoneNumber { get; set; } = string.Empty; 
        public DateTime BirthDate { get; set; }
        public Clinic? Clinic { get; set; } 
        public Admin? Admin { get; set; }
    }
}
