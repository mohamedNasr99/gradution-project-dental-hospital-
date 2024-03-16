namespace DentalHospital.Models
{
    public class Receptionist
    {
        public string SSN { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty; 
        public DateTime BirthDate { get; set; } = new DateTime();
    }
}
